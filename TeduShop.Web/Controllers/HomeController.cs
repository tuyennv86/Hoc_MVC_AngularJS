using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeduShop.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [ChildActionOnly]
        public PartialViewResult HeaderTop()
        {
            return PartialView("_headerTop");
        }

        [ChildActionOnly]
        public PartialViewResult HeaderBottom()
        {
            return PartialView("_HeaderBottom");
        }
    }
}