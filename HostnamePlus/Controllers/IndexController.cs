using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HostnamePlus.Controllers
{
    public class IndexController : Controller
    {
        public IActionResult Index()
        {
            return View(new Models.IndexModel(Request));
        }
    }
}