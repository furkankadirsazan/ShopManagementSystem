using ShopManagementSystem.WebUI.Entity;
using ShopManagementSystem.WebUI.Repository.Abstract;

namespace ShopManagementSystem.WebUI.Repository.Concrete.EntityFramework
{
    public class EfLogRepository : EfGenericRepository<Logs>, ILogRepository
    {
        public EfLogRepository(ShopManagementSystemEntities db) : base(db)
        {

        }
        public ShopManagementSystemEntities _db
        {
            get { return db; }
        }
    }
}