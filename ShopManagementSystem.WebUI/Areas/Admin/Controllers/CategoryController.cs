using ShopManagementSystem.WebUI.Controllers;
using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Extensions.Log;
using ShopManagementSystem.WebUI.Extensions.String;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : BaseController
    {
        private readonly IUnitOfWork uow;
        public CategoryController(IUnitOfWork _uow) : base(_uow)
        {
            uow = _uow;
        }
        // GET: Admin/Category
        public ActionResult Index() => View(uow.Categories.GetAll().ToList());

        [Log]
        public ActionResult Remove(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Category");
            try
            {
                var categories = uow.Categories.Get(Convert.ToInt32(id));
                if (categories != null)
                {
                    if (uow.Categories.CheckRelatedRecords(categories.ID))
                    {
                        TempData["ResultClass"] = AlertType.Warning;
                        TempData["Message"] = string.Format(WarningMessages.HasRelatedRecord, "Ürün Kategori", "bu kategoriye ait kayıtlar");
                        return RedirectToAction("Index", "Category");
                    }

                    uow.Categories.Delete(categories);
                    uow.SaveChanges();

                    TempData["ResultClass"] = AlertType.Success;
                    TempData["Message"] = SuccessMessages.Success;
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    TempData["ResultClass"] = AlertType.Danger;
                    TempData["Message"] = ExceptionMessages.EmptyEntity;
                    return RedirectToAction("Index", "Category");
                }
            }
            catch (Exception)
            {
                TempData["ResultClass"] = AlertType.Danger;
                TempData["Message"] = ExceptionMessages.UnidentifiedError;
                return RedirectToAction("Index", "Category");
            }
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Log]
        public ActionResult Add(Categories entity)
        {
            if (ModelState.IsValid)
            {
                if (entity != null)
                {
                    try
                    {
                        if (!uow.Categories.HasSameRecords(entity))
                        {
                            uow.Categories.Add(entity);
                            uow.SaveChanges();
                            TempData["ResultClass"] = AlertType.Success;
                            TempData["Message"] = SuccessMessages.Success;
                            return RedirectToAction("Index", "Category");
                        }
                        else
                        {
                            TempData["ResultClass"] = AlertType.Warning;
                            TempData["Message"] = String.Format(WarningMessages.HasSameRecord, "<b>Kategori Adı</b>");
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
            if (id == null) return RedirectToAction("Index", "Category");
            var _category = uow.Categories.Get(Convert.ToInt32(id));

            if (_category != null)
            {
                return View(_category);
            }

            else return RedirectToAction("Index", "Category");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Log]
        public ActionResult Edit(Categories entity)
        {
            if (ModelState.IsValid)
            {
                if (entity != null)
                {
                    try
                    {
                        uow.Categories.Edit(entity);
                        uow.SaveChanges();
                        TempData["ResultClass"] = AlertType.Success;
                        TempData["Message"] = SuccessMessages.Success;
                        return RedirectToAction("Edit", "Category", new { id = entity.ID });
                    }
                    catch (Exception)
                    {
                        TempData["ResultClass"] = AlertType.Danger;
                        TempData["Message"] = ExceptionMessages.UnidentifiedError;
                        return RedirectToAction("Edit", "Category", new { id = entity.ID });
                    }
                }
                else
                {
                    TempData["ResultClass"] = AlertType.Danger;
                    TempData["Message"] = ExceptionMessages.EmptyEntity;
                    return RedirectToAction("Index", "Category");
                }
            }
            TempData["ResultClass"] = AlertType.Danger;
            TempData["Message"] = ExceptionMessages.EmptyEntity;
            return RedirectToAction("Index", "Category");
        }

        [HttpPost]
        public ActionResult RemoveCategoryModal(int? id) => PartialView(uow.Categories.Get((int)id));

    }
}