using ShopManagementSystem.WebUI.Areas.Admin.Models;
using ShopManagementSystem.WebUI.Entity;

namespace ShopManagementSystem.WebUI.Repository.Abstract
{
    public interface ISettingRepository : IGenericRepository<Settings>
    {
        bool UpdateSettings(SettingModel sm);
        SettingModel SetSettings();
        string GetValueWithSKey(string skey);
    }
}
