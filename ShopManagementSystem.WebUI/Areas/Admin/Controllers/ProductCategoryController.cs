using ShopManagementSystem.WebUI.Areas.Admin.ViewModels;
using ShopManagementSystem.WebUI.Controllers;
using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Extensions.Log;
using ShopManagementSystem.WebUI.Extensions.String;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace ShopManagementSystem.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Shop")]
    public class ProductCategoryController : BaseController
    {
        private readonly IUnitOfWork uow;

        public ProductCategoryController(IUnitOfWork _uow) : base(_uow)
        {
            uow = _uow;
        }
        
        // GET: Admin/ProductCategory
        public ActionResult Index(int? shopId, int? productId)
        {
            if (!shopId.HasValue || !productId.HasValue)
            {
                return RedirectToAction("Index", "Product");
            }
            return (View( new ProductCategoryViewModel {
                ProductCategories = uow.ProductCategories.Find(a => a.ProductID == productId && a.ShopID == shopId).ToList(),
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
            SetViewBags();
            ViewBag.ProductID = productId;
            ViewBag.ShopID = shopId;
            return View(new ProductCategories { ShopID = (int)shopId ,ProductID=(int)productId});
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Log]
        public ActionResult Add(ProductCategories entity)
        {
            SetViewBags();
            if (ModelState.IsValid)
            {
                if (entity != null)
                {
                    if (uow.ProductCategories.HasSameRecords(entity))
                    {
                        TempData["ResultClass"] = AlertType.Warning;
                        TempData["Message"] = String.Format(WarningMessages.HasSameRecord, "<b>Ürün Adı veya Mağaza Adı</b>");
                        return RedirectToAction("Add", "ProductCategory", new { productId = entity.ProductID, shopId = entity.ShopID });
                    }
                    try
                    {                    
                        uow.ProductCategories.Add(entity);
                        uow.SaveChanges();

                        TempData["ResultClass"] = AlertType.Success;
                        TempData["Message"] = SuccessMessages.Success;
                        return RedirectToAction("Index", "ProductCategory",new { productId = entity.ProductID, shopId=entity.ShopID});
                    }
                    catch (Exception)
                    {
                        TempData["ResultClass"] = AlertType.Danger;
                        TempData["Message"] = ExceptionMessages.UnidentifiedError;
                        return RedirectToAction("Add", "ProductCategory", new { productId = entity.ProductID, shopId = entity.ShopID });
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

        [HttpPost]
        public ActionResult RemoveProductCategoryModal(int? id) => PartialView(uow.ProductCategories.Get((int)id));

        [Log]
        public ActionResult Remove(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Product");
            try
            {
                var productcategory = uow.ProductCategories.Get(Convert.ToInt32(id));
                if (productcategory == null)
                {
                    TempData["ResultClass"] = AlertType.Danger;
                    TempData["Message"] = ExceptionMessages.EmptyEntity;
                    return RedirectToAction("Index", "Product");
                }
                var ProductId = productcategory.ProductID;
                var ShopId = productcategory.ShopID;
                uow.ProductCategories.Delete(productcategory);
                uow.SaveChanges();

                TempData["ResultClass"] = AlertType.Success;
                TempData["Message"] = SuccessMessages.Success;
                return RedirectToAction("Index", "ProductCategory", new { productId = ProductId, shopId = ShopId });

            }
            catch (Exception)
            {
                TempData["ResultClass"] = AlertType.Danger;
                TempData["Message"] = ExceptionMessages.UnidentifiedError;
                return RedirectToAction("Index", "Product");
            }
        }

        protected void SetViewBags()
        {
            int shopID = (int)ViewData["ShopID"];
            var userrole = ViewData["ShopRole"] as string;
            ViewBag.Shops = (userrole == "Admin") ? uow.Shops.SetShopDropdownList() : uow.Shops.SetShopDropdownList(shopID);
            ViewBag.Products = (userrole == "Admin") ? uow.Products.SetProductDropdownList() : uow.Products.SetProductDropdownList(shopID);
            ViewBag.Categories = uow.Categories.SetCategoryDropdownList();
            ViewBag.OrderStatuses = uow.OrderStatuses.SetOrderStatusDropdownList();
        }

        protected void SetViewBags(bool isEdit)
        {
            if (isEdit)
            {
                int shopID = (int)ViewData["ShopID"];
                var userrole = ViewData["ShopRole"] as string;
                ViewBag.Shops = (userrole == "Admin") ? uow.Shops.SetShopDropdownList(true) : uow.Shops.SetShopDropdownList(true, shopID);
                ViewBag.Products = (userrole == "Admin") ? uow.Products.SetProductDropdownList(true) : uow.Products.SetProductDropdownList(true, shopID);
                ViewBag.Categories = uow.Categories.SetCategoryDropdownList(true);
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