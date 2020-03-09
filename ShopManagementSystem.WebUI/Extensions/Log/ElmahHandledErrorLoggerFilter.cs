using Elmah;
using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.Extensions.Log
{
    public class ElmahHandledErrorLoggerFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // Long only handled exceptions, because all other will be caught by ELMAH anyway
            if (context.ExceptionHandled)
                ErrorSignal.FromCurrentContext().Raise(context.Exception);
        }
    }
}