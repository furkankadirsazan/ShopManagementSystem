namespace ShopManagementSystem.WebUI.Models
{
    public class EmailModel
    {
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string ReceiverEmail { get; set; }
        public string MailTitle { get; set; }
        public string MailServer { get; set; }
        public string MailPassword { get; set; }
        public string Message { get; set; }
        public string VisitorPhone { get; set; }
        public string VisitorEmail { get; set; }
        public bool isSecure { get; set; }
        public bool isUseDefaultCredentials { get; set; }
        public int Port { get; set; }
        public string NewGeneratedPassword { get; set; }
        public EmailModel()
        {
            //Buraya default Ayarlar tanımlanabilir
        }
    }
}