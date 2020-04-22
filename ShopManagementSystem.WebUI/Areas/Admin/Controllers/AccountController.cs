using ShopManagementSystem.WebUI.Areas.Admin.Models;
using ShopManagementSystem.WebUI.Controllers;
using ShopManagementSystem.WebUI.Extensions.Log;
using ShopManagementSystem.WebUI.Extensions.String;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ShopManagementSystem.WebUI.Areas.Admin.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork uow;
        public AccountController(IUnitOfWork _uow):base(_uow)
        {
            uow = _uow;
        }
        public ActionResult Counties(int provinceId)
        {
            var counties = uow.Counties.GetAll().Where(x => x.ProvinceID == provinceId).Select(x => new { ID = x.ID, Name = x.Name }).ToList();
            return Json(counties);
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            var mail = (ViewData["ShopEmail"]) as string;
            var shop = uow.Shops.GetByEmail(mail);

            if (shop == null) return RedirectToAction("Login", "Security");
            ViewBag.Provinces = uow.Provinces.SetProvinceDropdownList(true);
            ViewBag.Counties = uow.Counties.SetCountyDropdownList(shop.ProvinceID, true);
            ShopAccountModel shopAccountModel = new ShopAccountModel()
            {
                ShopID = shop.ID,
                ProvinceID = shop.ProvinceID,
                CountyID = shop.CountyID,
                Username = shop.Username,
                Password = shop.Password,
                Email = shop.Email,
                Phone = shop.Phone,
                Name = shop.Name,
                Surname = shop.Surname,
                Title = shop.Title,
                TaxNumber = shop.TaxNumber,
                TaxAdministration = shop.TaxAdministration,
                ShopWebsite = shop.ShopWebsite,
                BannerImagePath = shop.BannerImagePath,
                LogoPath = shop.LogoPath,
                Point = shop.Point,
                Address = shop.Address
            };
            return View(shopAccountModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [Log]
        public ActionResult UserInfo(ShopAccountModel shopEntity, string NewPassword, string VerifyPassword)
        {
            if (ModelState.IsValid)
            {
                var mail = (ViewData["ShopEmail"]) as string;
                var shop = uow.Shops.GetByEmail(mail);
                if (shop == null) return RedirectToAction("Index", "Account");

                try
                {
                    if (NewPassword != VerifyPassword)
                    {
                        TempData["ResultClass"] = AlertType.Warning;
                        TempData["Message"] = WarningMessages.PasswordsAreNotSame;
                        return RedirectToAction("Index", "Account");
                    }

                    if (string.IsNullOrEmpty(NewPassword) && string.IsNullOrEmpty(VerifyPassword))
                    {

                        shop.Name = shopEntity.Name;
                        shop.Surname = shopEntity.Surname;


                        Session["FullName"] = shopEntity.Name + " " + shopEntity.Surname;

                        uow.Shops.Edit(shop);
                        uow.SaveChanges();

                        TempData["ResultClass"] = AlertType.Success;
                        TempData["Message"] = SuccessMessages.Success;
                        return RedirectToAction("Index", "Account");
                    }
                    else
                    {
                        shop.Name = shopEntity.Name;
                        shop.Surname = shopEntity.Surname;
                        shop.Password = VerifyPassword;

                        Session["FullName"] = shopEntity.Name + " " + shopEntity.Surname;
                        Session["ShopEmail"] = shopEntity.Email;

                        uow.Shops.Edit(shop);
                        uow.SaveChanges();

                        TempData["ResultClass"] = AlertType.Success;
                        TempData["Message"] = SuccessMessages.Success;
                        return RedirectToAction("Index", "Account");
                    }
                }
                catch (Exception)
                {
                    TempData["ResultClass"] = AlertType.Danger;
                    TempData["Message"] = ExceptionMessages.UnidentifiedError;
                    return RedirectToAction("Index", "Account");
                }
            }
            TempData["ResultClass"] = AlertType.Danger;
            TempData["Message"] = ExceptionMessages.EmptyEntity;
            return RedirectToAction("Index", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [Log]
        public ActionResult ShopInfo(ShopAccountModel shopEntity)
        {
            if (ModelState.IsValid)
            {
                var mail = (ViewData["ShopEmail"]) as string;
                var shop = uow.Shops.GetByEmail(mail);

                if (shop == null) return RedirectToAction("Index", "Account");

                try
                {
                    shop.Title = shopEntity.Title;
                    shop.TaxAdministration = shopEntity.TaxAdministration;
                    shop.TaxNumber = shopEntity.TaxNumber;
                    shop.ShopWebsite = shopEntity.ShopWebsite;

                    uow.Shops.Edit(shop);
                    uow.SaveChanges();

                    TempData["ResultClass"] = AlertType.Success;
                    TempData["Message"] = SuccessMessages.Success;
                    return RedirectToAction("Index", "Account");

                }
                catch (Exception)
                {
                    TempData["ResultClass"] = AlertType.Danger;
                    TempData["Message"] = ExceptionMessages.UnidentifiedError;
                    return RedirectToAction("Index", "Account");
                }
            }
            TempData["ResultClass"] = AlertType.Danger;
            TempData["Message"] = ExceptionMessages.EmptyEntity;
            return RedirectToAction("Index", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [Log]
        public ActionResult ContactInfo(ShopAccountModel shopEntity)
        {
            if (ModelState.IsValid)
            {
                var mail = (ViewData["ShopEmail"]) as string;
                var shop = uow.Shops.GetByEmail(mail);

                if (shop == null) return RedirectToAction("Index", "Account");

                try
                {
                    shop.ProvinceID = shopEntity.ProvinceID;
                    shop.CountyID = shopEntity.CountyID;
                    shop.Address = shopEntity.Address;
                    shop.Email = shopEntity.Email;
                    shop.Phone = shopEntity.Phone;

                    Session["ShopEmail"] = shopEntity.Email;

                    uow.Shops.Edit(shop);
                    uow.SaveChanges();

                    TempData["ResultClass"] = AlertType.Success;
                    TempData["Message"] = SuccessMessages.Success;
                    return RedirectToAction("Index", "Account");
                }
                catch (Exception)
                {
                    TempData["ResultClass"] = AlertType.Danger;
                    TempData["Message"] = ExceptionMessages.UnidentifiedError;
                    return RedirectToAction("Index", "Account");
                }
            }
            TempData["ResultClass"] = AlertType.Danger;
            TempData["Message"] = ExceptionMessages.EmptyEntity;
            return RedirectToAction("Index", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [Log]
        public ActionResult ImageInfo(ShopAccountModel shopEntity, IEnumerable<HttpPostedFileBase> upload, IEnumerable<HttpPostedFileBase> upload2)
        {
            if (ModelState.IsValid)
            {
                var mail = (ViewData["ShopEmail"]) as string;
                var shop = uow.Shops.GetByEmail(mail);

                if (shop == null) return RedirectToAction("Index", "Account");

                try
                {
                    if (upload != null)
                    {
                        foreach (var file in upload)
                        {
                            if (file != null && file.ContentLength > 0)
                            {
                                var fileuploadmodel = SetUploadFileName(file.FileName, Path.GetExtension(file.FileName), UploadStrings.ShopBannerFilePath);
                                file.SaveAs(Server.MapPath("~" + fileuploadmodel.Path));

                                shop.BannerImagePath = fileuploadmodel.Path;

                                uow.Shops.Edit(shop);
                                uow.SaveChanges();

                                TempData["ResultClass"] = AlertType.Success;
                                TempData["Message"] = SuccessMessages.Success;
                                return RedirectToAction("Index", "Account");
                            }
                        }
                    }
                    if (upload2 != null)
                    {
                        foreach (var file in upload2)
                        {
                            if (file != null && file.ContentLength > 0)
                            {
                                var fileuploadmodel = SetUploadFileName(file.FileName, Path.GetExtension(file.FileName), UploadStrings.ShopLogoFilePath);
                                file.SaveAs(Server.MapPath("~" + fileuploadmodel.Path));

                                shop.LogoPath = fileuploadmodel.Path;

                                uow.Shops.Edit(shop);
                                uow.SaveChanges();

                                TempData["ResultClass"] = AlertType.Success;
                                TempData["Message"] = SuccessMessages.Success;
                                return RedirectToAction("Index", "Account");
                            }
                        }
                    }
                    return RedirectToAction("Index", "Account");
                }
                catch (Exception)
                {
                    TempData["ResultClass"] = AlertType.Danger;
                    TempData["Message"] = ExceptionMessages.UnidentifiedError;
                    return RedirectToAction("Index", "Account");
                }
            }
            TempData["ResultClass"] = AlertType.Danger;
            TempData["Message"] = ExceptionMessages.EmptyEntity;
            return RedirectToAction("Index", "Account");
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
                        ViewData["ShopEmail"] = shop.Email;
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