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
        IProductService _productService;
        ISlideService _slideService;
        public HomeController (IProductCategoryService productCategoryService, ISlideService slideService, IProductService productService)
        {
            this._productCategoryService = productCategoryService;
            this._slideService = slideService;
            this._productService = productService;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Hệ thống bán đồ thể thao";

            var productHome = _productService.GetHome(true);
            var productHomeVm = Mapper.Map<IEnumerable<Product>,IEnumerable<ProductViewModel>>(productHome);

            var productHot = _productService.GetHot(true);
            var productHotVm = Mapper.Map<IEnumerable<ProductViewModel>>(productHot);

            HomeViewModel homeVm = new HomeViewModel();
            homeVm.ProductHome = productHomeVm;
            homeVm.ProductHot = productHotVm;

            return View(homeVm);
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}

        [ChildActionOnly]
        public PartialViewResult HeaderTop()
        {           
            return PartialView("PartialView/_headerTop");
        }

        [ChildActionOnly]
        public PartialViewResult HeaderBottom()
        {
            var model = _productCategoryService.GetAll();
            var listProductCategory = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
            return PartialView("PartialView/_HeaderBottom", listProductCategory);
        }

        [ChildActionOnly]
        public PartialViewResult SlideTop()
        {
            var model = _slideService.GetAll();            
            var listSlide = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(model);
            return PartialView("PartialView/_slidePartialView", listSlide);
            
        }
    }
}