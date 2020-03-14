using ShopManagementSystem.WebUI.Entity;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.Repository.Abstract
{
    public interface ITaxDescriptionRepository : IGenericRepository<TaxDescriptions>
    {
        SelectList SetTaxDescriptionDropdownList();

        IEnumerable<SelectListItem> SetTaxDescriptionDropdownList(bool IsSelectListItem);
    }
}
