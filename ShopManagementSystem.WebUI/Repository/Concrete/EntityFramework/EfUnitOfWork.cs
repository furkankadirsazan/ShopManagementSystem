using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopManagementSystem.WebUI.Repository.Concrete.EntityFramework
{
    public class EfUnitOfWork : IUnitOfWork
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
        
        private IUserRepository _users;
        public IUserRepository Users
        {
            get
            {
                return _users ?? (_users = new EfUserRepository(dbContext));
            }
        }      
        public void Dispose()
        {
            dbContext.Dispose();
        }
        public int SaveChanges()
        {
            return dbContext.SaveChanges();
        }
    }
}