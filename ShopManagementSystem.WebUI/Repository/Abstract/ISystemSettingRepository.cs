using ShopManagementSystem.WebUI.Areas.Admin.Models;
using ShopManagementSystem.WebUI.Entity;


namespace ShopManagementSystem.WebUI.Repository.Abstract
{
    public interface ISystemSettingRepository : IGenericRepository<SystemSettings>
    {
        bool GetValueWithSKey(string skey);
        bool UpdateSystemSettings(SettingModel sm);
    }
}
