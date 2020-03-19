using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System.Linq;
using System.Web.Security;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ShopManagementSystem.WebUI.Models;

namespace ShopManagementSystem.WebUI.Repository.Concrete.EntityFramework
{
    public class EfShopRepository:EfGenericRepository<Shops>,IShopRepository
    {
        public EfShopRepository(ShopManagementSystemEntities db) : base(db)
        {

        }
        public ShopManagementSystemEntities _db
        {
            get { return db; }
        }
        public bool CheckRelatedRecords(int id) => (db.Products.Where(a => a.ShopID == id).Any() || db.ProductGallery.Where(a=>a.ShopID == id).Any());
        public string CreateAuthenticationCode() => Membership.GeneratePassword(6, 1);
        public Shops GetByEmail(string email) => db.Shops.Where(u => u.Email == email).FirstOrDefault();
        public Shops GetByUsername(string username) => db.Shops.Where(u => u.Username == username).FirstOrDefault();
        public Shops GetByUsernameAndPassword(string username, string password) => db.Shops.Where(u => u.Username == username && u.Password == password).FirstOrDefault();
        public bool HasSameRecords(Shops entity) => db.Shops.Where(a => a.Username == entity.Username || a.Email == entity.Email || a.TaxNumber == entity.TaxNumber).Any();
        public bool HasSameRecords(CreateAccountModel entity) => db.Shops.Where(a => a.Username == entity.Username || a.Email == entity.Email || a.TaxNumber == entity.TaxNumber).Any();

        public SelectList SetShopDropdownList(int id) => new SelectList(db.Shops.Where(a=>a.ID==id).OrderBy(p => p.Name).ToList(), "ID", "Title");

        public SelectList SetShopDropdownList() => new SelectList(db.Shops.OrderBy(p => p.Name).ToList(), "ID", "Title");

        public IEnumerable<SelectListItem> SetShopDropdownList(bool IsSelectListItem) => db.Shops.Select(a => new SelectListItem { Text = a.Title, Value = a.ID.ToString() });
        public IEnumerable<SelectListItem> SetShopDropdownList(bool IsSelectListItem,int id) => db.Shops.Where(a=>a.ID==id).Select(a => new SelectListItem { Text = a.Title, Value = a.ID.ToString() });
    }
}