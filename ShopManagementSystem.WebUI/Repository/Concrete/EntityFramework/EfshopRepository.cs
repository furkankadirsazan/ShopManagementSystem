using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopManagementSystem.WebUI.Repository.Concrete.EntityFramework
{
    public class EfShopRepository:EfGenericRepository<Shops>,IShopRepository
    {
        public EfShopRepository(ShopManagementSystemEntities db) : base(db)
        {

        }
        public ShopManagementSystemEntities _db
        {
            get { return db; }
        }
    }
}