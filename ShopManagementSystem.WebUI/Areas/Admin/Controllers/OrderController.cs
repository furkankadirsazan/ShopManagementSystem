using ShopManagementSystem.WebUI.Areas.Admin.ViewModels;
using ShopManagementSystem.WebUI.Controllers;
using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Extensions.Log;
using ShopManagementSystem.WebUI.Extensions.Mail.Concrete;
using ShopManagementSystem.WebUI.Extensions.String;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace ShopManagementSystem.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Shop")]
    public class OrderController : BaseController
    {
        private readonly IUnitOfWork uow;
        public OrderController(IUnitOfWork _uow) : base(_uow)
        {
            uow = _uow;
        }
        // GET: Admin/Order
        public ActionResult Index()
        {
            int shopID = (int)ViewData["ShopID"];
            return (ViewData["ShopRole"] as string == "Admin") ? View(new OrderViewModel { Orders = uow.Orders.GetAll().ToList() }) :
            View(new OrderViewModel { Orders = uow.Orders.Find(a => a.ShopID == shopID).ToList() });
        }
        public ActionResult Counties(int provinceId)
        {
            var counties = uow.Counties.GetAll().Where(x => x.ProvinceID == provinceId).Select(x => new { ID = x.ID, Name = x.Name }).ToList();
            return Json(counties);
        }
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Order");

            var _order = uow.Orders.Get(Convert.ToInt32(id));

            if (_order != null)
            {
                return View(_order);
            }

            else return RedirectToAction("Index", "Order");
        }
        [Log]
        public ActionResult ChangeDopingoStatus(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Order");
            try
            {
                var order = uow.Orders.Get(Convert.ToInt32(id));
                if (order != null)
                {
                    order.IsInDopingo = !order.IsInDopingo;
                    uow.Orders.Edit(order);
                    uow.SaveChanges();

                    TempData["ResultClass"] = AlertType.Success;
                    TempData["Message"] = SuccessMessages.Success;
                    return RedirectToAction("Index", "Order");
                }
                else
                {
                    TempData["ResultClass"] = AlertType.Danger;
                    TempData["Message"] = ExceptionMessages.EmptyEntity;
                    return RedirectToAction("Index", "Order");
                }
            }
            catch (Exception)
            {
                TempData["ResultClass"] = AlertType.Danger;
                TempData["Message"] = ExceptionMessages.UnidentifiedError;
                return RedirectToAction("Index", "Order");
            }
        }
        [Log]
        public ActionResult Remove(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Order");
            try
            {
                var order = uow.Orders.Get(Convert.ToInt32(id));
                if (order != null)
                {
                    int Number = order.Number;
                    var product = uow.Products.Get(order.ProductID);
                    product.Number += Number;

                    uow.Products.Edit(product);
                    uow.Orders.Delete(order);
                    uow.SaveChanges();

                    TempData["ResultClass"] = AlertType.Success;
                    TempData["Message"] = SuccessMessages.Success;
                    return RedirectToAction("Index", "Order");
                }
                else
                {
                    TempData["ResultClass"] = AlertType.Danger;
                    TempData["Message"] = ExceptionMessages.EmptyEntity;
                    return RedirectToAction("Index", "Order");
                }
            }
            catch (Exception)
            {
                TempData["ResultClass"] = AlertType.Danger;
                TempData["Message"] = ExceptionMessages.UnidentifiedError;
                return RedirectToAction("Index", "Order");
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Add()
        {
            SetViewBags();
            return View();
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Log]
        [Authorize(Roles = "Admin")]
        public ActionResult Add(Orders entity)
        {
            SetViewBags();
            if (ModelState.IsValid)
            {
                if (entity != null)
                {
                    try
                    {
                        int Number = entity.Number;
                        var product = uow.Products.Get(entity.ProductID);
                        product.Number -= Number;
                        double Price = Convert.ToDouble(product.Price);
                        double tax = product.TaxDescriptions.Value;
                        double PriceWithTax = (Price * (1 + tax));
                        entity.IsInDopingo = false;
                        entity.OrderDate = DateTime.Now;
                        entity.UpdateDate = DateTime.Now;
                        entity.TotalPrice = Number * PriceWithTax;

                        uow.Orders.Add(entity);                     
                        uow.Products.Edit(product);
                        uow.SaveChanges();

                        //Mağazaya bilgilendirme maili gönder
                        Email email = new Email();
                        email.SendForNewOrder(product.Shops.Email, ToTitleCase(product.Shops.Title));
                        

                        TempData["ResultClass"] = AlertType.Success;
                        TempData["Message"] = SuccessMessages.Success;
                        return RedirectToAction("Index", "Order");
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
            if (id == null) return RedirectToAction("Index", "Order");

            var _order = uow.Orders.Get(Convert.ToInt32(id));

            if (_order != null)
            {
                SetViewBags(true);
                return View(_order);
            }

            else return RedirectToAction("Index", "Order");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Log]
        public ActionResult Edit(Orders entity)
        {
            if (ModelState.IsValid)
            {
                if (entity != null)
                {
                    try
                    {
                        entity.UpdateDate = DateTime.Now;
                        uow.Orders.Edit(entity);
                        uow.SaveChanges();
                        TempData["ResultClass"] = AlertType.Success;
                        TempData["Message"] = SuccessMessages.Success;
                        return RedirectToAction("Edit", "Order", new { id = entity.ID });
                    }
                    catch (Exception)
                    {
                        TempData["ResultClass"] = AlertType.Danger;
                        TempData["Message"] = ExceptionMessages.UnidentifiedError;
                        return RedirectToAction("Edit", "Order", new { id = entity.ID });
                    }
                }
                else
                {
                    TempData["ResultClass"] = AlertType.Danger;
                    TempData["Message"] = ExceptionMessages.EmptyEntity;
                    return RedirectToAction("Index", "Order");
                }
            }
            TempData["ResultClass"] = AlertType.Danger;
            TempData["Message"] = ExceptionMessages.EmptyEntity;
            return RedirectToAction("Index", "Order");
        }

        [HttpPost]
        public ActionResult RemoveOrderModal(int? id) => PartialView(uow.Orders.Get((int)id));

        protected void SetViewBags()
        {
            int shopID = (int)ViewData["ShopID"];
            var userrole = ViewData["ShopRole"] as string;
            ViewBag.Shops = (userrole == "Admin") ? uow.Shops.SetShopDropdownList() : uow.Shops.SetShopDropdownList(shopID);
            ViewBag.Products = (userrole == "Admin") ? uow.Products.SetProductDropdownList() : uow.Products.SetProductDropdownList(shopID);
            ViewBag.Customers = (userrole == "Admin") ? uow.Customers.SetCustomerDropdownList(true) : uow.Customers.SetCustomerDropdownList(true,shopID);
            ViewBag.OrderStatuses = uow.OrderStatuses.SetOrderStatusDropdownList();
        }
        protected void SetViewBags(bool isEdit)
        {
            if (isEdit)
            {
                int shopID = (int)ViewData["ShopID"];
                var userrole = ViewData["ShopRole"] as string;
                ViewBag.Shops = (userrole == "Admin") ? uow.Shops.SetShopDropdownList(true) : uow.Shops.SetShopDropdownList(true,shopID);
                ViewBag.Products = (userrole == "Admin") ? uow.Products.SetProductDropdownList(true) : uow.Products.SetProductDropdownList(true,shopID);
                ViewBag.Customers = (userrole == "Admin") ? uow.Customers.SetCustomerDropdownList(true) : uow.Customers.SetCustomerDropdownList(true, shopID);
                ViewBag.OrderStatuses = uow.OrderStatuses.SetOrderStatusDropdownList();
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