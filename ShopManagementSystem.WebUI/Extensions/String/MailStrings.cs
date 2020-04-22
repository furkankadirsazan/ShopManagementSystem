
namespace ShopManagementSystem.WebUI.Extensions.String
{
    internal static class MailStrings
    {
        public const string SenderEmail = "idealizteknikdestek@gmail.com";
        public const string SenderName = "İdealiz Teknik Destek Ekibi";
        public const string MailServer = "smtp.gmail.com";
        public const string MailPassword = "9IwcmHrxVt2q3XnO";
        public const bool IsSecure = true;
        public const bool IsBodyHtml = true;
        public const bool IsUseDefaultCredentials = false;
        public const int Port = 587;
        public const int Timeout = 10000;

        public const string Domain = "https://magaza.idealiz.biz";

        /// <summary>{0}</summary>
        public const string MailTitle = "{0}";

        /// <summary>{0} Shop Id {1} token </summary>
        public const string AuthenticationLink = Domain + "/security/verifyaccount?sid=" + "{0}" + "&token=" + "{1}";

        /// <summary>{0} Sender Name {1} Link {2} Link </summary>
        public const string AccountAuthenticationBody = "<p>" +
            "Bu mesaj sizlere {0} tarafından hesabınızı onaylamanız için gönderildi. Aşağıdaki linke tıklayarak hesabınızı onaylayınız." +
            "</p>" + "<br>" + "<p><b> Onaylama Linki: </b><a href=" + "\"{1}\">{2}</a></p>";

        /// <summary>{0} Sender Name {1} NewPassword </summary>
        public const string NewPasswordBody ="<p>" +
            "Bu mesaj sizlere {0} tarafından şifrenizi sıfırlamaya yardımcı olmak için gönderildi." +
            "</p>" + "</br>" + "<p>" +
            "Sizin için oluşturulmuş yeni şifreniz: <b>{1}</b></p>";

        /// <summary>{0} Title </summary>
        public const string CreateAccountBody =
            "<p>Merhaba {0} ,</p>" +
            "<p>Mağaza açma talebiniz başarıyla tarafımıza ulaşmıştır.</p>" +
            "<p>Talebiniz ekibimiz tarafından değerlendirilmekte olup süreç hakkında en kısa zamanda sizlere dönüş yapılacaktır.</p>" +
            "<p><b>İyi günler, iyi çalışmalar dileriz</b></p>";


        /// <summary>{0} Title </summary>
        public const string NewOrderBody =
            "<p>Merhaba {0} ,</p>" +
            "<p>Mağazanıza yeni bir sipariş geldi.</p>" +
            "<p>Detaylı bilgi için yönetim panelinizi ziyaret edebilirsiniz.</p>" +
            "<p><b>İyi günler, bol kazançlar dileriz</b></p>";

        /// <summary>{0}(mailTitle) from {1} (senderName)/summary>
        public const string Subject = "{0} from {1}" ;
    }
}