using ShopManagementSystem.WebUI.Extensions.Log;
using ShopManagementSystem.WebUI.Extensions.Security;
using ShopManagementSystem.WebUI.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopManagementSystem.WebUI.Controllers
{
    public class SecurityController : Controller
    {
        private readonly IUnitOfWork uow;
        public SecurityController(IUnitOfWork _uow)
        {
            uow = _uow;
        }
        // GET: Security
        public ActionResult Index()
        {
            return View();
        }


        [AllowAnonymous]
        [Log]
        public ActionResult VerifyAccount()
        {
            try
            {
                var token = Request.QueryString["token"].ToString();
                var shopId = Request.QueryString["sid"].ToString();
                if (string.IsNullOrEmpty(shopId))
                {
                    TempData["ResponseStatus"] = "NonUsableAccount";
                    return RedirectToAction("login", "security",new { Area="Admin"});
                }
                if (string.IsNullOrEmpty(token))
                {
                    TempData["ResponseStatus"] = "EmptyToken";
                    return RedirectToAction("login", "security", new { Area = "Admin" });
                }
                var user = uow.Users.GetByShopId(Convert.ToInt32(shopId));
                if (user != null && HashString.MD5Hash(user.AuthenticationCode) == token)
                {
                    TempData["ResponseStatus"] = "SuccessVerification";
                    user.AuthenticationCode = null;
                    user.IsAuthenticated = true;
                    uow.Users.Edit(user);
                    uow.SaveChanges();
                    return RedirectToAction("login", "security", new { Area = "Admin" });
                }
                else
                {
                    TempData["ResponseStatus"] = "NotSuccessVerification";
                    return RedirectToAction("login", "security", new { Area = "Admin" });
                }
            }
            catch (Exception)
            {
                TempData["ResponseStatus"] = "VerificationError";
                return RedirectToAction("login", "security", new { Area = "Admin" });
            }


        }

        //[HttpPost]
        //[AllowAnonymous]
        //public ActionResult CreateAccount(StudentCreateAccountModel studentEntity)
        //{
        //    List<SelectListItem> EducationStatuses = uow.EducationStatuses.SetEducationStatusDropdownList();
        //    ViewData["EducationStatuses"] = EducationStatuses;
        //    if (
        //        studentEntity.Ssn == null || studentEntity.Password == null || studentEntity.Name == null || studentEntity.Surname == null ||
        //        studentEntity.Address == null || studentEntity.Email == null || studentEntity.Phone == null || studentEntity.EducationStatusID == null
        //      ) return Json(new { Url = "/student/security/login", Status = "None" });
        //    else if ((studentEntity.Phone.Length != 11))
        //    {
        //        return Json(new { Url = "/student/security/login", Status = "CorrectThePhone" });
        //    }
        //    else if (!(studentEntity.Password.Length > 5 && studentEntity.Password.Length < 11))
        //    {
        //        return Json(new { Url = "/student/security/login", Status = "TooLong" });
        //    }
        //    else if (!studentEntity.IsAccepted)
        //    {
        //        return Json(new { Url = "/student/security/login", Status = "NotAccepted" });
        //    }
        //    else if (ModelState.IsValid)
        //    {
        //        var student = new Students();
        //        if (studentEntity != null)
        //        {
        //            if (uow.Students.HasSameRecords(studentEntity))
        //            {
        //                return Json(new { Url = "/student/security/login", Status = "Warning" });
        //            }

        //            if (Validation.SsnValidation(studentEntity.Ssn, studentEntity.Name, studentEntity.Surname, studentEntity.BirthdayDate.Year) == null)
        //            {
        //                return Json(new { Url = "/student/security/login", Status = "Fail" });
        //            }
        //            else if (Validation.SsnValidation(studentEntity.Ssn, studentEntity.Name, studentEntity.Surname, studentEntity.BirthdayDate.Year) == false)
        //            {
        //                return Json(new { Url = "/student/security/login", Status = "NotValid" });
        //            }
        //            else
        //            {
        //                student.Ssn = studentEntity.Ssn;
        //                student.RoleID = 3;
        //                student.Name = studentEntity.Name.ToUpper();
        //                student.Surname = studentEntity.Surname.ToUpper();
        //                student.Address = studentEntity.Address;
        //                student.Email = studentEntity.Email;
        //                student.Phone = studentEntity.Phone;
        //                student.Gender = studentEntity.Gender;
        //                //student.MaritalStatus = studentEntity.MaritalStatus;
        //                student.MaritalStatus = false;
        //                student.EducationStatusID = Convert.ToInt32(studentEntity.EducationStatusID);
        //                student.Password = studentEntity.Password;
        //                student.IsHandicapped = studentEntity.IsHandicapped;
        //                student.IsMartyrOrVeteranRelative = studentEntity.IsMartyrOrVeteranRelative;
        //                student.IsActive = true;
        //                //student.IsAuthenticated = false;
        //                student.IsAuthenticated = true;
        //                student.BirthdayDate = studentEntity.BirthdayDate;
        //                var authenticationCode = CreateAuthenticationCode();
        //                student.AuthenticationCode = authenticationCode;
        //                var authenticationCodeHash = HashString.MD5Hash(authenticationCode);
        //                student.IsRemember = false;

        //                uow.Students.Add(student);
        //                uow.SaveChanges();

        //                EmailModel em = new EmailModel()
        //                {
        //                    SenderEmail = "kaymekteknikdestek@gmail.com",
        //                    ReceiverEmail = student.Email,
        //                    SenderName = "Kaymek Teknik Destek Ekibi",
        //                    MailTitle = "Kaymek Online Hesap Onaylama İşlemi",
        //                    MailServer = "smtp.gmail.com",
        //                    MailPassword = "9IwcmHrxVt2q3XnO",
        //                    isSecure = true,
        //                    isUseDefaultCredentials = false,
        //                    Message = "https://www.kaymekonline.com/student/security/verifyaccount?ssn=" + student.Ssn + "&token=" + authenticationCodeHash,
        //                    Port = 587
        //                };
        //                try
        //                {
        //                    //Email email = new Email();
        //                    //email.SendForAccountAuthentication(em);
        //                    return Json(new { Url = "/student/security/login", Status = "Success" });
        //                }
        //                catch (Exception)
        //                {
        //                    return Json(new { Url = "/student/security/login", Status = "MailNotSend" });
        //                }

        //            }
        //        }
        //        else
        //        {
        //            return Json(new { Url = "/student/security/login", Status = "Fail" });
        //        }
        //    }
        //    return View(studentEntity);
        //}

       
    }
}