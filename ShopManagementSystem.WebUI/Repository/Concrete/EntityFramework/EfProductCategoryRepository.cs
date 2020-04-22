using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System.Linq;

namespace ShopManagementSystem.WebUI.Repository.Concrete.EntityFramework
{
    public class EfProductCategoryRepository : EfGenericRepository<ProductCategories>, IProductCategoryRepository
    {
        public EfProductCategoryRepository(ShopManagementSystemEntities db) : base(db)
        {

        }
        public ShopManagementSystemEntities _db
        {
            get { return db; }
        }
        public bool HasSameRecords(ProductCategories entity) => db.ProductCategories.Where(a => a.ProductID == entity.ProductID && a.ShopID == entity.ShopID && a.CategoryID==entity.CategoryID).Any();
    }
}