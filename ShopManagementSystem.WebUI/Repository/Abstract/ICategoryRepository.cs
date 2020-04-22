using ShopManagementSystem.WebUI.Entity;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.Repository.Abstract
{
    public interface ICategoryRepository: IGenericRepository<Categories>
    {
        bool HasSameRecords(Categories entity);
        bool CheckRelatedRecords(int id);
        SelectList SetCategoryDropdownList();
        IEnumerable<SelectListItem> SetCategoryDropdownList(bool IsSelectListItem);
    }
}
