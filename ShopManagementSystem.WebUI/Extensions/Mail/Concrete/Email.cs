using ShopManagementSystem.WebUI.Extensions.Mail.Abstract;
using ShopManagementSystem.WebUI.Models;
using System;
using System.Net;
using System.Net.Mail;

namespace ShopManagementSystem.WebUI.Extensions.Mail.Concrete
{
    public class Email : IEmail<EmailModel>
    {
        public void SendForAccountAuthentication(EmailModel entity)
        {
            if (entity != null)
            {
                var senderEmail = entity.SenderEmail.ToLower();
                var receiverEmail = entity.ReceiverEmail.ToLower();
                var mailTitle = entity.MailTitle;
                var senderName = entity.SenderName;
                var _message = entity.Message;
                var fromAddress = new MailAddress(senderEmail);
                var toAddress = new MailAddress(receiverEmail);
                var subject = mailTitle + " from " + senderName;
                var body = "" +
                "<p>" +
                "Bu mesaj sizlere " + senderName + " tarafından hesabınızı onaylamanız için gönderildi. Aşağıdaki linke tıklayarak hesabınızı onaylayınız." +
                "</p>" + "<br>" +
                "<p><b> Onaylama Linki: </b><a href="+"\"" + _message  +"\">" + _message + "</a></p>";
                var smtpAdress = entity.MailServer;
                string mailPassword = entity.MailPassword;
                using (var smtp = new SmtpClient
                {
                    Host = smtpAdress,
                    Port = entity.Port,
                    Timeout = 10000,
                    EnableSsl = entity.isSecure,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = entity.isUseDefaultCredentials,
                    Credentials = new NetworkCredential(fromAddress.Address, mailPassword)
                })
                {
                    using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body, IsBodyHtml = true })
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

        public void SendForNewPassword(EmailModel entity)
        {
            if (entity != null)
            {
                var senderEmail = entity.SenderEmail.ToLower();
                var receiverEmail = entity.ReceiverEmail.ToLower();
                var mailTitle = entity.MailTitle;
                var senderName = entity.SenderName;
                var fromAddress = new MailAddress(senderEmail);
                var toAddress = new MailAddress(receiverEmail);
                var subject = mailTitle + " from " + senderName;
                var newGeneratedPassword = entity.NewGeneratedPassword;
                var body = "" +
                "<p>" +
                "Bu mesaj sizlere " + senderName + " tarafından şifrenizi sıfırlamaya yardımcı olmak için gönderildi." +
                "</p>" + "</br>" + "<p>" +
                "Sizin için oluşturulmuş yeni şifreniz: <b>" + newGeneratedPassword + "</b></p>";

                var smtpAdress = entity.MailServer;
                string mailPassword = entity.MailPassword;
                using (var smtp = new SmtpClient
                {
                    Host = smtpAdress,
                    Port = entity.Port,
                    Timeout = 10000,
                    EnableSsl = entity.isSecure,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = entity.isUseDefaultCredentials,
                    Credentials = new NetworkCredential(fromAddress.Address, mailPassword)
                })
                {
                    using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body, IsBodyHtml = true })
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
}