using ShopManagementSystem.WebUI.Entity;

namespace ShopManagementSystem.WebUI.Repository.Abstract
{
    public interface IProductRepository : IGenericRepository<Products>
    {
        bool HasSameRecords(Products entity);
    }
}
