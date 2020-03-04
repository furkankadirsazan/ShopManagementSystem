using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagementSystem.WebUI.Repository.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        ILogRepository Logs { get; }
        IRoleRepository Roles { get; }
        ISettingRepository Settings { get; }
        IUserRepository Users { get; }
        int SaveChanges();
    }
}
