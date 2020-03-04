using Elmah;
using ShopManagementSystem.WebUI.App_Start;
using ShopManagementSystem.WebUI.Repository.Concrete.EntityFramework;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ShopManagementSystem.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected EfUnitOfWork uow = new EfUnitOfWork();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
        public void SSLRedirecting()
        {

            var context = HttpContext.Current;
            if (context == null) return;
            if (Request.IsLocal) return;
            var redirect = false;
            var uri = context.Request.Url;
            var scheme = uri.GetComponents(UriComponents.Scheme, UriFormat.Unescaped);
            var pathAndQuery = uri.GetComponents(UriComponents.PathAndQuery, UriFormat.Unescaped);

            if (uow.SystemSettings.GetValueWithSKey("safety-site"))
            {
                if (!scheme.Equals("https"))
                {
                    scheme = "https";
                    redirect = true;
                }
            }
            else
            {
                scheme = "http";
                redirect = true;
            }

            if (redirect)
            {
                HttpContext.Current.Response.Status = "301 Moved Permanently";
                HttpContext.Current.Response.StatusCode = 301;
                HttpContext.Current.Response.AddHeader("Location", scheme + "://" + pathAndQuery);
            }
        }
        // ELMAH Filtering
        protected void ErrorLog_Filtering(object sender, ExceptionFilterEventArgs e)
        {
            FilterError404(e);
        }
        protected void ErrorMail_Filtering(object sender, ExceptionFilterEventArgs e)
        {
            FilterError404(e);
        }
        // Dismiss 404 errors for ELMAH
        private void FilterError404(ExceptionFilterEventArgs e)
        {
            if (e.Exception.GetBaseException() is HttpException)
            {
                HttpException ex = (HttpException)e.Exception.GetBaseException();
                if (ex.GetHttpCode() == 404)
                    e.Dismiss();
            }
        }
    }
}
