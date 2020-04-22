using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Repository.Abstract;

namespace ShopManagementSystem.WebUI.Repository.Concrete.EntityFramework
{
    public sealed class EfUnitOfWork : IUnitOfWork
    {
        private readonly ShopManagementSystemEntities dbContext;
        public EfUnitOfWork()
        {
            dbContext = new ShopManagementSystemEntities();
        }
        private ILogRepository _logs;
        public ILogRepository Logs
        {
            get
            {
                return _logs ?? (_logs = new EfLogRepository(dbContext));
            }
        }
       
        private IRoleRepository _roles;
        public IRoleRepository Roles
        {
            get
            {
                return _roles ?? (_roles = new EfRoleRepository(dbContext));
            }
        }
        private ISettingRepository _settings;
        public ISettingRepository Settings
        {
            get
            {
                return _settings ?? (_settings = new EfSettingRepository(dbContext));
            }
        }
       
        private ISystemSettingRepository _systemsettings;
        public ISystemSettingRepository SystemSettings
        {
            get
            {
                return _systemsettings ?? (_systemsettings = new EfSystemSettingRepository(dbContext));
            }
        }
        

        private IOutOfStockStatusRepository _outofstockstatus;
        public IOutOfStockStatusRepository OutOfStockStatuses
        {
            get
            {
                return _outofstockstatus ?? (_outofstockstatus = new EfOutOfStockStatusRepository(dbContext));
            }
        }
        private IProductGalleryRepository _productgalleries;
        public IProductGalleryRepository ProductGalleries
        {
            get
            {
                return _productgalleries ?? (_productgalleries = new EfProductGalleryRepository(dbContext));
            }
        }
        private IProductRepository _products;
        public IProductRepository Products
        {
            get
            {
                return _products ?? (_products = new EfProductRepository(dbContext));
            }
        }
        private IShopRepository _shops;
        public IShopRepository Shops
        {
            get
            {
                return _shops ?? (_shops = new EfShopRepository(dbContext));
            }
        }

        private ITaxDescriptionRepository _taxdescriptions;
        public ITaxDescriptionRepository TaxDescriptions
        {
            get
            {
                return _taxdescriptions ?? (_taxdescriptions = new EfTaxDescriptionRepository(dbContext));
            }
        }

        private IWarrantyPeriodRepository _warrantyperiods;
        public IWarrantyPeriodRepository WarrantyPeriods
        {
            get
            {
                return _warrantyperiods ?? (_warrantyperiods = new EfWarrantyPeriodRepository(dbContext));
            }
        }

        private IProvinceRepository _provinces;
        public IProvinceRepository Provinces
        {
            get
            {
                return _provinces ?? (_provinces = new EfProvinceRepository(dbContext));
            }
        }
        private ICountyRepository _counties;
        public ICountyRepository Counties
        {
            get
            {
                return _counties ?? (_counties = new EfCountyRepository(dbContext));
            }
        }

        private IOrderRepository _orders;
        public IOrderRepository Orders
        {
            get
            {
                return _orders ?? (_orders = new EfOrderRepository(dbContext));
            }
        }
        private ICustomerRepository _customers;
        public ICustomerRepository Customers
        {
            get
            {
                return _customers ?? (_customers = new EfCustomerRepository(dbContext));
            }
        }
        private IOrderStatusRepository _orderstatuses;
        public IOrderStatusRepository OrderStatuses
        {
            get
            {
                return _orderstatuses ?? (_orderstatuses = new EfOrderStatusRepository(dbContext));
            }
        }
        private ICategoryRepository _categories;
        public ICategoryRepository Categories
        {
            get
            {
                return _categories ?? (_categories = new EfCategoryRepository(dbContext));
            }
        }
        private IProductCategoryRepository _productcategories;
        public IProductCategoryRepository ProductCategories
        {
            get
            {
                return _productcategories ?? (_productcategories = new EfProductCategoryRepository(dbContext));
            }
        }
        public void Dispose()
        {
            dbContext.Dispose();
        }
        public int SaveChanges() => dbContext.SaveChanges();

    }
}