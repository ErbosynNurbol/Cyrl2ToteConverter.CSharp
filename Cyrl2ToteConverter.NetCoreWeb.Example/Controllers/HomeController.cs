using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cyrl2ToteConverter.NetCoreWeb.Example.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cyrl2Tote(string cyrlText)
        {
            string toteText = Cyrl2ToteHelper.Cyrl2Tote(cyrlText);
            return Json(new { toteText });
        }
    }
}