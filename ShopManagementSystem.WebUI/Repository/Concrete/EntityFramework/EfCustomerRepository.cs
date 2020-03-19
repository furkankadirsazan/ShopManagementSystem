using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.Repository.Concrete.EntityFramework
{
    public class EfCustomerRepository : EfGenericRepository<Customers>, ICustomerRepository
    {
        public EfCustomerRepository(ShopManagementSystemEntities db) : base(db)
        {

        }
        public ShopManagementSystemEntities _db
        {
            get { return db; }
        }
        public bool HasSameRecords(Customers entity) => db.Customers.Where(a => a.ShopID == entity.ShopID && (a.Email == entity.Email || a.Phone == entity.Phone)).Any();
        public bool CheckRelatedRecords(int id) => (db.Orders.Where(a => a.CustomerID == id).Any());
        public SelectList SetCustomerDropdownList(int shopId) => new SelectList(db.Customers.Where(a => a.ShopID == shopId).OrderBy(p => p.Name).ToList(), "ID", "Name");
        public SelectList SetCustomerDropdownList() => new SelectList(db.Customers.OrderBy(p => p.Name).ToList(), "ID", "Name");
        public IEnumerable<SelectListItem> SetCustomerDropdownList(bool IsSelectListItem) => db.Customers.Select(a => new SelectListItem { Text = a.Name + " " + a.Surname + " - " + a.Phone, Value = a.ID.ToString() });
        public IEnumerable<SelectListItem> SetCustomerDropdownList(bool IsSelectListItem, int shopId) => db.Customers.Where(a => a.ShopID == shopId).Select(a => new SelectListItem { Text = a.Name + " " + a.Surname, Value = a.ID.ToString() });
    }
}