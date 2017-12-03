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
        private IPAddress RemoteIpAddress
        {
            get
            {
                return Request.HttpContext.Connection.RemoteIpAddress;
            }
        }

        public bool IsIPv6
        {
            get
            {
                return RemoteIpAddress.AddressFamily == AddressFamily.InterNetworkV6;
            }
        }

        public String HostName
        {
            get
            {
                try {
                    return Dns.GetHostEntry(RemoteIpAddress).HostName;
                } catch {
                    return "N/A";
                }
            }
        }

        public String IP
        {
            get
            {
                return RemoteIpAddress.ToString();
            }
        }

        public String UserAgent
        {
            get
            {
                return Request.Headers["User-Agent"].ToString();
            }
        }

        public void OnGet()
        {

        }
    }
}
