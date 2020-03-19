using ShopManagementSystem.WebUI.Entity;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.Repository.Abstract
{
    public interface IProductRepository : IGenericRepository<Products>
    {
        bool HasSameRecords(Products entity);

        SelectList SetProductDropdownList();

        SelectList SetProductDropdownList(int shopId);

        IEnumerable<SelectListItem> SetProductDropdownList(bool IsSelectListItem);
        IEnumerable<SelectListItem> SetProductDropdownList(bool IsSelectListItem, int shopId);

        bool CheckRelatedRecords(int id);
    }
}
