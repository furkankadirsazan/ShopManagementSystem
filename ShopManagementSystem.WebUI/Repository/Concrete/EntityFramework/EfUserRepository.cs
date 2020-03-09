using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System.Linq;
using System.Web.Security;

namespace ShopManagementSystem.WebUI.Repository.Concrete.EntityFramework
{
    public class EfUserRepository : EfGenericRepository<Users>, IUserRepository
    {
        public EfUserRepository(ShopManagementSystemEntities db) : base(db)
        {

        }
        public ShopManagementSystemEntities _db
        {
            get { return db; }
        }

        public string CreateAuthenticationCode() => Membership.GeneratePassword(6, 1);
        public Users GetByEmail(string email) => db.Users.Where(u => u.Email == email).FirstOrDefault();
        public Users GetByShopId(int shopId) => db.Users.Where(u => u.ShopID == shopId).FirstOrDefault();
        public Users GetByUsername(string username) => db.Users.Where(u => u.Username == username).FirstOrDefault();    
        public Users GetByUsernameAndPassword(string username, string password) => db.Users.Where(u => u.Username == username && u.Password == password).FirstOrDefault();
        public bool HasSameRecords(Users entity) => db.Users.Where(a => a.Username == entity.Username || a.Email == entity.Email).Any();

    }
}