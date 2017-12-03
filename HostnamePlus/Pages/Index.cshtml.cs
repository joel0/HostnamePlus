using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.Sockets;

namespace HostnamePlus.Pages
{
    public class IndexModel : PageModel
    {
        private const String BASE_URL = "hostname.jmay.us";

        public String OtherIpAPIURL {
            get {
                // If the connection is IPv6, hit the IPv4 API, and vice versa.
                String subdomain = IsIPv6 ? "ipv4" : "ipv6";
                return String.Format("//{0}.{1}/api/OtherIp", subdomain, BASE_URL);
            }
        }

        public String OtherIpType {
            get {
                return IsIPv6 ? "IPv4" : "IPv6";
            }
        }

        private IPAddress RemoteIpAddress {
            get {
                return Request.HttpContext.Connection.RemoteIpAddress;
            }
        }

        public bool IsIPv6 {
            get {
                return RemoteIpAddress.AddressFamily == AddressFamily.InterNetworkV6;
            }
        }

        public String HostName {
            get {
                try {
                    return Dns.GetHostEntry(RemoteIpAddress).HostName;
                } catch {
                    return "N/A";
                }
            }
        }

        public String IP {
            get {
                return RemoteIpAddress.ToString();
            }
        }

        public String UserAgent {
            get {
                return Request.Headers["User-Agent"].ToString();
            }
        }

        public void OnGet()
        {

        }
    }
}
