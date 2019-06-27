using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Net.Sockets;

namespace HostnamePlus.Models
{
    /// <summary>
    /// A model with the information for the home page.  This model injests a
    /// HttpRequest to populate the fields.
    /// </summary>
    public class IndexModel
    {
        /// <summary>
        /// The request object of connection from the client.
        /// </summary>
        private readonly HttpRequest Request;

        /// <summary>
        /// Constructs the model with the client's request connection.
        /// </summary>
        /// <param name="Request">The HttpRequest that the client opened to
        /// load the page</param>
        public IndexModel(HttpRequest Request)
        {
            this.Request = Request;
        }

        /// <summary>
        /// The URL for the API to load the other IP from on the client side.
        /// If the client is connected via IPv4, this URL is for the IPv6 only
        /// API.
        /// </summary>
        public String OtherIpAPIURL {
            get {
                // If the connection is IPv6, hit the IPv4 API, and vice versa.
                String subdomain = IsIPv6 ? "ipv4" : "ipv6";
                return String.Format("//{0}.{1}/api/OtherIp",
                    subdomain, Program.BASE_URL);
            }
        }

        /// <summary>
        /// The friendly text for the alternate IP family.
        /// </summary>
        public String OtherIpType {
            get {
                return IsIPv6 ? "IPv4" : "IPv6";
            }
        }

        /// <summary>
        /// The friendly text for the IP family.
        /// </summary>
        public String IpType {
            get {
                return IsIPv6 ? "IPv6" : "IPv4";
            }
        }

        /// <summary>
        /// Simple check of the IP address family.
        /// </summary>
        public bool IsIPv6 {
            get {
                return RemoteIpAddress.AddressFamily == AddressFamily.InterNetworkV6;
            }
        }

        /// <summary>
        /// The client's IP address, sourced from the Request.
        /// </summary>
        private IPAddress RemoteIpAddress {
            get {
                return Request.HttpContext.Connection.RemoteIpAddress;
            }
        }

        /// <summary>
        /// The reverse DNS of the client's IP, or N/A if the DNS lookup fails.
        /// IPv6 reverse DNS lookups often fail.
        /// </summary>
        public String HostName {
            get {
                try {
                    return Dns.GetHostEntry(RemoteIpAddress).HostName;
                } catch {
                    return "N/A";
                }
            }
        }

        /// <summary>
        /// A string of the client's IP.
        /// </summary>
        public String IP {
            get {
                return RemoteIpAddress.ToString();
            }
        }

        /// <summary>
        /// A string of the client's User Agent from the request headers. Empty
        /// string if the header is missing.
        /// </summary>
        public String UserAgent {
            get {
                return Request.Headers["User-Agent"].ToString();
            }
        }
    }
}
