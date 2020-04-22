using ShopManagementSystem.WebUI.Areas.Admin.ViewModels;
using ShopManagementSystem.WebUI.Controllers;
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

namespace ShopManagementSystem.WebUI.Areas.Admin.Controllers
{
    public class ProductGalleryController : BaseController
    {
        private readonly IUnitOfWork uow;

        public ProductGalleryController(IUnitOfWork _uow) : base(_uow)
        {
            uow = _uow;
        }
        // GET: Admin/ProductGallery
        public ActionResult Index(int? shopId, int? productId)
        {
            if (!shopId.HasValue || !productId.HasValue)
            {
                return RedirectToAction("Index", "Product");
            }
            return (View(new ProductGalleryViewModel
            {
                ProductGallery = uow.ProductGalleries.Find(a => a.ProductID == productId && a.ShopID == shopId).ToList(),
                Product = uow.Products.Get((int)productId),
                Shop = uow.Shops.Get((int)shopId)
            }));
        }

        public ActionResult Add(int? shopId, int? productId)
        {
            if (!shopId.HasValue || !productId.HasValue)
            {
                return RedirectToAction("Index", "Product");
            }
            ViewBag.ProductID = productId;
            ViewBag.ShopID = shopId;
            return View(new ProductGallery { ShopID = (int)shopId, ProductID = (int)productId });
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Log]
        public ActionResult Add(IEnumerable<HttpPostedFileBase> Images, ProductGallery entity)
        {

            if (ModelState.IsValid)
            {
                if (entity != null && Images != null)
                {
                    try
                    {
                        foreach (var item in Images)
                        {
                            var fileuploadmodel = SetUploadFileName(item.FileName, Path.GetExtension(item.FileName), UploadStrings.ProductGalleryFilePath);
                            item.SaveAs(Server.MapPath("~" + fileuploadmodel.Path));
                            uow.ProductGalleries.Add(
                                new ProductGallery
                                {
                                    ShopID = entity.ShopID,
                                    ProductID = entity.ProductID,
                                    ImagePath = fileuploadmodel.Path,
                                    FileName = item.FileName
                                }
                            );
                            uow.SaveChanges();
                        }
                        TempData["ResultClass"] = AlertType.Success;
                        TempData["Message"] = SuccessMessages.Success;
                        return RedirectToAction("Index", "ProductGallery", new { productId = entity.ProductID, shopId = entity.ShopID });
                    }
                    catch (Exception)
                    {
                        TempData["ResultClass"] = AlertType.Danger;
                        TempData["Message"] = ExceptionMessages.UnidentifiedError;
                        return RedirectToAction("Add", "ProductGallery", new { productId = entity.ProductID, shopId = entity.ShopID });
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

        [HttpPost]
        public ActionResult RemoveProductGalleryModal(int? id) => PartialView(uow.ProductGalleries.Get((int)id));

        [Log]
        public ActionResult Remove(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Product");
            try
            {
                var productgallery = uow.ProductGalleries.Get(Convert.ToInt32(id));
                if (productgallery == null)
                {
                    TempData["ResultClass"] = AlertType.Danger;
                    TempData["Message"] = ExceptionMessages.EmptyEntity;
                    return RedirectToAction("Index", "Product");
                }
                var ProductId = productgallery.ProductID;
                var ShopId = productgallery.ShopID;

                if (System.IO.File.Exists(Server.MapPath("~" + productgallery.ImagePath)))
                {
                    System.IO.File.Delete(Server.MapPath("~" + productgallery.ImagePath));
                }
                uow.ProductGalleries.Delete(productgallery);
                uow.SaveChanges();

                TempData["ResultClass"] = AlertType.Success;
                TempData["Message"] = SuccessMessages.Success;
                return RedirectToAction("Index", "ProductGallery", new { productId = ProductId, shopId = ShopId });

            }
            catch (Exception)
            {
                TempData["ResultClass"] = AlertType.Danger;
                TempData["Message"] = ExceptionMessages.UnidentifiedError;
                return RedirectToAction("Index", "Product");
            }
        }

    }
}