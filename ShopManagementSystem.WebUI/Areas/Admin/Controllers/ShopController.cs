using ShopManagementSystem.WebUI.Areas.Admin.ViewModels;
using ShopManagementSystem.WebUI.Controllers;
using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Extensions.Log;
using ShopManagementSystem.WebUI.Extensions.String;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ShopController : BaseController
    {
        private readonly IUnitOfWork uow;
        public ShopController(IUnitOfWork _uow) : base(_uow)
        {
            uow = _uow;
        }
        // GET: Admin/Shop
        public ActionResult Index() => View(new ShopViewModel { Shops = uow.Shops.GetAll().ToList()});
        public ActionResult Counties(int provinceId)
        {
            var counties = uow.Counties.GetAll().Where(x => x.ProvinceID == provinceId).Select(x => new { ID = x.ID, Name = x.Name }).ToList();
            return Json(counties);
        }


        [Log]
        public ActionResult Remove(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Shop");
            try
            {
                var shop = uow.Shops.Get(Convert.ToInt32(id));
                if (shop != null)
                {
                    if (uow.Shops.CheckRelatedRecords(shop.ID))
                    {
                        TempData["ResultClass"] = AlertType.Warning;
                        TempData["Message"] = string.Format(WarningMessages.HasRelatedRecord,"Ürünler veya Ürün Galerisi","bu mağazaya ait kayıtlar");
                        return RedirectToAction("Index", "Shop");
                    }

                    uow.Shops.Delete(shop);
                    uow.SaveChanges();

                    TempData["ResultClass"] = AlertType.Success;
                    TempData["Message"] = SuccessMessages.Success;
                    return RedirectToAction("Index", "Shop");
                }
                else
                {
                    TempData["ResultClass"] = AlertType.Danger;
                    TempData["Message"] = ExceptionMessages.EmptyEntity;
                    return RedirectToAction("Index", "Shop");
                }
            }
            catch (Exception)
            {
                TempData["ResultClass"] = AlertType.Danger;
                TempData["Message"] = ExceptionMessages.UnidentifiedError;
                return RedirectToAction("Index", "Shop");
            }
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Shop");

            var _shop = uow.Shops.Get(Convert.ToInt32(id));

            if (_shop != null)
            {
                return View(_shop);
            }

            else return RedirectToAction("Index", "Shop");
        }


        public ActionResult Add()
        {
            ViewBag.Provinces = uow.Provinces.SetProvinceDropdownList();
            ViewBag.Roles = uow.Roles.SetRoleDropdownList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Log]
        public ActionResult Add(Shops entity)
        {
            ViewBag.Provinces = uow.Provinces.SetProvinceDropdownList();
            ViewBag.Roles = uow.Roles.SetRoleDropdownList();
            if (ModelState.IsValid)
            {
                if (entity != null)
                {
                    try
                    {
                        if (!uow.Shops.HasSameRecords(entity))
                        {
                            entity.IsRemember = false;
                            entity.AuthenticationCode = null;
                            entity.BannerImagePath = UploadStrings.ShopDefaultBannerFilePath;
                            entity.LogoPath = UploadStrings.ShopDefaultLogoFilePath;
                            entity.RegistrationDate = DateTime.Now;
                            uow.Shops.Add(entity);
                            uow.SaveChanges();
                            TempData["ResultClass"] = AlertType.Success;
                            TempData["Message"] = SuccessMessages.Success;
                            return RedirectToAction("Index", "Shop");
                        }
                        else
                        {
                            TempData["ResultClass"] = AlertType.Warning;
                            TempData["Message"] = String.Format(WarningMessages.HasSameRecord,"<b>Kullanıcı adı, Email veya Vergi No</b>");
                            return View(entity);
                        }
                    }
                    catch (Exception)
                    {
                        TempData["ResultClass"] = AlertType.Danger;
                        TempData["Message"] = ExceptionMessages.UnidentifiedError;
                        return View(entity);
                    }
                }
                else
                {
                    TempData["ResultClass"] = AlertType.Danger;
                    TempData["Message"] = ExceptionMessages.EmptyEntity;
                    return View();
                }
            }
            return View();

        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Shop");

            ViewBag.Provinces = uow.Provinces.SetProvinceDropdownList();
            ViewBag.Roles = uow.Roles.SetRoleDropdownList();
         
            var _shop = uow.Shops.Get(Convert.ToInt32(id));

            if (_shop != null)
            {
                ViewBag.Counties = uow.Counties.SetCountyDropdownList(_shop.ProvinceID,true);
                return View(_shop);
            } 

            else return RedirectToAction("Index", "Shop");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Log]
        public ActionResult Edit(Shops entity)
        {
            ViewBag.Provinces = uow.Provinces.SetProvinceDropdownList();
            ViewBag.Roles = uow.Roles.SetRoleDropdownList();

            if (ModelState.IsValid)
            {
                if (entity != null)
                {
                    try
                    {
                        uow.Shops.Edit(entity);
                        uow.SaveChanges();
                        TempData["ResultClass"] = AlertType.Success;
                        TempData["Message"] = SuccessMessages.Success;
                        return RedirectToAction("Edit", "Shop", new { id = entity.ID });
                    }
                    catch (Exception)
                    {
                        TempData["ResultClass"] = AlertType.Danger;
                        TempData["Message"] = ExceptionMessages.UnidentifiedError;
                        return RedirectToAction("Edit", "Shop", new { id = entity.ID });
                    }
                }
                else
                {
                    TempData["ResultClass"] = AlertType.Danger;
                    TempData["Message"] = ExceptionMessages.EmptyEntity;
                    return RedirectToAction("Index", "Shop");
                }
            }
            TempData["ResultClass"] = AlertType.Danger;
            TempData["Message"] = ExceptionMessages.EmptyEntity;
            return RedirectToAction("Index", "Shop");
        }

        [HttpPost]
        public ActionResult RemoveShopModal(int? id) => PartialView(uow.Shops.Get((int)id));

    }
}