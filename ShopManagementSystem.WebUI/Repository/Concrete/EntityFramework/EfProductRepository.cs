using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System;

namespace ShopManagementSystem.WebUI.Repository.Concrete.EntityFramework
{
    public class EfProductRepository : EfGenericRepository<Products>, IProductRepository
    {
        public EfProductRepository(ShopManagementSystemEntities db) : base(db)
        {

        }
        public ShopManagementSystemEntities _db
        {
            get { return db; }
        }

        public bool CheckRelatedRecords(int id) => (db.Orders.Where(a => a.ProductID == id).Any() || db.ProductGallery.Where(a => a.ProductID == id).Any());

        public bool HasSameRecords(Products entity) => db.Products.Where(a => a.Name == entity.Name).Any();

        public SelectList SetProductDropdownList(int shopId) => new SelectList(db.Products.Where(a => a.ShopID == shopId).OrderBy(p => p.Name).ToList(), "ID", "Name");

        public SelectList SetProductDropdownList() => new SelectList(db.Products.OrderBy(p => p.Name).ToList(), "ID", "Name");

        public IEnumerable<SelectListItem> SetProductDropdownList(bool IsSelectListItem) => db.Products.Select(a => new SelectListItem { Text = a.Name, Value = a.ID.ToString() });

        public IEnumerable<SelectListItem> SetProductDropdownList(bool IsSelectListItem, int shopId) => db.Products.Where(a => a.ShopID == shopId).Select(a => new SelectListItem { Text = a.Name, Value = a.ID.ToString() });
    }
}