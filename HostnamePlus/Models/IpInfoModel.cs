using System;
using System.Net;
using System.Threading.Tasks;

namespace HostnamePlus.Models
{
    /// <summary>
    /// A model for working with IP addresses and resolving information about
    /// them.
    /// </summary>
    public class IpInfoModel
    {
        /// <summary>
        /// The parsed IP address, if it's a valid IP. Otherwise null.
        /// </summary>
        private IPAddress Ip = null;
        /// <summary>
        /// The provided IP address string, if constructed with a string.
        /// Null if constructed with an already parsed IP.
        /// </summary>
        private string RawIp = null;
        /// <summary>
        /// The async task that's resolving of the hostname.
        /// "-" if the IP is malformed.
        /// "N/A" if DNS resolution fails for any reason (usually NXDOMAIN).
        /// Otherwise, contains the FQDN string.
        /// </summary>
        private Task<String> HostNameTask = null;

        /// <summary>
        /// Constructs a model for requesting info about the provided IP.
        /// </summary>
        public IpInfoModel(IPAddress Ip) {
            this.Ip = Ip;
        }

        /// <summary>
        /// Constructs a model for requesting info about the provided IP.
        /// </summary>
        /// <param name="Ip">the IP address as a string, which is allowed to be
        /// malformed. Malformed IPs will provide limited info.</param>
        public IpInfoModel(string Ip) {
            this.RawIp = Ip;
            IPAddress.TryParse(Ip, out this.Ip);
        }

        /// <summary>
        /// Asynchronously start DNS resolution of the IP, if it hasn't been
        /// started yet.
        /// </summary>
        public void StartHostNameResolution() {
            if (HostNameTask == null) {
                HostNameTask = ResolveHostNameAsync();
            }
        }

        /// <summary>
        /// Attempts DNS resolution with the system's default DNS resolver.
        /// </summary>
        /// <returns>
        /// A task doing resolution.
        /// "-" if the IP is malformed.
        /// "N/A" if the resolution fails.
        /// Otherwise, the FQDN from DNS.
        /// </returns>
        private Task<String> ResolveHostNameAsync() {
            return Task.Run(() => {
                if (Ip != null) {
                    try {
                        return Dns.GetHostEntry(Ip).HostName;
                    } catch {
                        return "N/A";
                    }
                } else {
                    return "-";
                }
            });
        }

        /// <summary>
        /// Query reverse DNS for the host name. Resolution may be slow.
        /// </summary>
        /// <returns>"-" if the IP is malformed. "N/A" if the resolution fails.
        /// Otherwise, the FQDN returned by DNS.</returns>
        public async Task<String> GetHostNameAsync() {
            StartHostNameResolution();
            return await HostNameTask;
        }

        /// <summary>
        /// Query reverse DNS for the host name. This synchronous function
        /// blocks until resolution completes.
        /// <summary>
        /// <returns>"-" if the IP is malformed. "N/A" if the resolution fails.
        /// Otherwise, the FQDN returned by DNS.</returns>
        public String GetHostName() {
            StartHostNameResolution();
            HostNameTask.Wait();
            return HostNameTask.Result;
        }

        /// <summary>
        /// The IP address as a string. If the IP provided to the constructor
        /// was malformed, this will return a malformed IP.
        /// </summary>
        public String IpString {
            get {
                if (Ip != null) {
                    return Ip.ToString();
                }
                return RawIp;
            }
        }

        /// <summary>
        /// Checks whether the IP address is an IPv6 address or IPv4. Malformed
        /// IPs result in a "false" (IPv4) return value.
        /// </summary>
        public Boolean IsIPv6 {
            get {
                if (Ip == null) {
                    return false;
                }
                return Ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6;
            }
        }
    }
}