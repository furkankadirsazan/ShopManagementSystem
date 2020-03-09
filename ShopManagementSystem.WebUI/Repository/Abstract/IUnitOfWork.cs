using System;

namespace ShopManagementSystem.WebUI.Repository.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        ILogRepository Logs { get; }
        IRoleRepository Roles { get; }
        ISettingRepository Settings { get; }
        IUserRepository Users { get; }
        IOutOfStockStatusRepository OutOfStockStatuses { get; }
        IProductGalleryRepository ProductGalleries{ get; }
        IProductRepository Products { get; }
        IShopRepository Shops { get; }
        ISystemSettingRepository SystemSettings { get; }
        ITaxDescriptionRepository TaxDescriptions { get; }
        IWarrantyPeriodRepository WarrantyPeriods { get; }
        int SaveChanges();
        
    }
}
