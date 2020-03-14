using ShopManagementSystem.WebUI.Controllers;
using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Extensions.Log;
using ShopManagementSystem.WebUI.Extensions.String;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class WarrantyPeriodController : BaseController
    {
        private readonly IUnitOfWork uow;

        public WarrantyPeriodController(IUnitOfWork _uow) : base(_uow)
        {
            uow = _uow;
        }
        // GET: Admin/WarrantyPeriod
        public ActionResult Index() => View(uow.WarrantyPeriods.GetAll().ToList());

        [Log]
        public ActionResult Remove(int? id)
        {
            if (id == null) return RedirectToAction("Index", "WarrantyPeriod");
            try
            {
                var warrantyperiod = uow.WarrantyPeriods.Get(Convert.ToInt32(id));
                if (warrantyperiod != null)
                {
                    if (uow.WarrantyPeriods.CheckRelatedRecords(warrantyperiod.ID))
                    {
                        TempData["ResultClass"] = AlertType.Warning;
                        TempData["Message"] = string.Format(WarningMessages.HasRelatedRecord, "Ürünler", "bu garanti süresini kulllanan kayıtlar");
                        return RedirectToAction("Index", "WarrantyPeriod");
                    }

                    uow.WarrantyPeriods.Delete(warrantyperiod);
                    uow.SaveChanges();

                    TempData["ResultClass"] = AlertType.Success;
                    TempData["Message"] = SuccessMessages.Success;
                    return RedirectToAction("Index", "WarrantyPeriod");
                }
                else
                {
                    TempData["ResultClass"] = AlertType.Danger;
                    TempData["Message"] = ExceptionMessages.EmptyEntity;
                    return RedirectToAction("Index", "WarrantyPeriod");
                }
            }
            catch (Exception)
            {
                TempData["ResultClass"] = AlertType.Danger;
                TempData["Message"] = ExceptionMessages.UnidentifiedError;
                return RedirectToAction("Index", "WarrantyPeriod");
            }
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Log]
        public ActionResult Add(WarrantyPeriods entity)
        {
            if (ModelState.IsValid)
            {
                if (entity != null)
                {
                    if (uow.WarrantyPeriods.HasSameRecords(entity.Name))
                    {
                        TempData["ResultClass"] = AlertType.Warning;
                        TempData["Message"] = String.Format(WarningMessages.HasSameRecord, "<b>Garanti Süresi</b>");
                        return View(entity);
                    }
                    try
                    {
                        uow.WarrantyPeriods.Add(entity);
                        uow.SaveChanges();
                        TempData["ResultClass"] = AlertType.Success;
                        TempData["Message"] = SuccessMessages.Success;
                        return RedirectToAction("Index", "WarrantyPeriod");
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
            if (id == null) return RedirectToAction("Index", "WarrantyPeriod");

            var _warrantyperiods = uow.WarrantyPeriods.Get(Convert.ToInt32(id));

            if (_warrantyperiods != null)
            {
                return View(_warrantyperiods);
            }

            else return RedirectToAction("Index", "WarrantyPeriod");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Log]
        public ActionResult Edit(WarrantyPeriods entity)
        {
            if (ModelState.IsValid)
            {
                if (entity != null)
                {
                    try
                    {
                        uow.WarrantyPeriods.Edit(entity);
                        uow.SaveChanges();
                        TempData["ResultClass"] = AlertType.Success;
                        TempData["Message"] = SuccessMessages.Success;
                        return RedirectToAction("Edit", "WarrantyPeriod", new { id = entity.ID });
                    }
                    catch (Exception)
                    {
                        TempData["ResultClass"] = AlertType.Danger;
                        TempData["Message"] = ExceptionMessages.UnidentifiedError;
                        return RedirectToAction("Edit", "WarrantyPeriod", new { id = entity.ID });
                    }
                }
                else
                {
                    TempData["ResultClass"] = AlertType.Danger;
                    TempData["Message"] = ExceptionMessages.EmptyEntity;
                    return RedirectToAction("Index", "WarrantyPeriod");
                }
            }
            TempData["ResultClass"] = AlertType.Danger;
            TempData["Message"] = ExceptionMessages.EmptyEntity;
            return RedirectToAction("Index", "WarrantyPeriod");
        }

        [HttpPost]
        public ActionResult RemoveWarrantyPeriodModal(int? id) => PartialView(uow.WarrantyPeriods.Get((int)id));
    }
}