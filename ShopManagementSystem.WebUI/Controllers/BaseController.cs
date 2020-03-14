using ShopManagementSystem.WebUI.Areas.Admin.Models;
using ShopManagementSystem.WebUI.Extensions.Security;
using ShopManagementSystem.WebUI.Extensions.String;
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
        public FileUploadModel SetUploadFileName(string filename,string extension)  => 
            new FileUploadModel
            {
                FileName = filename,
                Path = string.Format(UploadStrings.ProductImageFilePath, Guid.NewGuid() + extension)
            };

      
            
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (User.Identity.Name != null)
            {
                var username = User.Identity.Name;
                if (!string.IsNullOrEmpty(username))
                {
                    var shop = _uow.Shops.Find(u => u.Username == username).FirstOrDefault();
                    if (shop != null)
                    {
                        string fullName = string.Concat(new string[] { shop.Name + " " + shop.Surname });
                        ViewData.Add("FullName", fullName);
                        ViewData.Add("UserRole", shop.Roles.Name);
                    }                 
                }
            }
            else
            {
                FormsAuthentication.SignOut();
                Redirect("~/security/login");
            }
            base.OnActionExecuted(filterContext);
        }
    }
}