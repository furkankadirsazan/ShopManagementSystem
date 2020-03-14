using ShopManagementSystem.WebUI.Repository.Abstract;
using ShopManagementSystem.WebUI.Repository.Concrete.EntityFramework;
using System;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace ShopManagementSystem.WebUI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<ILogRepository, EfLogRepository>();
            container.RegisterType<IRoleRepository, EfRoleRepository>();
            container.RegisterType<ISettingRepository, EfSettingRepository>();
            container.RegisterType<ISystemSettingRepository, EfSystemSettingRepository>();
            container.RegisterType<IProvinceRepository, EfProvinceRepository>();
            container.RegisterType<ICountyRepository, EfCountyRepository>();
            container.RegisterType<IUnitOfWork, EfUnitOfWork>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}