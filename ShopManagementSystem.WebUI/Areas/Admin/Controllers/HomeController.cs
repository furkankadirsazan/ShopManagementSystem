using ShopManagementSystem.WebUI.Areas.Admin.ViewModels;
using ShopManagementSystem.WebUI.Controllers;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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

        [HttpGet]
        public ActionResult UserInfo()
        {
            if (string.IsNullOrEmpty(ViewData["FullName"] as string))
            {
                return RedirectToAction("Login", "Security");
            }

            return PartialView("_UserInfo");
        }
        [HttpGet]
        public ActionResult UserAvatar()
        {
            if (string.IsNullOrEmpty(ViewData["FullName"] as string))
            {
                return RedirectToAction("Login", "Security");
            }

            return PartialView("_UserAvatar");
        }
        // GET: Home
        public ActionResult Index()
        {
            int shopID = (int)ViewData["ShopID"];
            var userrole = ViewData["ShopRole"] as string;

            StatisticViewModel vm = new StatisticViewModel();
        
            vm.OrdersInWait = (userrole == "Admin") ? uow.Orders.Find(a => a.OrderStatusID == 1).Count() :
               uow.Orders.Find(a => a.ShopID == shopID && (a.OrderStatusID == 1)).Count();

            vm.OrdersInProcess = (userrole == "Admin") ? uow.Orders.Find(a => a.OrderStatusID == 2 || a.OrderStatusID == 3 || a.OrderStatusID == 7).Count() :
               uow.Orders.Find(a => a.ShopID == shopID && (a.OrderStatusID == 2 || a.OrderStatusID == 3 || a.OrderStatusID == 7)).Count();

            vm.OrdersInCompleted = (userrole == "Admin") ? uow.Orders.Find(a => a.OrderStatusID == 14).Count() :
               uow.Orders.Find(a => a.ShopID == shopID && (a.OrderStatusID == 14)).Count();

            vm.OrdersInCancelled = (userrole == "Admin") ? uow.Orders.Find(a => a.OrderStatusID == 10).Count() :
               uow.Orders.Find(a => a.ShopID == shopID && (a.OrderStatusID == 10)).Count();

            vm.OrdersInCargo = (userrole == "Admin") ? uow.Orders.Find(a => a.OrderStatusID == 4).Count() :
               uow.Orders.Find(a => a.ShopID == shopID && (a.OrderStatusID == 4)).Count();

            vm.OrdersInUnsuccesfull = (userrole == "Admin") ? uow.Orders.Find(a => a.OrderStatusID == 5 || a.OrderStatusID == 6 || a.OrderStatusID == 8 || 
               a.OrderStatusID == 12 || a.OrderStatusID == 13 || a.OrderStatusID == 15).Count() :
               uow.Orders.Find(a => a.ShopID == shopID && (a.OrderStatusID == 5 || a.OrderStatusID == 6 || a.OrderStatusID == 8 ||
               a.OrderStatusID == 12 || a.OrderStatusID == 13 || a.OrderStatusID == 15)).Count();

            vm.OrdersInReturnBack = (userrole == "Admin") ? uow.Orders.Find(a => a.OrderStatusID == 11 || a.OrderStatusID == 9).Count() :
               uow.Orders.Find(a => a.ShopID == shopID && (a.OrderStatusID == 11 ||a.OrderStatusID ==9)).Count();

            vm.OrdersTotal = (userrole == "Admin") ? uow.Orders.GetAll().Count() : uow.Orders.Find(a => a.ShopID == shopID).Count();

            return View(vm);
        }
        public ActionResult Faq()
        {
            return View();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (User.Identity.Name != null)
            {
                var username = User.Identity.Name;
                if (!string.IsNullOrEmpty(username))
                {
                    var shop = uow.Shops.Find(u => u.Username == username).FirstOrDefault();
                    if (shop != null)
                    {
                        ViewData.Add("ShopID", shop.ID);
                        ViewData.Add("ShopRole", shop.Roles.Name);
                    }
                }
            }
            else
            {
                FormsAuthentication.SignOut();
                RedirectToAction("Login", "Security");
            }
            base.OnActionExecuting(filterContext);
        }
    }

}