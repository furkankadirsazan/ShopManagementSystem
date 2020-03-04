using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.Controllers
{
    public class HomePageController : Controller
    {
        //private readonly IUnitOfWork uow;
        //public HomePageController(IUnitOfWork _uow)
        //{
        //    uow = _uow;
        //}

        //public IUnitOfWork Uow
        //{
        //    get
        //    {
        //        return uow;
        //    }
        //}
        // GET: Home
        //[Route("kaymek")]
        public ActionResult Index()
        {
            return View();
        }
        
    }
}