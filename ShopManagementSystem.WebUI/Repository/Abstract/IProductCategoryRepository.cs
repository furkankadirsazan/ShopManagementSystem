using ShopManagementSystem.WebUI.Entity;

namespace ShopManagementSystem.WebUI.Repository.Abstract
{
    public interface IProductCategoryRepository : IGenericRepository<ProductCategories>
    {
        bool HasSameRecords(ProductCategories entity);
    }
}
