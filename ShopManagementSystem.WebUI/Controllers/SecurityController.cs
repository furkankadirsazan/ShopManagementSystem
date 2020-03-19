using ShopManagementSystem.WebUI.Areas.Admin.Models;
using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Extensions.Log;
using ShopManagementSystem.WebUI.Extensions.Mail.Concrete;
using ShopManagementSystem.WebUI.Extensions.Security;
using ShopManagementSystem.WebUI.Extensions.String;
using ShopManagementSystem.WebUI.Models;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ShopManagementSystem.WebUI.Controllers
{
    public class SecurityController : BaseController
    {
        private readonly IUnitOfWork uow;
        public SecurityController(IUnitOfWork _uow):base(_uow)
        {
            uow = _uow;
        }
        // GET: Security
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(ShopLoginModel shopEntity)
        {
            if (ModelState.IsValid)
            {
                var shop = uow.Shops.GetByUsernameAndPassword(shopEntity.Username, shopEntity.Password);

                if (shop != null)
                {

                    if (!shop.IsAuthenticated)
                    {
                        return Json(new { Url = "/security/login", Status = "NotAuthentication" });
                    }

                    if (shopEntity.IsRemember)
                    {
                        shop.IsRemember = shopEntity.IsRemember;
                        uow.Shops.Edit(shop);
                        uow.SaveChanges();
                        FormsAuthentication.SetAuthCookie(shop.Username, Convert.ToBoolean(shop.IsRemember));
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(shop.Username, Convert.ToBoolean(shop.IsRemember));
                    }

                    Session["ShopID"] = shop.ID;
                    Session["FullName"] = shop.Name+ " " + shop.Surname;
                    Session["ShopEmail"] = shop.Email;
                    Session["Role"] = shop.RoleID;
                    Session["RoleName"] = shop.Roles.Name;

                    return Json(new { Url = "/admin/home/index", Status = "Success" });
                }
                else if (shopEntity.Username == null || shopEntity.Password == null)
                {
                    return Json(new { Url = "/security/login", Status = "None" });
                }
                else
                {
                    return Json(new { Url = "/security/login", Status = "Fail" });
                }
            }
            return View(shopEntity);
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login","Security");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ForgotMyPassword()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult ForgotMyPassword(ShopLoginModel shopEntity)
        {
            var shop = uow.Shops.GetByEmail(shopEntity.Email);
            if (shop != null)
            {
                try
                {
                    string newPassword = Membership.GeneratePassword(6, 1);

                    Email email = new Email();
                    email.SendForNewPassword(shopEntity.Email, newPassword);

                    shop.Password = newPassword;
                    uow.Shops.Edit(shop);
                    uow.SaveChanges();
                }
                catch (Exception)
                {
                    return Json(new { Url = "/security/login", Status = "Fail" });
                }
                return Json(new { Url = "/security/login", Status = "Success" });
            }
            else if (shopEntity.Email == null)
            {
                return Json(new { Url = "/security/login", Status = "NonEmail" });
            }
            else
            {
                return Json(new { Url = "/security/login", Status = "Fail" });
            }
        }

        [AllowAnonymous]
        [Log]
        public ActionResult VerifyAccount()
        {
            try
            {
                var token = Request.QueryString["token"].ToString();
                var shopId = Request.QueryString["sid"].ToString();
                if (string.IsNullOrEmpty(shopId))
                {
                    TempData["ResponseStatus"] = "NonUsableAccount";
                    return RedirectToAction("login", "security");
                }
                if (string.IsNullOrEmpty(token))
                {
                    TempData["ResponseStatus"] = "EmptyToken";
                    return RedirectToAction("login", "security");
                }
                var shop = uow.Shops.Get(Convert.ToInt32(shopId));
                if (shop != null && HashString.MD5Hash(shop.AuthenticationCode) == token)
                {
                    TempData["ResponseStatus"] = "SuccessVerification";
                    shop.AuthenticationCode = null;
                    shop.IsAuthenticated = true;
                    uow.Shops.Edit(shop);
                    uow.SaveChanges();
                    return RedirectToAction("login", "security");
                }
                else
                {
                    TempData["ResponseStatus"] = "NotSuccessVerification";
                    return RedirectToAction("login", "security");
                }
            }
            catch (Exception)
            {
                TempData["ResponseStatus"] = "VerificationError";
                return RedirectToAction("login", "security");
            }


        }

        public ActionResult Counties(int provinceId)
        {
            var counties = uow.Counties.GetAll().Where(x => x.ProvinceID == provinceId).Select(x => new { ID = x.ID, Name = x.Name }).ToList();
            return Json(counties);
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult CreateAccount()
        {
            ViewBag.Provinces = uow.Provinces.SetProvinceDropdownList();
            return View();
        }
       

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccount(CreateAccountModel entity)
        {
            ViewBag.Provinces = uow.Provinces.SetProvinceDropdownList();

            if (entity == null) return Json(new { Url = "/security/login", Status = "NullObject" });

            if ( 
                 entity.ProvinceID == 0 || entity.CountyID == 0 || entity.Username == null || entity.Password == null || 
                 entity.Name == null || entity.Surname == null || entity.Email == null || entity.Phone == null || entity.Title == null || 
                 entity.Address == null  || entity.TaxAdministration == null || entity.TaxAdministration == null || entity.TaxNumber == null
               ) return Json(new { Url = "/security/login", Status = "NoToPassMandatoryFields" });

            if (entity.Password != entity.PasswordAgain) return Json(new { Url = "/security/login", Status = "PasswordsAreNotSame" });

            if (entity.Phone.Length != 11) return Json(new { Url = "/security/login", Status = "CorrectThePhone" });

            if (!(entity.Password.Length > 6 && entity.Password.Length < 15)) return Json(new { Url = "/security/login", Status = "PasswordCheck" });

            if (!entity.IsAccepted) return Json(new { Url = "/security/login", Status = "NotAccepted" });

            if (ModelState.IsValid)
            {
                if (uow.Shops.HasSameRecords(entity)) return Json(new { Url = "/security/login", Status = "Warning" });            
                else
                {                   
                    try
                    {
                        var shop = ShopBuilder(entity);
                        uow.Shops.Add(shop);
                        uow.SaveChanges();
                        try
                        {
                            Email email = new Email();
                            email.SendForSignUp(shop.Email, ToTitleCase(shop.Title));
                        }
                        catch (Exception)
                        {
                            return Json(new { Url = "/security/login", Status = "MailNotSend" });
                        }                   
                        return Json(new { Url = "/security/login", Status = "Success" });
                    }
                    catch (Exception)
                    {
                        return Json(new { Url = "/security/login", Status = "Fail" });
                    }
                }
            }
            return View(entity);
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
                        ViewData.Add("ShopEmail", shop.Email);
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