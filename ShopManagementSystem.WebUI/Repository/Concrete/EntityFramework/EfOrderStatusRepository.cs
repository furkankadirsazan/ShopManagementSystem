using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.Repository.Concrete.EntityFramework
{
    public class EfOrderStatusRepository : EfGenericRepository<OrderStatuses>, IOrderStatusRepository
    {
        public EfOrderStatusRepository(ShopManagementSystemEntities db) : base(db)
        {

        }
        public ShopManagementSystemEntities _db
        {
            get { return db; }
        }

        public SelectList SetOrderStatusDropdownList() => new SelectList(db.OrderStatuses.OrderBy(p => p.Name).ToList(), "ID", "Name");
        public IEnumerable<SelectListItem> SetOrderStatusDropdownList(bool IsSelectListItem) => db.OrderStatuses.Select(a => new SelectListItem { Text = a.Name, Value = a.ID.ToString() });
    }
}