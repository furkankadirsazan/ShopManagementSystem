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
        public SelectList SetRoleDropdownList() => new SelectList (db.Roles.OrderBy(p => p.Name).ToList(), "ID", "DisplayName");


        //public List<SelectListItem> SetCountyDropdownList()
        //{
        //    List<SelectListItem> Counties = new List<SelectListItem>();
        //    Counties.Add(
        //           new SelectListItem
        //           {
        //               Text = "Seçiniz",
        //               Value = ""
        //           });
        //    foreach (var item in db.Counties.OrderBy(p => p.Name).ToList())
        //    {
        //        Counties.Add(
        //            new SelectListItem
        //            {
        //                Text = item.Name,
        //                Value = item.ID.ToString()
        //            });
        //    }
        //    return Counties;
        //}
    }
}