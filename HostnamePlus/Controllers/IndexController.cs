using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HostnamePlus.Models;

namespace HostnamePlus.Controllers
{
    public class IndexController : Controller
    {
        public IActionResult Index()
        {
            IndexModel model = new IndexModel(Request);
            String view = "Index";
            if (Request.Headers["User-Agent"].ToString().StartsWith("curl")) {
                view = "Curl";
                Response.ContentType = "text/plain";
            }
            return View(view, model);
        }
    }
}