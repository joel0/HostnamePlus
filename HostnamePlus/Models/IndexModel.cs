using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace HostnamePlus.Models
{
    /// <summary>
    /// A model with the information for the home page.  This model injests a
    /// HttpRequest to populate the fields.
    /// </summary>
    public class IndexModel
    {
        /// <summary>
        /// A string of the client's User Agent from the request headers. Empty
        /// string if the header is missing.
        /// </summary>
        public String UserAgent;
        /// <summary>
        /// The IpInfoModel with the client's IP address.
        /// </summary>
        private readonly IpInfoModel RemoteIpInfo;
        /// <summary>
        /// If the X-Forwarded-For header exists, this is an array populated
        /// with each IP found in the forward chain. Note that this is
        /// unsanitized user input, which may not be valid IP addresses or
        /// truthful information.
        /// </summary>
        public readonly IpInfoModel[] ProxiedIpsInfo;

        /// <summary>
        /// Constructs the model with the client's request connection.
        /// </summary>
        /// <param name="Request">The HttpRequest that the client opened to
        /// load the page</param>
        public IndexModel(HttpRequest Request)
        {
            UserAgent = Request.Headers["User-Agent"].ToString();
            RemoteIpInfo = new IpInfoModel(Request.HttpContext.Connection.RemoteIpAddress);
            ProxiedIpsInfo = GetProxiedIpsInfo(Request.Headers["X-Forwarded-For"].ToString(), 10);
        }

        /// <summary>
        /// The URL for the API to load the other IP from on the client side.
        /// If the client is connected via IPv4, this URL is for the IPv6 only
        /// API.
        /// </summary>
        public String OtherIpAPIURL {
            get {
                // If the connection is IPv6, hit the IPv4 API, and vice versa.
                String subdomain = RemoteIpInfo.IsIPv6 ? "ipv4" : "ipv6";
                return String.Format("//{0}.{1}/api/OtherIp",
                    subdomain, Program.BASE_URL);
            }
        }

        /// <summary>
        /// The friendly text for the alternate IP family.
        /// </summary>
        public String OtherIpType {
            get {
                return RemoteIpInfo.IsIPv6 ? "IPv4" : "IPv6";
            }
        }

        /// <summary>
        /// The friendly text for the IP family.
        /// </summary>
        public String IpType {
            get {
                return RemoteIpInfo.IsIPv6 ? "IPv6" : "IPv4";
            }
        }

        /// <summary>
        /// Parses the X-Forwarded-For header into IpInfoModels. This
        /// immediately starts host name resolution asynchronously on all IPs.
        /// </summary>
        /// <param name="xForwardedFor">a comma separated list of IP addresses
        /// from one or more X-Forwarded-For headers.</param>
        private static IpInfoModel[] GetProxiedIpsInfo(string xForwardedFor, int limit) {
            if (String.IsNullOrWhiteSpace(xForwardedFor)) {
                return new IpInfoModel[0];
            }

            String[] untrimmedIps = xForwardedFor.Split(',', limit + 1, StringSplitOptions.RemoveEmptyEntries);
            // Constraint the IPs to process to the size limit.
            if (untrimmedIps.Length > limit) {
                untrimmedIps[limit] = "truncated results";
            }
            // Trim and convert raw header values to IpInfoModels.
            List<IpInfoModel> ipsInfo = new List<IpInfoModel>(untrimmedIps.Length);
            Array.ForEach(untrimmedIps, untrimmedIp => {
                if (!String.IsNullOrWhiteSpace(untrimmedIp)) {
                    ipsInfo.Add(new IpInfoModel(untrimmedIp.Trim()));
                }
            });
            return ipsInfo.ToArray();
        }

        /// <summary>
        /// Whether there's any IPs found in X-Forwarded-For header(s).
        /// </summary>
        public Boolean HasProxiedIps {
            get {
                return ProxiedIpsInfo.Length > 0;
            }
        }

        /// <summary>
        /// The reverse DNS of the client's IP, or N/A if the DNS lookup fails.
        /// IPv6 reverse DNS lookups often fail.
        /// </summary>
        public async Task<String> GetHostNameAsync() {
            return await RemoteIpInfo.HostNameTask;
        }

        /// <summary>
        /// A string of the client's IP.
        /// </summary>
        public String IP {
            get {
                return RemoteIpInfo.IpString;
            }
        }
    }
}
