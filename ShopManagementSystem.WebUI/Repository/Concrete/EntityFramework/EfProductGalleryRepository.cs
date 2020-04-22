using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System.Linq;

namespace ShopManagementSystem.WebUI.Repository.Concrete.EntityFramework
{
    public class EfProductGalleryRepository:EfGenericRepository<ProductGallery>,IProductGalleryRepository
    {
        public EfProductGalleryRepository(ShopManagementSystemEntities db) : base(db)
        {

        }
        public ShopManagementSystemEntities _db
        {
            get { return db; }
        }
        public bool HasSameRecords(ProductGallery entity) => db.ProductGallery.Where(a => a.ProductID == entity.ProductID && a.ShopID == entity.ShopID && a.FileName == entity.FileName).Any();
    }
}