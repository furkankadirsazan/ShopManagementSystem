using ShopManagementSystem.WebUI.Entity;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.Repository.Abstract
{
    public interface ICountyRepository : IGenericRepository<Counties>
    {
        SelectList SetCountyDropdownList();
        SelectList SetCountyDropdownList(int provinceId);
        IEnumerable<SelectListItem> SetCountyDropdownList(int provinceId, bool IsSelectListItem);
    }
}
