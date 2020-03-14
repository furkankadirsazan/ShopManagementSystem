using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System.Linq;

namespace ShopManagementSystem.WebUI.Repository.Concrete.EntityFramework
{
    public class EfProductRepository : EfGenericRepository<Products>, IProductRepository
    {
        public EfProductRepository(ShopManagementSystemEntities db) : base(db)
        {

        }
        public ShopManagementSystemEntities _db
        {
            get { return db; }
        }

        public bool HasSameRecords(Products entity) => db.Products.Where(a => a.Name == entity.Name).Any();
    }
}