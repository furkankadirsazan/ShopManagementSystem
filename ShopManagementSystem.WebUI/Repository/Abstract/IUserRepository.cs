using ShopManagementSystem.WebUI.Entity;

namespace ShopManagementSystem.WebUI.Repository.Abstract
{
    public interface IUserRepository : IGenericRepository<Users>
    {
        Users GetByUsername(string username);
        Users GetByEmail(string email);
        Users GetByUsernameAndPassword(string username, string password);
        bool HasSameRecords(Users entity);
    }
}
