using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HostnamePlus.Models;

namespace HostnamePlus.Controllers
{
    [Produces("application/json")]
    [Route("api/OtherIp")]
    public class OtherIpController : Controller
    {
        // GET: api/OtherIp
        [HttpGet]
        public IpModel Get()
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return new IpModel(Request);
        }
    }
}
