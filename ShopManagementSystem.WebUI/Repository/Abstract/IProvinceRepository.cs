using ShopManagementSystem.WebUI.Entity;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.Repository.Abstract
{
    public interface IProvinceRepository : IGenericRepository<Provinces>
    {
        SelectList SetProvinceDropdownList();

        IEnumerable<SelectListItem> SetProvinceDropdownList(bool IsSelectListItem);
    }
}
