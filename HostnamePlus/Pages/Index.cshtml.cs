using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HostnamePlus.Pages
{
    public class IndexModel : PageModel
    {
        public bool IsIPv6
        {
            get
            {
                return Request.HttpContext.Connection.RemoteIpAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6;
            }
        }

        public String HostName
        {
            get
            {
                return "TODO HOSTNAME";
            }
        }

        public String IP
        {
            get
            {
                return "TODO IP";
            }
        }

        public String UserAgent
        {
            get
            {
                return "TODO USERAGENT";
            }
        }

        public void OnGet()
        {

        }
    }
}
