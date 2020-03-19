using ShopManagementSystem.WebUI.Entity;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.Repository.Abstract
{
    public interface ICustomerRepository : IGenericRepository<Customers>
    {
        bool HasSameRecords(Customers entity);
        bool CheckRelatedRecords(int id);
        SelectList SetCustomerDropdownList();
        SelectList SetCustomerDropdownList(int shopId);
        IEnumerable<SelectListItem> SetCustomerDropdownList(bool IsSelectListItem);

        IEnumerable<SelectListItem> SetCustomerDropdownList(bool IsSelectListItem, int shopId);
    }
}
