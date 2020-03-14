using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;

namespace ShopManagementSystem.WebUI.Repository.Concrete.EntityFramework
{
    public class EfWarrantyPeriodRepository : EfGenericRepository<WarrantyPeriods>, IWarrantyPeriodRepository
    {
        public EfWarrantyPeriodRepository(ShopManagementSystemEntities db) : base(db)
        {

        }
        public ShopManagementSystemEntities _db
        {
            get { return db; }
        }

        public bool CheckRelatedRecords(int id) => (db.Products.Where(a => a.WarrantyPeriodID == id).Any());

        public bool HasSameRecords(string name) => (db.WarrantyPeriods.Where(a => a.Name == name.ToLower()).Any());

        public SelectList SetWarrantyPeriodDropdownList() => new SelectList(db.WarrantyPeriods.OrderBy(p => p.Name).ToList(), "ID", "Name");

        public IEnumerable<SelectListItem> SetWarrantyPeriodDropdownList(bool IsSelectListItem) => db.WarrantyPeriods.Select(a => new SelectListItem { Text = a.Name, Value = a.ID.ToString() });
    }
}