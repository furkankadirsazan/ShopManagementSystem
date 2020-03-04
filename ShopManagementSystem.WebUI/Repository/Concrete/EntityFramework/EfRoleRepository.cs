using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Repository.Abstract;

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.Repository.Concrete.EntityFramework
{
    public class EfRoleRepository : EfGenericRepository<Roles>, IRoleRepository
    {
        public EfRoleRepository(ShopManagementSystemEntities db) : base(db)
        {

        }
        public ShopManagementSystemEntities _db
        {
            get { return db; }
        }
        public List<SelectListItem> SetRoleDropdownList()
        {
            List<SelectListItem> Roles = new List<SelectListItem>();
            Roles.Add(
                   new SelectListItem
                   {
                       Text = "Seçiniz",
                       Value = ""
                   });
            foreach (var item in db.Roles.OrderBy(p => p.Name).ToList())
            {
                Roles.Add(
                    new SelectListItem
                    {
                        Text = item.DisplayName,
                        Value = item.ID.ToString()
                    });
            }
            return Roles;
        }
    }
}