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
using System.Web.Security;

namespace ShopManagementSystem.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Shop")]
    public class CustomerController : BaseController
    {
        private readonly IUnitOfWork uow;
        public CustomerController(IUnitOfWork _uow) : base(_uow)
        {
            uow = _uow;
        }

        // GET: Admin/Customer
        public ActionResult Index()
        {
            int shopID = (int)ViewData["ShopID"];
            return (ViewData["ShopRole"] as string == "Admin") ? View(new CustomerViewModel { Customers = uow.Customers.GetAll().ToList() }) :
            View(new CustomerViewModel { Customers = uow.Customers.Find(a => a.ShopID == shopID).ToList() });
        }
        public ActionResult Counties(int provinceId)
        {
            var counties = uow.Counties.GetAll().Where(x => x.ProvinceID == provinceId).Select(x => new { ID = x.ID, Name = x.Name }).ToList();
            return Json(counties);
        }
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Customer");

            var _customer = uow.Customers.Get(Convert.ToInt32(id));

            if (_customer != null)
            {
                return View(_customer);
            }

            else return RedirectToAction("Index", "Customer");
        }
        
        [Log]
        public ActionResult Remove(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Customer");
            try
            {
                var customer = uow.Customers.Get(Convert.ToInt32(id));
                if (customer != null)
                {
                    if (uow.Customers.CheckRelatedRecords(customer.ID))
                    {
                        TempData["ResultClass"] = AlertType.Warning;
                        TempData["Message"] = string.Format(WarningMessages.HasRelatedRecord, "Siparişler", "bu müşteriye ait kayıtlar");
                        return RedirectToAction("Index", "Customer");
                    }
                   
                    uow.Customers.Delete(customer);
                    uow.SaveChanges();

                    TempData["ResultClass"] = AlertType.Success;
                    TempData["Message"] = SuccessMessages.Success;
                    return RedirectToAction("Index", "Customer");
                }
                else
                {
                    TempData["ResultClass"] = AlertType.Danger;
                    TempData["Message"] = ExceptionMessages.EmptyEntity;
                    return RedirectToAction("Index", "Customer");
                }
            }
            catch (Exception)
            {
                TempData["ResultClass"] = AlertType.Danger;
                TempData["Message"] = ExceptionMessages.UnidentifiedError;
                return RedirectToAction("Index", "Customer");
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
        public ActionResult Add(Customers entity)
        {
            SetViewBags();
            if (ModelState.IsValid)
            {
                if (entity != null)
                {
                    if (uow.Customers.HasSameRecords(entity))
                    {
                        TempData["ResultClass"] = AlertType.Warning;
                        TempData["Message"] = String.Format(WarningMessages.HasSameRecord, "<b>Mail Adresi veya Telefon</b>");
                        return View(entity);
                    }
                    try
                    {                       
                        entity.AddedDate = DateTime.Now;
                        uow.Customers.Add(entity);
                        uow.SaveChanges();

                        TempData["ResultClass"] = AlertType.Success;
                        TempData["Message"] = SuccessMessages.Success;
                        ViewBag.ButtonIsVisible = true;
                        return RedirectToAction("Index", "Customer");
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
            if (id == null) return RedirectToAction("Index", "Customer");

            var _customer = uow.Customers.Get(Convert.ToInt32(id));

            if (_customer != null)
            {
                SetViewBags(true);
                ViewBag.Counties = uow.Counties.SetCountyDropdownList(_customer.ProvinceID, true);
                return View(_customer);
            }

            else return RedirectToAction("Index", "Customer");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Log]
        public ActionResult Edit(Customers entity)
        {
            if (ModelState.IsValid)
            {
                if (entity != null)
                {
                    try
                    {
                        uow.Customers.Edit(entity);
                        uow.SaveChanges();
                        TempData["ResultClass"] = AlertType.Success;
                        TempData["Message"] = SuccessMessages.Success;
                        ViewBag.ButtonIsVisible = true;
                        return RedirectToAction("Edit", "Customer", new { id = entity.ID });
                    }
                    catch (Exception)
                    {
                        TempData["ResultClass"] = AlertType.Danger;
                        TempData["Message"] = ExceptionMessages.UnidentifiedError;
                        return RedirectToAction("Edit", "Customer", new { id = entity.ID });
                    }
                }
                else
                {
                    TempData["ResultClass"] = AlertType.Danger;
                    TempData["Message"] = ExceptionMessages.EmptyEntity;
                    return RedirectToAction("Index", "Customer");
                }
            }
            TempData["ResultClass"] = AlertType.Danger;
            TempData["Message"] = ExceptionMessages.EmptyEntity;
            return RedirectToAction("Index", "Customer");
        }

        [HttpPost]
        public ActionResult RemoveCustomerModal(int? id) => PartialView(uow.Customers.Get((int)id));

        protected void SetViewBags()
        {
            int shopID = (int)ViewData["ShopID"];
            var userrole = ViewData["ShopRole"] as string;
            ViewBag.Shops = (userrole == "Admin") ? uow.Shops.SetShopDropdownList() : uow.Shops.SetShopDropdownList(shopID);
            ViewBag.Provinces = uow.Provinces.SetProvinceDropdownList();
        }
        protected void SetViewBags(bool isEdit)
        {
            if (isEdit)
            {
                int shopID = (int)ViewData["ShopID"];
                var userrole = ViewData["ShopRole"] as string;
                ViewBag.Shops = (userrole == "Admin") ? uow.Shops.SetShopDropdownList(true) : uow.Shops.SetShopDropdownList(true, shopID);
                ViewBag.Provinces = uow.Provinces.SetProvinceDropdownList(true);
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