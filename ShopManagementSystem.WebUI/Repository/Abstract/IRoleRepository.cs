using ShopManagementSystem.WebUI.Entity;
using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.Repository.Abstract
{
    public interface IRoleRepository : IGenericRepository<Roles>
    {
        SelectList SetRoleDropdownList();
    }
}
