using ShopManagementSystem.WebUI.Areas.Admin.Models;
using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagementSystem.WebUI.Repository.Concrete.EntityFramework
{
    public class EfSettingRepository : EfGenericRepository<Settings>, ISettingRepository
    {
        public EfSettingRepository(ShopManagementSystemEntities db) : base(db)
        {

        }
        public ShopManagementSystemEntities _db
        {
            get { return db; }
        }
        public bool UpdateSettings(SettingModel sm)
        {
            try
            {
                var settings = db.Settings.ToList();
                foreach (var item in settings)
                {
                    var a = sm.Fields[item.Skey].ToString();
                    item.Value = a;
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public SettingModel SetSettings()
        {
            var settings = db.Settings.ToList();
            var systemsettings = db.SystemSettings.ToList();
            SettingModel sm = new SettingModel
            {
                Fields = new Dictionary<string, string>() { },
                SystemSettings = new Dictionary<string, bool>() { }
            };
            foreach (var item in settings)
            {
                sm.Fields.Add(item.Skey, item.Value);
            }
            foreach (var item in systemsettings)
            {
                sm.SystemSettings.Add(item.Name, item.Value);
            }

            return sm;
        }
        public string GetValueWithSKey(string skey)
        {
            var setting = db.Settings.Where(a => a.Skey == skey).FirstOrDefault();
            return setting.Value ?? "";
        }

    }
}