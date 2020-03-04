using System;
using System.Web;
using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.Extensions.Security
{
    public class TenantAttribute : AuthorizeAttribute
    {
        public TenantAttribute(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
        public string LoginPage { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.HttpContext.Response.Redirect(LoginPage);
            }
            base.OnAuthorization(filterContext);
        }
    }
}