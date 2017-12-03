using System;
using Microsoft.AspNetCore.Mvc;
using HostnamePlus.Models;

namespace HostnamePlus.Controllers
{
    /// <summary>
    /// This controller chooses the view based on the UserAgent.
    /// Curl gets a simplified text-only response, for readability.
    /// </summary>
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

            Response.Headers.Add("Link",
                String.Format(
                    "<{0}>; rel=preload; as=script,<{1}>; rel=preload; as=style",
                    Program.getOtherIpJsPath, Program.mainCssPath));
            return View(view, model);
        }
    }
}