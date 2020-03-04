using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.Routes.LowercaseUrls = true;
            context.MapRoute(
               "Admin_default",
               "Admin/{controller}/{action}/{id}",
               new { controller = "home", action = "index", id = UrlParameter.Optional },
               new[] { "ShopManagementSystem.WebUI.Areas.Admin.Controllers" }
           );
        }
    }
}