using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public List<SelectListItem> SetTaxDescriptionDropdownList()
        {
            throw new NotImplementedException();
        }
    }
}