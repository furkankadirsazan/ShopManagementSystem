using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public List<SelectListItem> SetWarrantyPeriodDropdownList()
        {
            throw new NotImplementedException();
        }
    }
}