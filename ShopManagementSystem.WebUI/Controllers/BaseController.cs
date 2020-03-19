using ShopManagementSystem.WebUI.Areas.Admin.Models;
using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Extensions.String;
using ShopManagementSystem.WebUI.Models;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System;
using System.Globalization;
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
        public FileUploadModel SetUploadFileName(string filename, string extension) =>
            new FileUploadModel
            {
                FileName = filename,
                Path = string.Format(UploadStrings.ProductImageFilePath, Guid.NewGuid() + extension)
            };

        public string ToTitleCase(string input) => new CultureInfo("tr-TR", false).TextInfo.ToTitleCase(input);
        public Shops ShopBuilder(CreateAccountModel entity) =>
            new Shops
            {
                RoleID = SystemStrings.ShopRoleID,
                ProvinceID = entity.ProvinceID,
                CountyID = entity.CountyID,
                Username = entity.Username,
                Password = entity.Password,
                Name = ToTitleCase(entity.Name),
                Surname = ToTitleCase(entity.Surname),
                Email = entity.Email,
                Phone = entity.Phone,
                Title = ToTitleCase(entity.Title),
                BannerImagePath = UploadStrings.ShopDefaultBannerFilePath,
                LogoPath = UploadStrings.ShopDefaultLogoFilePath,
                Point = 0,
                Address = ToTitleCase(entity.Address),
                TaxAdministration = ToTitleCase(entity.TaxAdministration),
                TaxNumber = ToTitleCase(entity.TaxNumber),
                ShopWebsite = (!string.IsNullOrEmpty(entity.ShopWebsite)) ? entity.ShopWebsite.ToLower() : "" ,
                AuthenticationCode = _uow.Shops.CreateAuthenticationCode(),
                RegistrationDate = DateTime.Now,
                IsAuthenticated = false,
                IsRemember = false
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