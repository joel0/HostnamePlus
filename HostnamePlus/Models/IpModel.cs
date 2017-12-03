using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HostnamePlus.Models
{
    public class IpModel
    {
        public readonly String IpAddress;
        public readonly String HostName;

        public IpModel(String IpAddress, String HostName)
        {
            this.IpAddress = IpAddress;
            this.HostName = HostName;
        }

        public IpModel(HttpRequest Request)
        {
            IPAddress ip = Request.HttpContext.Connection.RemoteIpAddress;
            IpAddress = ip.ToString();
            try {
                HostName = Dns.GetHostEntry(ip).HostName;
            } catch {
                HostName = "N/A";
            }
        }
    }
}
