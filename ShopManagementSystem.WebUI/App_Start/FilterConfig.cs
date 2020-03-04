using ShopManagementSystem.WebUI.Extensions.Log;
using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.App_Start
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ElmahHandledErrorLoggerFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}