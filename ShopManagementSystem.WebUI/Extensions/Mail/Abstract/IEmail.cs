
namespace ShopManagementSystem.WebUI.Extensions.Mail.Abstract
{
    public interface IEmail<T>
    {
        void SendForNewPassword(string ReceiverEmail, string NewGeneratedPassword);
        void SendForAccountAuthentication(string ReceiverEmail, string AuthenticationCode, int ShopId);
    }
}
