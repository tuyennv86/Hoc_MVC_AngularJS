using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    public class HomeController : Controller
    {
        IProductCategoryService _productCategoryService;
        public HomeController (IProductCategoryService productCategoryService)
        {
            this._productCategoryService = productCategoryService;
        }

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
            var model = _productCategoryService.GetAll();
            var listProductCategory = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
            return PartialView("_HeaderBottom", listProductCategory);
        }
    }
}