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
    [RoutePrefix("api/slide")]
    //[Authorize]
    public class SlideController : ApiControllerBase
    {
        ISlideService _slideService;
        public SlideController(IErrorService errorService, ISlideService slideService) : base(errorService)
        {
            this._slideService = slideService;
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetByID(HttpRequestMessage request, int Id)
        {
            return CreateHttpResponse(request, () => {
                var slides = _slideService.GetById(Id);
                var slideVm = Mapper.Map<SlideViewModel>(slides);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, slideVm);
                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () => {
                var listsilde = _slideService.GetAll();
                var listslideVm = Mapper.Map<List<SlideViewModel>>(listsilde);
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listslideVm);
                return response;
            });
        }

        [Route("getpageing")]
        [HttpGet]
        public HttpResponseMessage GetPageing(HttpRequestMessage request, int page, int pagesize)
        {
            return CreateHttpResponse(request, () => {
                int totalRow = 0;
                var listsilde = _slideService.GetAllPaging(page, pagesize, out totalRow);
                var responseData = Mapper.Map<List<SlideViewModel>>(listsilde);
                var paginationSet = new PaginationSet<SlideViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pagesize)
                };               
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }

        [Route("addslide")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Post(HttpRequestMessage request, SlideViewModel slideVm)
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
                    Slide slide = new Slide();
                    slide.UpdateSlide(slideVm);
                    var resul = _slideService.Add(slide);
                    _slideService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.OK, resul);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Put(HttpRequestMessage request, SlideViewModel slideVm)
        {
            return CreateHttpResponse(request, () => {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    Slide slide = new Slide();
                    slide.UpdateSlide(slideVm);
                    _slideService.Update(slide);
                    _slideService.SaveChanges();
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
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    _slideService.Delete(id);
                    _slideService.SaveChanges();
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
                        _slideService.Delete(Id);
                    }
                    _slideService.SaveChanges();
                    response = request.CreateResponse(HttpStatusCode.OK, ids.Count);
                }
                return response;
            });
        }
    }
}
