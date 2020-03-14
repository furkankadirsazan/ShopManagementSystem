using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.Repository.Concrete.EntityFramework
{
    public class EfTaxDescriptionRepository : EfGenericRepository<TaxDescriptions>,ITaxDescriptionRepository
    {
        public EfTaxDescriptionRepository(ShopManagementSystemEntities db) : base(db)
        {

        }
        public ShopManagementSystemEntities _db
        {
            get { return db; }
        }
        public SelectList SetTaxDescriptionDropdownList() => new SelectList(db.TaxDescriptions.OrderBy(p => p.Name).ToList(), "ID", "Name");
        public IEnumerable<SelectListItem> SetTaxDescriptionDropdownList(bool IsSelectListItem) => db.TaxDescriptions.Select(a => new SelectListItem { Text = a.Name, Value = a.ID.ToString() });
    }
}