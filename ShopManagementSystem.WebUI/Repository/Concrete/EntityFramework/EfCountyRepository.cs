using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.Repository.Concrete.EntityFramework
{
    public class EfCountyRepository : EfGenericRepository<Counties>, ICountyRepository
    {
        public EfCountyRepository(ShopManagementSystemEntities db) : base(db)
        {

        }
        public ShopManagementSystemEntities _db
        {
            get { return db; }
        }

        public IEnumerable<SelectListItem> SetCountyDropdownList(int provinceId,bool IsSelectListItem) => db.Counties.Where(a=>a.ProvinceID==provinceId).Select(a=>new SelectListItem { Text = a.Name, Value = a.ID.ToString()});

        public SelectList SetCountyDropdownList() => new SelectList(db.Counties.OrderBy(p => p.Name).ToList(), "ID", "Name");

        public SelectList SetCountyDropdownList(int provinceId) => new SelectList(db.Counties.OrderBy(p => p.Name).ToList(), "ID", "Name"); 
        
    }
}