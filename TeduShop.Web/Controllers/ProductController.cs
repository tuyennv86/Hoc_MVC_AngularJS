using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using TeduShop.Common;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Models;
using PagedList;
using System.Web.Script.Serialization;

namespace TeduShop.Web.Controllers
{
    public class ProductController : Controller
    {
        IProductService _productService;
        IProductCategoryService _productCategoryService;
        ITagService _tagService;

        public ProductController(IProductService productService, IProductCategoryService productCategoryService, ITagService tagService)
        {
            this._productService = productService;
            this._productCategoryService = productCategoryService;
            this._tagService = tagService;
        }
        // GET: Product
        public ActionResult Category(int id, int? Page)
        {

            var category = _productCategoryService.GetById(id);
            var categoryVm = Mapper.Map<ProductCategory, ProductCategoryViewModel>(category);
            ViewBag.Title = categoryVm.Name;

            int PageSize = Convert.ToInt32(ConfigHelper.GetByKey("PageSize"));
            int total = 0;
            int PageIndex = Page ?? 1;
            var listProduct = _productService.GetListProductByCategoryPageing(id, PageIndex, PageSize, out total);
            var listProductVm = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(listProduct);
            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = listProductVm,
                MaxPage = Convert.ToInt32(ConfigHelper.GetByKey("MaxPage")),
                TotalCount = total,
                TotalPages = (int)Math.Ceiling((double)total / PageSize)
            };

            return View(paginationSet);
        }

        public ActionResult Detail(int id)
        {
            var product = _productService.GetById(id);
            _productService.InCreateView(id);
            var productVm = Mapper.Map<Product, ProductViewModel>(product);
            ViewBag.Title = productVm.Name;

            var productRelate = _productService.GetRelate(id, 12);
            var productRelateVm = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productRelate);
            ViewBag.relatedProducts = productRelateVm;

            List<string> listImg = new JavaScriptSerializer().Deserialize<List<string>>(productVm.MoreImages);
            ViewBag.moreImg = listImg;

            var listTag = _tagService.GetAllByProductId(id);
            var listTagVm = Mapper.Map<IEnumerable<Tag>, IEnumerable<TagViewModel>>(listTag);
            ViewBag.tag = listTagVm;

            return View(productVm);
        }

        public ActionResult ListProductTag(string tag, int? page)
        {
            int PageSize = Convert.ToInt32(ConfigHelper.GetByKey("PageSize"));
            int total = 0;
            int PageIndex = page ?? 1;
            var listProduct = _productService.GetAllByTagPaging(tag, PageIndex, PageSize, out total);
            var listProductVm = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(listProduct);
            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = listProductVm,
                MaxPage = Convert.ToInt32(ConfigHelper.GetByKey("MaxPage")),
                TotalCount = total,
                TotalPages = (int)Math.Ceiling((double)total / PageSize)
            };
            return View(paginationSet);
        }

        public JsonResult GetListProductByName(string keyword)
        {
            var listProduct = _productService.GetListProductByName(keyword);
            return Json(new
            {
                data = listProduct
            }, JsonRequestBehavior.AllowGet);
        }
    }
}