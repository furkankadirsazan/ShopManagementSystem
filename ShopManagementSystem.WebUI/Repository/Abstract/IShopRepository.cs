using ShopManagementSystem.WebUI.Entity;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.Repository.Abstract
{
    public interface IShopRepository : IGenericRepository<Shops>
    {
        Shops GetByUsername(string username);
        Shops GetByEmail(string email);
        Shops GetByUsernameAndPassword(string username, string password);
        bool HasSameRecords(Shops entity);
        string CreateAuthenticationCode();
        bool CheckRelatedRecords(int id);

        SelectList SetShopDropdownList();

        SelectList SetShopDropdownList(int id);

        IEnumerable<SelectListItem> SetShopDropdownList(bool IsSelectListItem);
        IEnumerable<SelectListItem> SetShopDropdownList(bool IsSelectListItem,int id);
    }
}
