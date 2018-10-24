using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using AutoMapper;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

namespace TeduShop.Web.Api
{
    [RoutePrefix("api/productcategory")]
    public class ProductCategoryController : ApiControllerBase
    {
        private IProductCategoryService _productCategoryService;

        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategorySerice) : base(errorService)
        {
            this._productCategoryService = productCategorySerice;
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetByID(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () => {

                var product = _productCategoryService.GetById(id);
                var productVm = Mapper.Map<ProductCategoryViewModel>(product);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, productVm);
                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyword, int page, int pageSize)
        {
            return CreateHttpResponse(request, () =>
             {
                 int totalRow = 0;
                 var listproductCategory = _productCategoryService.GetAll(keyword);
                 totalRow = listproductCategory.Count();
                 var query = listproductCategory.OrderByDescending(x => x.CreatedBy).Skip(page * pageSize).Take(pageSize);
                 var responseData = Mapper.Map<List<ProductCategoryViewModel>>(query);
                 var paginationSet = new PaginationSet<ProductCategoryViewModel>()
                 {
                     Items = responseData,
                     Page = page,
                     TotalCount = totalRow,
                     TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                 };

                 HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                 return response;
             });
        } 
        [Route("getallparent")]
        [HttpGet]
        public HttpResponseMessage GetallParent(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {                
                var listproductCategory = _productCategoryService.GetAll();               
                var responseData = Mapper.Map<List<ProductCategoryViewModel>>(listproductCategory);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }
        [Route("Add")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Post(HttpRequestMessage request, ProductCategoryViewModel productCategoryVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    ProductCategory productCategory = new ProductCategory();
                    productCategory.UpdateProductCategory(productCategoryVm);
                    var category = _productCategoryService.Add(productCategory);
                    _productCategoryService.Save();
                    response = request.CreateResponse(HttpStatusCode.OK, category);
                }
                return response;
            });
        }

        [Route("Update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Put(HttpRequestMessage request, ProductCategoryViewModel productCategoryVm)
        {
            return CreateHttpResponse(request, () => {
                HttpResponseMessage response = null;
                if(!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    ProductCategory productCategory = new ProductCategory();
                    productCategory.UpdateProductCategory(productCategoryVm);                    
                    _productCategoryService.Update(productCategory);
                    _productCategoryService.Save();
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }
        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage request, int Id)
        {
            return CreateHttpResponse(request, () => {
                HttpResponseMessage response = null;
                if(!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }else
                {                    
                    _productCategoryService.Delete(Id);
                    _productCategoryService.Save();
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string listId)
        {
            return CreateHttpResponse(request, () => {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var ids = new JavaScriptSerializer().Deserialize<List<int>>(listId);
                    foreach (int Id in ids)
                    {
                        _productCategoryService.Delete(Id);
                    }
                    _productCategoryService.Save();
                    response = request.CreateResponse(HttpStatusCode.OK, ids.Count);
                }
                return response;
            });
        }
    }
}