
namespace ShopManagementSystem.WebUI.Extensions.Mail.Abstract
{
    public interface IEmail
    {
        void SendForNewPassword(string ReceiverEmail, string NewGeneratedPassword);
        void SendForAccountAuthentication(string ReceiverEmail, string AuthenticationCode, int ShopId);
        void SendForSignUp(string ReceiverEmail, string Title);
    }
}
