
namespace ShopManagementSystem.WebUI.Extensions.Mail.Abstract
{
    public interface IEmail<T>
    {
        void SendForNewPassword(T entity);
        void SendForAccountAuthentication(T entity);
    }
}
