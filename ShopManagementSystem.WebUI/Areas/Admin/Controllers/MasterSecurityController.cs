
using ShopManagementSystem.WebUI.Repository.Abstract;
using System.Web.Mvc;
using System.Web.Security;

namespace ShopManagementSystem.WebUI.Areas.Admin.Controllers
{
    [AllowAnonymous]
    public class MasterSecurityController : Controller
    {
        private readonly IUnitOfWork uow;
        public MasterSecurityController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(UserLoginModel userEntity)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = uow.Users.GetByUsernameAndPassword(userEntity.Username, userEntity.Password);

        //        if (user != null)
        //        {
        //            if (userEntity.IsRemember)
        //            {
        //                user.IsRemember = userEntity.IsRemember;
        //                uow.Users.Edit(user);
        //                uow.SaveChanges();
        //                FormsAuthentication.SetAuthCookie(user.Username, Convert.ToBoolean(user.IsRemember));
        //            }
        //            else
        //            {
        //                FormsAuthentication.SetAuthCookie(user.Username, Convert.ToBoolean(user.IsRemember));
        //            }

        //            Session["UserID"] = user.ID;
        //            Session["FullName"] = user.FullName;
        //            Session["UserEmail"] = user.Email;
        //            Session["Role"] = user.RoleID;
        //            Session["RoleName"] = user.Roles.Name;

        //            return Json(new { Url = "/admin/home/index", Status = "Success" });
        //        }
        //        else if (userEntity.Username == null || userEntity.Password == null)
        //        {
        //            return Json(new { Url = "/admin/security/login", Status = "None" });
        //        }
        //        else
        //        {
        //            return Json(new { Url = "/admin/security/login", Status = "Fail" });
        //        }
        //    }
        //    return View(userEntity);
        //}

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/security/login");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ForgotMyPassword()
        {
            return View();
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[AllowAnonymous]
        //public ActionResult ForgotMyPassword(UserLoginModel userEntity)
        //{
        //    var user = uow.Users.GetByEmail(userEntity.Email);
        //    if (user != null)
        //    {
        //        try
        //        {
        //            string newPassword = Membership.GeneratePassword(6, 1);

        //            Email email = new Email();
        //            email.SendForNewPassword(userEntity.Email, newPassword);

        //            user.Password = newPassword;
        //            uow.Users.Edit(user);
        //            uow.SaveChanges();
        //        }
        //        catch (Exception)
        //        {
        //            return Json(new { Url = "/admin/security/login", Status = "Fail" });
        //        }
        //        return Json(new { Url = "/admin/security/login", Status = "Success" });
        //    }
        //    else if (userEntity.Email == null)
        //    {
        //        return Json(new { Url = "/admin/security/login", Status = "NonEmail" });
        //    }
        //    else
        //    {
        //        return Json(new { Url = "/admin/security/login", Status = "Fail" });
        //    }
        //}

        //[HttpGet]
        //[AllowAnonymous]
        //public ActionResult Account()
        //{
        //    var mail = (ViewData["UserEmail"]) as string;
        //    var user = uow.Users.GetByEmail(mail);
        //    if (user != null)
        //    {
        //        UserAccountModel userAccountModel = new UserAccountModel()
        //        {
        //            Email = user.Email,
        //            Password = user.Password,
        //            UserID = user.ID,
        //            FullName = user.FullName,
        //            IsRemember = user.IsRemember,
        //            CourseBranchID = user.CourseBranchID
        //        };
        //        return View(userAccountModel);
        //    }
        //    return RedirectToAction("Login", "Security");
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[AllowAnonymous]
        //[Log]
        //public ActionResult Account(UserAccountModel userEntity, string NewPassword, string VerifyPassword)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var mail = (ViewData["UserEmail"]) as string;
        //        var user = uow.Users.GetByEmail(mail);
        //        try
        //        {
        //            if (NewPassword != VerifyPassword)
        //            {
        //                return Json(new { Url = "/admin/security/account", Status = "NotSame" });
        //            }
        //            if (userEntity.FullName == null || userEntity.Email == null)
        //            {
        //                return Json(new { Url = "/admin/security/account", Status = "None" });
        //            }

        //            if (NewPassword == "" && VerifyPassword == "")
        //            {
        //                user.FullName = userEntity.FullName;
        //                user.Email = userEntity.Email;
        //                FormsAuthentication.SetAuthCookie(user.Username, Convert.ToBoolean(user.IsRemember));
        //                Session["FullName"] = userEntity.FullName;
        //                Session["UserEmail"] = userEntity.Email;
        //                uow.Users.Edit(user);
        //                uow.SaveChanges();
        //                return Json(new { Url = "/admin/security/account", Status = "Success" });
        //            }
        //            else
        //            {
        //                user.FullName = userEntity.FullName;
        //                user.Email = userEntity.Email;
        //                user.Password = VerifyPassword;
        //                FormsAuthentication.SetAuthCookie(user.Username, Convert.ToBoolean(user.IsRemember));
        //                Session["FullName"] = userEntity.FullName;
        //                Session["UserEmail"] = userEntity.Email;
        //                uow.Users.Edit(user);
        //                uow.SaveChanges();
        //                return Json(new { Url = "/admin/security/account", Status = "Success" });
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            return Json(new { Url = "/admin/security/account", Status = "Fail" });
        //        }
        //    }
        //    return View(userEntity);
        //}
        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    if (User.Identity.Name != null)
        //    {
        //        var username = User.Identity.Name;
        //        if (!string.IsNullOrEmpty(username))
        //        {
        //            var user = uow.Users.Find(u => u.Username == username).FirstOrDefault();
        //            if (user != null)
        //            {
        //                ViewData.Add("UserEmail", user.Email);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        FormsAuthentication.SignOut();
        //        RedirectToAction("Login", "Security", new { Area = "Admin" });
        //    }
        //    base.OnActionExecuting(filterContext);
        //}

    }
}