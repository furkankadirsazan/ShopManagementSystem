using ShopManagementSystem.WebUI.Entity;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.Repository.Abstract
{
    public interface IOrderStatusRepository : IGenericRepository<OrderStatuses>
    {
        SelectList SetOrderStatusDropdownList();
        IEnumerable<SelectListItem> SetOrderStatusDropdownList(bool IsSelectListItem);
    }
}
