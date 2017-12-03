using System;
using Microsoft.AspNetCore.Mvc;
using HostnamePlus.Models;

namespace HostnamePlus.Controllers
{
    [Produces("application/json")]
    [Route("api/OtherIp")]
    public class OtherIpController : Controller
    {
        // GET: api/OtherIp
        /// <summary>
        /// This API returns a JSON object with the request's IP and hostname.
        /// It is intended to be loaded from IPv4 if the main page was loaded
        /// via IPv6, and vice versa.
        /// </summary>
        /// <returns>The client's connection inforation.</returns>
        [HttpGet]
        public IndexModel Get()
        {
            String origin = String.Format("{0} {1}.{0} {2}.{0}", Program.BASE_URL, "ipv4", "ipv6");
            Response.Headers.Add("Access-Control-Allow-Origin", origin);
            return new IndexModel(Request);
        }
    }
}
