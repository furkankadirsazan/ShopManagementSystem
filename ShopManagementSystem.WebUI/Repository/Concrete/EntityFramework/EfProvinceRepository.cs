using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System.Linq;
using System.Web.Mvc;
using System;
using System.Collections.Generic;

namespace ShopManagementSystem.WebUI.Repository.Concrete.EntityFramework
{
    public class EfProvinceRepository : EfGenericRepository<Provinces>, IProvinceRepository
    {
        public EfProvinceRepository(ShopManagementSystemEntities db) : base(db)
        {

        }
        public ShopManagementSystemEntities _db
        {
            get { return db; }
        }

        public SelectList SetProvinceDropdownList() =>  new SelectList(db.Provinces.OrderBy(p => p.Name).ToList(), "ID", "Name");

        public IEnumerable<SelectListItem> SetProvinceDropdownList(bool IsSelectListItem) => db.Provinces.Select(a => new SelectListItem { Text = a.Name, Value = a.ID.ToString() });
    }
}