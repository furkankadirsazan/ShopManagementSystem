using ShopManagementSystem.WebUI.Entity;

namespace ShopManagementSystem.WebUI.Repository.Abstract
{
    public interface IOrderRepository : IGenericRepository<Orders>
    {
        bool HasSameRecords(Orders entity);
    }
}
