using ShopManagementSystem.WebUI.Areas.Admin.Models;
using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagementSystem.WebUI.Repository.Concrete.EntityFramework
{

    public class EfSystemSettingRepository : EfGenericRepository<SystemSettings>, ISystemSettingRepository
    {
        public EfSystemSettingRepository(ShopManagementSystemEntities db) : base(db)
        {

        }
        public ShopManagementSystemEntities _db
        {
            get { return db; }
        }
        public bool GetValueWithSKey(string skey)
        {
            var setting = db.SystemSettings.Where(a => a.Name == skey).FirstOrDefault();
            return setting.Value;
        }
        //public bool UpdateSystemSettings(IEnumerable<SystemSettings> settings)
        //{
        //    try
        //    {
        //        foreach (var item in settings)
        //        {
        //            var cmd = $"update SystemSettings set Value={item.Value} where Name={item.Name}";
        //            db.Database.ExecuteSqlCommand(cmd);
        //        }
        //        db.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        public bool UpdateSystemSettings(SettingModel sm)
        {
            try
            {
                var settings = db.SystemSettings.ToList();
                foreach (var item in settings)
                {
                    var a = sm.SystemSettings[item.Name];
                    item.Value = Convert.ToBoolean(a);
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}