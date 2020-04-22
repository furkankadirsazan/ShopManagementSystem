using ShopManagementSystem.WebUI.Extensions.Mail.Abstract;
using ShopManagementSystem.WebUI.Extensions.Security;
using ShopManagementSystem.WebUI.Extensions.String;
using System;
using System.Net;
using System.Net.Mail;

namespace ShopManagementSystem.WebUI.Extensions.Mail.Concrete
{
    public class Email : IEmail
    {
        public void SendForAccountAuthentication(string ReceiverEmail, string AuthenticationCode, int ShopId)
        {

            var senderEmail = MailStrings.SenderEmail;
            var receiverEmail = ReceiverEmail.ToLower();
            var mailTitle = System.String.Format(MailStrings.MailTitle, "İdealiz Mağaza Girişi Hesap Onaylama İşleminiz Hakkında");
            var senderName = MailStrings.SenderName;

            var fromAddress = new MailAddress(senderEmail);
            var toAddress = new MailAddress(receiverEmail);
            var subject = System.String.Format(MailStrings.Subject, mailTitle, senderName);
            var authenticationCodeHash = HashString.MD5Hash(AuthenticationCode);
            var link = System.String.Format(MailStrings.AuthenticationLink, ShopId, authenticationCodeHash);
            var body = System.String.Format(MailStrings.AccountAuthenticationBody, senderName, link, link);
           
            using (var smtp = new SmtpClient
            {
                Host = MailStrings.MailServer,
                Port = MailStrings.Port,
                Timeout = MailStrings.Timeout,
                EnableSsl = MailStrings.IsSecure,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = MailStrings.IsUseDefaultCredentials,
                Credentials = new NetworkCredential(fromAddress.Address, MailStrings.MailPassword)
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body, IsBodyHtml = MailStrings.IsBodyHtml })
                {
                    try
                    {
                        smtp.Send(message);
                    }
                    catch
                    {
                        throw new NotImplementedException();
                    }
                }
            }
        }

        public void SendForNewPassword(string ReceiverEmail, string NewGeneratedPassword)
        {
            var senderEmail = MailStrings.SenderEmail;
            var receiverEmail = ReceiverEmail.ToLower();
            var mailTitle = System.String.Format(MailStrings.MailTitle, "İdealiz Mağaza Girişi Şifre Sıfırlama Talebiniz Hakkında");

            var senderName = MailStrings.SenderName;
            var fromAddress = new MailAddress(senderEmail);
            var toAddress = new MailAddress(receiverEmail);
            var subject = System.String.Format(MailStrings.Subject,mailTitle,senderName);
            var body = System.String.Format(MailStrings.NewPasswordBody,senderName,NewGeneratedPassword);

            using (var smtp = new SmtpClient
            {
                Host = MailStrings.MailServer,
                Port = MailStrings.Port,
                Timeout = MailStrings.Timeout,
                EnableSsl = MailStrings.IsSecure,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = MailStrings.IsUseDefaultCredentials,
                Credentials = new NetworkCredential(fromAddress.Address, MailStrings.MailPassword)
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body, IsBodyHtml = MailStrings.IsBodyHtml })
                {
                    try
                    {
                        smtp.Send(message);
                    }
                    catch
                    {
                        throw new NotImplementedException();
                    }
                }
            }

        }

        public void SendForSignUp(string ReceiverEmail, string Title)
        {
            var senderEmail = MailStrings.SenderEmail;
            var receiverEmail = ReceiverEmail.ToLower();
            var mailTitle = System.String.Format(MailStrings.MailTitle, "İdealiz Mağaza Açma Başvurunuz Hakkında");

            var senderName = MailStrings.SenderName;
            var fromAddress = new MailAddress(senderEmail);
            var toAddress = new MailAddress(receiverEmail);
            var subject = System.String.Format(MailStrings.Subject, mailTitle, senderName);
            var body = System.String.Format(MailStrings.CreateAccountBody, Title);

            using (var smtp = new SmtpClient
            {
                Host = MailStrings.MailServer,
                Port = MailStrings.Port,
                Timeout = MailStrings.Timeout,
                EnableSsl = MailStrings.IsSecure,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = MailStrings.IsUseDefaultCredentials,
                Credentials = new NetworkCredential(fromAddress.Address, MailStrings.MailPassword)
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body, IsBodyHtml = MailStrings.IsBodyHtml })
                {
                    try
                    {
                        smtp.Send(message);
                    }
                    catch
                    {
                        throw new NotImplementedException();
                    }
                }
            }

        }

        public void SendForNewOrder(string ReceiverEmail, string Title)
        {
            var senderEmail = MailStrings.SenderEmail;
            var receiverEmail = ReceiverEmail.ToLower();
            var mailTitle = System.String.Format(MailStrings.MailTitle, "Yeni Siparişiniz Var | İdealiz.biz");

            var senderName = MailStrings.SenderName;
            var fromAddress = new MailAddress(senderEmail);
            var toAddress = new MailAddress(receiverEmail);
            var subject = System.String.Format(MailStrings.Subject, mailTitle, senderName);
            var body = System.String.Format(MailStrings.NewOrderBody, Title);

            using (var smtp = new SmtpClient
            {
                Host = MailStrings.MailServer,
                Port = MailStrings.Port,
                Timeout = MailStrings.Timeout,
                EnableSsl = MailStrings.IsSecure,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = MailStrings.IsUseDefaultCredentials,
                Credentials = new NetworkCredential(fromAddress.Address, MailStrings.MailPassword)
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body, IsBodyHtml = MailStrings.IsBodyHtml })
                {
                    try
                    {
                        smtp.Send(message);
                    }
                    catch
                    {
                        throw new NotImplementedException();
                    }
                }
            }

        }

    }
}