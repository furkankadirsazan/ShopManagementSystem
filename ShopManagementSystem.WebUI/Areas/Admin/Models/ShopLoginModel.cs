namespace ShopManagementSystem.WebUI.Areas.Admin.Models
{
    public class ShopLoginModel
    {
        public int ShopID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsRemember { get; set; }
    }
}