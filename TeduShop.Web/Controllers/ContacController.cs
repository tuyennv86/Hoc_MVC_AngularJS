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
    public class ContacController : Controller
    {
        IContacDetailService _contacDetailService;
        public ContacController(IContacDetailService contacDetailService)
        {
            this._contacDetailService = contacDetailService;
        }
      

        // GET: Contac
        public ActionResult Index()
        {
            var data = _contacDetailService.GetSingDefault(true);
            var dataVm = Mapper.Map<ContacDetail, ContacDetailViewModel>(data);
            //ViewBag.lat = dataVm.LatMap;
            //ViewBag.lng = dataVm.LngMap;
            return View(dataVm);
        }
    }
}