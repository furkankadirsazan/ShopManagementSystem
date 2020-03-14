using ShopManagementSystem.WebUI.Entity;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.Repository.Abstract
{
    public interface IOutOfStockStatusRepository : IGenericRepository<OutOfStockStatuses>
    {
        SelectList SetOutOfStockDropdownList();
        IEnumerable<SelectListItem> SetOutOfStockDropdownList(bool IsSelectListItem);
        bool CheckRelatedRecords(int id);
        bool HasSameRecords(string name);
    }
}
