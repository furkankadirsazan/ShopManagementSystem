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
    public class OutOfStockStatusController :  BaseController
    {
        private readonly IUnitOfWork uow;

        public OutOfStockStatusController(IUnitOfWork _uow) : base(_uow)
        {
            uow = _uow;
        }
        
        // GET: Admin/OutOfStockStatus
        public ActionResult Index() => View(uow.OutOfStockStatuses.GetAll().ToList());

        [Log]
        public ActionResult Remove(int? id)
        {
            if (id == null) return RedirectToAction("Index", "OutOfStockStatus");
            try
            {
                var outofstockstatus = uow.OutOfStockStatuses.Get(Convert.ToInt32(id));
                if (outofstockstatus != null)
                {
                    if (uow.OutOfStockStatuses.CheckRelatedRecords(outofstockstatus.ID))
                    {
                        TempData["ResultClass"] = AlertType.Warning;
                        TempData["Message"] = string.Format(WarningMessages.HasRelatedRecord, "Ürünler", "bu stok dışı duruma ait kayıtlar");
                        return RedirectToAction("Index", "OutOfStockStatus");
                    }

                    uow.OutOfStockStatuses.Delete(outofstockstatus);
                    uow.SaveChanges();

                    TempData["ResultClass"] = AlertType.Success;
                    TempData["Message"] = SuccessMessages.Success;
                    return RedirectToAction("Index", "OutOfStockStatus");
                }
                else
                {
                    TempData["ResultClass"] = AlertType.Danger;
                    TempData["Message"] = ExceptionMessages.EmptyEntity;
                    return RedirectToAction("Index", "OutOfStockStatus");
                }
            }
            catch (Exception)
            {
                TempData["ResultClass"] = AlertType.Danger;
                TempData["Message"] = ExceptionMessages.UnidentifiedError;
                return RedirectToAction("Index", "OutOfStockStatus");
            }
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Log]
        public ActionResult Add(OutOfStockStatuses entity)
        {
            if (ModelState.IsValid)
            {
                if (entity != null)
                {
                    if (uow.OutOfStockStatuses.HasSameRecords(entity.Name))
                    {
                        TempData["ResultClass"] = AlertType.Warning;
                        TempData["Message"] = String.Format(WarningMessages.HasSameRecord, "<b>Stok Dışı Durum Adı</b>");
                        return View(entity);
                    }
                    try
                    {                   
                        uow.OutOfStockStatuses.Add(entity);
                        uow.SaveChanges();
                        TempData["ResultClass"] = AlertType.Success;
                        TempData["Message"] = SuccessMessages.Success;
                        return RedirectToAction("Index", "OutOfStockStatus");
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
            if (id == null) return RedirectToAction("Index", "OutOfStockStatus");

            var _outofstockstatus = uow.OutOfStockStatuses.Get(Convert.ToInt32(id));

            if (_outofstockstatus != null)
            {
                return View(_outofstockstatus);
            }

            else return RedirectToAction("Index", "OutOfStockStatus");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Log]
        public ActionResult Edit(OutOfStockStatuses entity)
        {
            if (ModelState.IsValid)
            {
                if (entity != null)
                {
                    try
                    {
                        uow.OutOfStockStatuses.Edit(entity);
                        uow.SaveChanges();
                        TempData["ResultClass"] = AlertType.Success;
                        TempData["Message"] = SuccessMessages.Success;
                        return RedirectToAction("Edit", "OutOfStockStatus", new { id = entity.ID });
                    }
                    catch (Exception)
                    {
                        TempData["ResultClass"] = AlertType.Danger;
                        TempData["Message"] = ExceptionMessages.UnidentifiedError;
                        return RedirectToAction("Edit", "OutOfStockStatus", new { id = entity.ID });
                    }
                }
                else
                {
                    TempData["ResultClass"] = AlertType.Danger;
                    TempData["Message"] = ExceptionMessages.EmptyEntity;
                    return RedirectToAction("Index", "OutOfStockStatus");
                }
            }
            TempData["ResultClass"] = AlertType.Danger;
            TempData["Message"] = ExceptionMessages.EmptyEntity;
            return RedirectToAction("Index", "OutOfStockStatus");
        }

        [HttpPost]
        public ActionResult RemoveOutOfStockStatusModal(int? id) => PartialView(uow.OutOfStockStatuses.Get((int)id));
    }
}