using ShopManagementSystem.WebUI.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.Extensions.Log
{
    public class TenantLogAttribute : ActionFilterAttribute
    {
        private Stopwatch _stopwatch;
        private ShopManagementSystemEntities uow;
        public TenantLogAttribute()
        {
            _stopwatch = new Stopwatch();
             uow = new ShopManagementSystemEntities();
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _stopwatch.Reset();
            _stopwatch.Start();
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _stopwatch.Stop();  
                                
            var request = filterContext.HttpContext.Request;   
            //var student = uow.Students.Where(a => a.Ssn == HttpContext.Current.User.Identity.Name).FirstOrDefault();
            //int studentId = Convert.ToInt32((student==null || string.IsNullOrEmpty(student.ID.ToString())) ? HttpContext.Current.Session["StudentID"] : student.ID);     
            Logs l = new Logs();
            l.AddedDate = DateTime.Now;
            l.ExecutionMs = _stopwatch.ElapsedMilliseconds;
            l.IPAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress;
            l.UrlAccessed = request.RawUrl;
            l.UserId = 0;//studentId;

       
            uow.Logs.Add(l);
            uow.SaveChanges();


            base.OnActionExecuted(filterContext);  
        }

        private string SerializeRequest(HttpRequestBase request)
        {
            string result = string.Empty;
            #region Form
            List<string> formVals = new List<string>(); //eğer sayfada bir form varsa, formda gönderilen tüm inputları alıp bir listeye atıyorum.
            if (request.Form.AllKeys != null && request.Form.AllKeys.ToList().Count > 0)
            {
                foreach (string s in request.Form.AllKeys.ToList())
                {
                    formVals.Add(request.Form[s]);
                }
            }
            #endregion

            result = Json.Encode(new
            {
                request.Form,    //gönderilen formun tamamı
                formVals,   //formdaki inputlara girilen veriler
                request.Browser.Browser,    //kullanıcının tarayıcısı
                request.Browser.IsMobileDevice,     //istek bir mobil cihazdan mı geldi
                request.Browser.Version,    //kullanıcının tarayıcı versiyonu
                request.UserAgent,      //yönlendiren kuruluşu bulmamızı sağlayan useragent bilgisi
                request.UserLanguages,  //tarayıcı dili
                request.UrlReferrer     //yönlendiren sayfayı bulmamızı sağlayan urlreferrer bilgisi
            });     //tüm bu bilgileri serialize edip stringe çeviriyorum

            return result;
        }
    }
}