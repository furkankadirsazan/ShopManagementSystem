using ShopManagementSystem.WebUI.Entity;

namespace ShopManagementSystem.WebUI.Repository.Abstract
{
    public interface IProductGalleryRepository : IGenericRepository<ProductGallery>
    {
        bool HasSameRecords(ProductGallery entity);
    }
}
