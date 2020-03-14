using ShopManagementSystem.WebUI.Entity;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.Repository.Abstract
{
    public interface IWarrantyPeriodRepository : IGenericRepository<WarrantyPeriods>
    {
        SelectList SetWarrantyPeriodDropdownList();
        IEnumerable<SelectListItem> SetWarrantyPeriodDropdownList(bool IsSelectListItem);
        bool CheckRelatedRecords(int id);
        bool HasSameRecords(string name);
    }
}
