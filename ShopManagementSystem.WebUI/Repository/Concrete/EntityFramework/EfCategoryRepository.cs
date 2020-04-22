using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.Repository.Concrete.EntityFramework
{
    public class EfCategoryRepository:EfGenericRepository<Categories>,ICategoryRepository
    {
        public EfCategoryRepository(ShopManagementSystemEntities db) : base(db)
        {

        }
        public ShopManagementSystemEntities _db
        {
            get { return db; }
        }
        public bool HasSameRecords(Categories entity) => db.Categories.Where(a => a.Name == entity.Name).Any();
        public bool CheckRelatedRecords(int id) => (db.ProductCategories.Where(a => a.CategoryID == id).Any());
        public SelectList SetCategoryDropdownList() => new SelectList(db.Categories.OrderBy(p => p.Name).ToList(), "ID", "Name");
        public IEnumerable<SelectListItem> SetCategoryDropdownList(bool IsSelectListItem) => db.Categories.Select(a => new SelectListItem { Text = a.Name, Value = a.ID.ToString() });

    }
}