using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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
    [RoutePrefix("api/product")]
    public class ProductController : ApiControllerBase
    {
        IProductService _productService;
        public ProductController(IErrorService errorService, IProductService productService) : base(errorService)
        {
            this._productService = productService;
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () => {
                var product = _productService.GetById(id);
                var productVm = Mapper.Map<ProductViewModel>(product);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, productVm);
                return response;
            });
        }
        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage Getall(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {              
                var listproduct = _productService.GetAll();                            
                var responseData = Mapper.Map<List<ProductViewModel>>(listproduct);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("getpage")]
        [HttpGet]
        public HttpResponseMessage GetPage(HttpRequestMessage request, string keyword, int page, int pageSize)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var listproduct = _productService.GetAll(keyword);
                totalRow = listproduct.Count();
                var query = listproduct.OrderByDescending(x => x.CreatedBy).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<List<ProductViewModel>>(query);
                var paginationSet = new PaginationSet<ProductViewModel>()
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

        [Route("addproduct")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Post(HttpRequestMessage request, ProductViewModel productVm)
        {            
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if(!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    Product product = new Product();
                    product.UpdateProduct(productVm);
                    var resul = _productService.Add(product);
                    _productService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.OK, resul);
                }
                
                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Put(HttpRequestMessage request, ProductViewModel productVm)
        {
            //productVm.CreatedDate = DateTime.ParseExact(productVm.CreatedDate.ToString(), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

            return CreateHttpResponse(request, () => {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    Product product = new Product();                    
                    product.UpdateProduct(productVm);
                    _productService.Update(product);
                    _productService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () => {
                HttpResponseMessage response = null;
                if(!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    _productService.Delete(id);
                    _productService.SaveChanges();
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
                        _productService.Delete(Id);
                    }
                    _productService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.OK, ids.Count);
                }
                return response;
            });
        }
    }
}