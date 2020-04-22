﻿using ShopManagementSystem.WebUI.Controllers;
using ShopManagementSystem.WebUI.Entity;
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
    [Authorize(Roles = "Admin,Shop")]
    public class ProductController : BaseController
    {
        private readonly IUnitOfWork uow;
        public ProductController(IUnitOfWork _uow) : base(_uow)
        {
            uow = _uow;
        }
        // GET: Admin/Product
        public ActionResult Index()
        {
            int shopID = (int)ViewData["ShopID"];
            return (ViewData["ShopRole"] as string == "Admin") ? View(uow.Products.GetAll().ToList()) :
            View(uow.Products.Find(a => a.ShopID == shopID).ToList());
        }
        [Log]
        public ActionResult ChangeDopingoStatus(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Product");
            try
            {
                var product = uow.Products.Get(Convert.ToInt32(id));
                if (product != null)
                {
                    product.IsInDopingo = !product.IsInDopingo;
                    uow.Products.Edit(product);
                    uow.SaveChanges();

                    TempData["ResultClass"] = AlertType.Success;
                    TempData["Message"] = SuccessMessages.Success;
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    TempData["ResultClass"] = AlertType.Danger;
                    TempData["Message"] = ExceptionMessages.EmptyEntity;
                    return RedirectToAction("Index", "Product");
                }
            }
            catch (Exception)
            {
                TempData["ResultClass"] = AlertType.Danger;
                TempData["Message"] = ExceptionMessages.UnidentifiedError;
                return RedirectToAction("Index", "Product");
            }
        }

        [Log]
        public ActionResult Remove(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Product");
            try
            {
                var product = uow.Products.Get(Convert.ToInt32(id));
                if (product != null)
                {
                    if (uow.Products.CheckRelatedRecords(product.ID))
                    {
                        TempData["ResultClass"] = AlertType.Warning;
                        TempData["Message"] = string.Format(WarningMessages.HasRelatedRecord, "Sipariş veya Ürün Galerisi", "bu ürüne ait kayıtlar");
                        return RedirectToAction("Index", "Product");
                    }

                    uow.Products.Delete(product);
                    uow.SaveChanges();

                    TempData["ResultClass"] = AlertType.Success;
                    TempData["Message"] = SuccessMessages.Success;
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    TempData["ResultClass"] = AlertType.Danger;
                    TempData["Message"] = ExceptionMessages.EmptyEntity;
                    return RedirectToAction("Index", "Product");
                }
            }
            catch (Exception)
            {
                TempData["ResultClass"] = AlertType.Danger;
                TempData["Message"] = ExceptionMessages.UnidentifiedError;
                return RedirectToAction("Index", "Product");
            }
        }

        public ActionResult Add()
        {
            SetViewBags();
            return View();
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Log]
        public ActionResult Add(Products entity,IEnumerable<HttpPostedFileBase> upload)
        {
            SetViewBags();
            if (ModelState.IsValid)
            {
                if (entity != null)
                {
                    if (uow.Products.HasSameRecords(entity))
                    {
                        TempData["ResultClass"] = AlertType.Warning;
                        TempData["Message"] = String.Format(WarningMessages.HasSameRecord, "<b>Ürün Adı</b>");
                        return View(entity);
                    }
                    try
                    {
                        if (upload != null)
                        {
                            foreach (var file in upload)
                            {
                                if (file != null && file.ContentLength > 0)
                                {
                                    var fileuploadmodel = SetUploadFileName(file.FileName, Path.GetExtension(file.FileName), UploadStrings.ProductImageFilePath);
                                    file.SaveAs(Server.MapPath("~"+fileuploadmodel.Path));

                                    entity.IsInDopingo = false;
                                    entity.ImagePath = fileuploadmodel.Path;
                                    entity.ThumbNailImagePath = fileuploadmodel.Path;
                                    entity.FileName = fileuploadmodel.FileName;
                                
                                    uow.Products.Add(entity);
                                    uow.SaveChanges();

                                    TempData["ResultClass"] = AlertType.Success;
                                    TempData["Message"] = SuccessMessages.Success;
                                    return RedirectToAction("Index", "Product");
                                }
                                else
                                {
                                    TempData["ResultClass"] = AlertType.Warning;
                                    TempData["Message"] = string.Format(WarningMessages.EmptyImpassable,"Fotoğraf");
                                    return View();
                                }
                            }
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
            TempData["ResultClass"] = AlertType.Danger;
            TempData["Message"] = ExceptionMessages.EmptyEntity;
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Product");

            var _product = uow.Products.Get(Convert.ToInt32(id));

            if (_product != null)
            {
                SetViewBags(true);
                return View(_product);
            }

            else return RedirectToAction("Index", "Product");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Log]
        public ActionResult Edit(Products entity, IEnumerable<HttpPostedFileBase> upload)
        {
            if (ModelState.IsValid)
            {
                if (entity != null)
                {
                    try
                    {
                        if (upload != null)
                        {
                            foreach (var file in upload)
                            {
                                if (file != null && file.ContentLength > 0)
                                {
                                    var fileuploadmodel = SetUploadFileName(file.FileName, Path.GetExtension(file.FileName), UploadStrings.ProductImageFilePath);
                                    file.SaveAs(Server.MapPath("~" + fileuploadmodel.Path));

                                    entity.ImagePath = fileuploadmodel.Path;
                                    entity.ThumbNailImagePath = fileuploadmodel.Path;
                                    entity.FileName = fileuploadmodel.FileName;

                                    uow.Products.Edit(entity);
                                    uow.SaveChanges();

                                    TempData["ResultClass"] = AlertType.Success;
                                    TempData["Message"] = SuccessMessages.Success;
                                    return RedirectToAction("Edit", "Product", new { id = entity.ID });
                                }
                                else
                                {
                                    var tmpproduct = uow.Products.Find(a => a.ID == entity.ID).FirstOrDefault();
                                    entity.ImagePath = tmpproduct.ImagePath;
                                    entity.ThumbNailImagePath = tmpproduct.ThumbNailImagePath;
                                    entity.FileName = tmpproduct.FileName;
                                    uow.SaveChanges();
                                    TempData["ResultClass"] = AlertType.Success;
                                    TempData["Message"] = SuccessMessages.Success;
                                    return RedirectToAction("Edit", "Product", new { id = entity.ID });
                                }
                            }
                        }                                    
                    }
                    catch (Exception)
                    {
                        TempData["ResultClass"] = AlertType.Danger;
                        TempData["Message"] = ExceptionMessages.UnidentifiedError;
                        return RedirectToAction("Edit", "Product", new { id = entity.ID });
                    }
                }
                else
                {
                    TempData["ResultClass"] = AlertType.Danger;
                    TempData["Message"] = ExceptionMessages.EmptyEntity;
                    return RedirectToAction("Index", "Product");
                }
            }
            TempData["ResultClass"] = AlertType.Danger;
            TempData["Message"] = ExceptionMessages.EmptyEntity;
            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Product");

            var _product = uow.Products.Get(Convert.ToInt32(id));

            if (_product != null)
            {
                return View(_product);
            }

            else return RedirectToAction("Index", "Product");
        }
        [HttpPost]
        public ActionResult RemoveProductModal(int? id) => PartialView(uow.Products.Get((int)id));
        protected void SetViewBags() {
            int shopID = (int)ViewData["ShopID"];
            var userrole = ViewData["ShopRole"] as string;
            ViewBag.Shops = (userrole == "Admin") ? uow.Shops.SetShopDropdownList() : uow.Shops.SetShopDropdownList(shopID);
            ViewBag.TaxDescriptions = uow.TaxDescriptions.SetTaxDescriptionDropdownList();
            ViewBag.OutOfStockStatuses = uow.OutOfStockStatuses.SetOutOfStockDropdownList();
            ViewBag.WarrantyPeriods = uow.WarrantyPeriods.SetWarrantyPeriodDropdownList();
        }
        protected void SetViewBags(bool isEdit)
        {
            if (isEdit)
            {
                int shopID = (int)ViewData["ShopID"];
                ViewBag.Shops = (ViewData["UserRole"] as string == "Admin") ? uow.Shops.SetShopDropdownList(true) : uow.Shops.SetShopDropdownList(true,shopID);
                ViewBag.TaxDescriptions = uow.TaxDescriptions.SetTaxDescriptionDropdownList(true);
                ViewBag.OutOfStockStatuses = uow.OutOfStockStatuses.SetOutOfStockDropdownList(true);
                ViewBag.WarrantyPeriods = uow.WarrantyPeriods.SetWarrantyPeriodDropdownList(true);
            }
            else
            {
                SetViewBags();
            }           
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