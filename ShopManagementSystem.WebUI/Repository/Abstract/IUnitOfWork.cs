﻿using System;

namespace ShopManagementSystem.WebUI.Repository.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        ILogRepository Logs { get; }
        IRoleRepository Roles { get; }
        ISettingRepository Settings { get; }
        IOutOfStockStatusRepository OutOfStockStatuses { get; }
        IProductGalleryRepository ProductGalleries{ get; }
        IProductRepository Products { get; }
        IShopRepository Shops { get; }
        ISystemSettingRepository SystemSettings { get; }
        ITaxDescriptionRepository TaxDescriptions { get; }
        IWarrantyPeriodRepository WarrantyPeriods { get; }
        ICountyRepository Counties { get; }
        IProvinceRepository Provinces { get; }
        IOrderRepository Orders { get; }
        ICustomerRepository Customers { get; }
        IOrderStatusRepository OrderStatuses { get; }
        ICategoryRepository Categories { get; }
        IProductCategoryRepository ProductCategories { get; }
        int SaveChanges();
        
    }
}
