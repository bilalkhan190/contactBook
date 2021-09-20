using ContacBookApp.DAL;
using ContacBookApp.DAL.EntityFrameWork;
using ContacBookApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ContacBookApp.Helper.ApplicationHelper;

namespace ContacBookApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ContactBook context;
        private readonly IAccountManager _AccountManager;
        public AccountController()
        {
            context = new ContactBook();
            _AccountManager = new AccountManager();
        }
        public ActionResult Index()
        {
            if (IsUserLogin())
                return RedirectToAction("","contacts");
            else
                return View("Login");

        }
        [HttpPost]
        public ActionResult Login(LoginMeta model)
        {
            AjaxReponse ajaxResponse = new AjaxReponse();
            ajaxResponse.Status = true;
            ajaxResponse.Message = "Login successfull. redirecting...";
            ajaxResponse.Type = EnumJQueryResponseType.MessageAndRedirectWithDelay;
            ajaxResponse.RedirectURL = ViewBag.WebsiteURL + "contacts";
            var record = _AccountManager.ValidateUser(model.EmailAddress, Encrypt(model.Password));
            if (record != null)
            {
                AddSession("UserRecord", record);
                if(!string.IsNullOrEmpty(model.RememberMe))
                {
                    AddCookie("EmailAddress", record.Email);
                    AddCookie("Password", record.Password);
                }           
            }
            else
            {
                ajaxResponse.Status = false;
                ajaxResponse.Message = "Invalid username or Password";
                ajaxResponse.Type = EnumJQueryResponseType.MessageOnly;
                
            }
            return Json(ajaxResponse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Register()
        {
            return View("Register");
        }
        [HttpPost]
        public ActionResult Register(RegisterMeta model)
        {
            AjaxReponse ajaxReponse = new AjaxReponse();
            ajaxReponse.Message = "Registered Successfully Redirecting...";
            ajaxReponse.Status = true;
            ajaxReponse.Type = EnumJQueryResponseType.MessageAndRedirectWithDelay;
            try
            {
                model.Password = Encrypt(model.Password);
                User user = new User();
                user.FullName = model.FullName;
                user.Email = model.Email;
                user.Password = model.Password;
                user.CreatedDate = DateTime.Now;
                user.CreatedBy = 0;
                user.Role = EnumRoles.User;
                user.Status = EnumStatus.Enable;
                user.image = string.Empty;
                _AccountManager.CreateUser(user);
                _AccountManager.Save();
            }
            catch (Exception ex)
            {

                ajaxReponse.Message = ex.Message;
                ajaxReponse.Status = false;
                ajaxReponse.Type = EnumJQueryResponseType.MessageOnly;
            }
           
           
               
            

            return View("Register");
        }

        public ActionResult SignOut()
        {
            Session.RemoveAll();
            Session.Abandon();
            RemoveCookie("EmailAddress");
            RemoveCookie("Password");            
            return RedirectToAction("","account");
        }
    }


}