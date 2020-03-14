using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;

namespace ShopManagementSystem.WebUI.Repository.Concrete.EntityFramework
{
    public class EfOutOfStockStatusRepository:EfGenericRepository<OutOfStockStatuses>,IOutOfStockStatusRepository
    {
        public EfOutOfStockStatusRepository(ShopManagementSystemEntities db) : base(db)
        {

        }
        public ShopManagementSystemEntities _db
        {
            get { return db; }
        }

        public bool CheckRelatedRecords(int id) => (db.Products.Where(a => a.OutOfStockStatusID == id).Any());

        public bool HasSameRecords(string name) => (db.OutOfStockStatuses.Where(a => a.Name == name.ToLower()).Any());

        public SelectList SetOutOfStockDropdownList() => new SelectList(db.OutOfStockStatuses.OrderBy(p => p.Name).ToList(), "ID", "Name");

        public IEnumerable<SelectListItem> SetOutOfStockDropdownList(bool IsSelectListItem) => db.OutOfStockStatuses.Select(a => new SelectListItem { Text = a.Name, Value = a.ID.ToString() });
    }
}