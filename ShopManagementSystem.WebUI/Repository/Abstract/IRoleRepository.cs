using ShopManagementSystem.WebUI.Entity;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.Repository.Abstract
{
    public interface IRoleRepository : IGenericRepository<Roles>
    {
        List<SelectListItem> SetRoleDropdownList();
    }
}
