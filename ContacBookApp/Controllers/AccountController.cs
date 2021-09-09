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
        private readonly ContactEntities context;
        private readonly IAccountManager _AccountManager;
        public AccountController()
        {
            context = new ContactEntities();
            _AccountManager = new AccountManager();
        }
        public ActionResult Index()
        {

            return View("Login");
        }

        public ActionResult Login(LoginMeta model)
        {
            AjaxReponse ajaxResponse = new AjaxReponse();
            ajaxResponse.Status = true;
            ajaxResponse.Message = "Login successfull. redirecting...";
            ajaxResponse.Type = EnumJQueryResponseType.MessageAndRedirectWithDelay;
            var record = _AccountManager.ValidateUser(model.EmailAddress, Encrypt(model.Password));
            if (record != null)
            {
                AddCookie("CurrentUserId", record.ID.ToString());
                AddCookie("Role",record.ID.ToString());
            }
            else
            {
                ajaxResponse.Status = false;
                ajaxResponse.Message = "something went wrong.";
                ajaxResponse.Type = EnumJQueryResponseType.MessageOnly;
            }
            return Json(ajaxResponse,JsonRequestBehavior.AllowGet);
        }
    }
}