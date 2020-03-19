using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System.Linq;

namespace ShopManagementSystem.WebUI.Repository.Concrete.EntityFramework
{
    public class EfOrderRepository : EfGenericRepository<Orders>, IOrderRepository
    {
        public EfOrderRepository(ShopManagementSystemEntities db) : base(db)
        {

        }
        public ShopManagementSystemEntities _db
        {
            get { return db; }
        }
        public bool HasSameRecords(Orders entity) => db.Orders.Where(a => a.ProductID == entity.ProductID && a.ShopID == entity.ShopID).Any();
    }
}