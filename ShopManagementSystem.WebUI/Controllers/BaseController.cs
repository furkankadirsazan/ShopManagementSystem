using ShopManagementSystem.WebUI.Repository.Abstract;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace ShopManagementSystem.WebUI.Controllers
{
    public class BaseController : Controller
    {
        public readonly IUnitOfWork _uow;

        public BaseController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (User.Identity.Name != null)
            {
                var username = User.Identity.Name;
                if (!string.IsNullOrEmpty(username))
                {
                    var user = _uow.Users.Find(u => u.Username == username).FirstOrDefault();
                    if (user != null)
                    {
                        string fullName = string.Concat(new string[] { user.FullName });
                        ViewData.Add("FullName", fullName);
                        ViewData.Add("UserRole", user.Roles.Name);
                    }
                }
            }
            else
            {
                FormsAuthentication.SignOut();
                RedirectToAction("Login", "Security", new { Area = "Admin" });
            }
            base.OnActionExecuted(filterContext);
        }

    }
}