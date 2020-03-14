using ShopManagementSystem.WebUI.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.Controllers
{
    public class HomePageController : BaseController
    {
        private readonly IUnitOfWork uow;
        public HomePageController(IUnitOfWork _uow):base(_uow)
        {
            uow = _uow;
        }

        public IUnitOfWork Uow
        {
            get
            {
                return uow;
            }
        }
        // GET: Home
        //[Route("kaymek")]
        public ActionResult Index()
        {
            return View();
        }
        
    }
}