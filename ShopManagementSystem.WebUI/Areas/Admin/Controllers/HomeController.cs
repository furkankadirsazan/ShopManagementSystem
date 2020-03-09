using ShopManagementSystem.WebUI.Controllers;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Shop")]
    public class HomeController : BaseController
    {
        private readonly IUnitOfWork uow;
        public HomeController(IUnitOfWork _uow) : base(_uow)
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
        public ActionResult Index()
        {          
            return View();
        }
        public ActionResult Faq()
        {
            return View();
        }
    }
}