using PropertyManagementSystem.Helpers;
using PropertyManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using static PropertyManagementSystem.Helpers.ApplicationHelper;

namespace PropertyManagementSystem.Controllers
{
    public class DashboardController : BaseController
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            //User CurrentUserRecord = GetUserData();
            //ViewBag.AllDepartments = Database.Departments.Where(x => x.Status == EnumStatus.Enable && x.IsDeleted == false).ToList();
            //ViewBag.Request = Database.Requests.Where(x => x.IsDeleted == false).ToList();
            //var LeadsRecords = Database.Leads.AsQueryable();
            //ViewBag.HighPriority = LeadsRecords.Where(x => x.Priority == EnumPriority.High && x.IsDeleted.Equals(false)).Count();
            //ViewBag.MediumPriority = LeadsRecords.Where(x => x.Priority == EnumPriority.Medium && x.IsDeleted.Equals(false)).Count();
            //ViewBag.LowPriority = LeadsRecords.Where(x => x.Priority == EnumPriority.Low && x.IsDeleted.Equals(false)).Count();
            //ViewBag.TotalLeads = LeadsRecords.Where(x => x.IsDeleted.Equals(false)).Count();
            //ViewBag.ActiveLeads = LeadsRecords.Where(x => x.IsDeleted.Equals(false) && x.Priority != EnumPriority.Closed).Count();
            //ViewBag.ClosedLeads = LeadsRecords.Where(x => x.Priority == EnumPriority.Closed && x.IsDeleted.Equals(false)).Count();
            //ViewBag.VisitingLeads = LeadsRecords.Where(x => x.Priority == EnumPriority.VisitingOffice && x.IsDeleted.Equals(false)).OrderBy(o => o.UpdatedDateTime).ThenBy(o => o.CreatedDateTime).Take(3).ToList();
            //if (CurrentUserRecord.RoleID == EnumRole.SuperAdministrator)
            //{
            //    ViewBag.LeadRecords = LeadsRecords.Where(x => x.IsDeleted.Equals(false)).Take(3).ToList();
            //}
            //else
            //{
            //    ViewBag.LeadRecords = LeadsRecords.Where(x => x.IsDeleted.Equals(false) && x.CreatedBy.Equals(CurrentUserRecord.ID)).Take(3).ToList();
            //}
            //DateTime TodayDate = GetDateTime();
            //ViewBag.TodayTotalLeads = LeadsRecords.Where(x => DbFunctions.TruncateTime(x.CreatedDateTime) == DbFunctions.TruncateTime(TodayDate)).Count();
            //ViewBag.TodayActiveLeads = LeadsRecords.Where(x => x.Status == EnumStatus.Enable && DbFunctions.TruncateTime(x.CreatedDateTime) == DbFunctions.TruncateTime(TodayDate)).Count();
            //ViewBag.TodayClosedLeads = LeadsRecords.Where(x => x.Priority == EnumPriority.Closed && DbFunctions.TruncateTime(x.CreatedDateTime) == DbFunctions.TruncateTime(TodayDate)).Count();
            //ViewBag.TodayHighPriority = LeadsRecords.Where(x => x.Priority == EnumPriority.High && x.IsDeleted.Equals(false) && DbFunctions.TruncateTime(x.CreatedDateTime) == DbFunctions.TruncateTime(TodayDate)).Count();
            //ViewBag.TodayMediumPriority = LeadsRecords.Where(x => x.Priority == EnumPriority.Medium && x.IsDeleted.Equals(false) && DbFunctions.TruncateTime(x.CreatedDateTime) == DbFunctions.TruncateTime(TodayDate)).Count();
            //ViewBag.TodayLowPriority = LeadsRecords.Where(x => x.Priority == EnumPriority.Low && x.IsDeleted.Equals(false) && DbFunctions.TruncateTime(x.CreatedDateTime) == DbFunctions.TruncateTime(TodayDate)).Count();
          //  ViewBag.UserLeads = Database.Users.Where(x => x.IsDeleted.Equals(false) && !x.RoleID.Equals(EnumRole.SuperAdministrator)).ToList().Select(x => new UsersLeadListModel { Text = x.Name, Value = x.Requests.Count });
            return View();
        }
        public ActionResult Profiles()
        {
            User UserRecord = GetUserData();
            ProfileModel profileModel = new ProfileModel();
            profileModel.ID = UserRecord.ID;
            profileModel.Name = UserRecord.Name;
            profileModel.EmailAddress = UserRecord.EmailAddress;
            return View("Profile", profileModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Profiles(ProfileModel modelRecord, string DateOfBirth)
        {
            AjaxResponse AjaxResponse = new AjaxResponse();
            AjaxResponse.Success = false;
            AjaxResponse.Type = EnumJQueryResponseType.MessageOnly;
            AjaxResponse.Message = "Post Data Not Found";
            try
            {
                if (IsUserLogin())
                {
                    bool isAbleToUpdate = true;
                    var Record1 = Database.Users.FirstOrDefault(o => o.EmailAddress.ToLower().Equals(modelRecord.EmailAddress.ToLower()) && o.ID != modelRecord.ID && o.IsDeleted == false);
                    if (Record1 != null)
                    {
                        AjaxResponse.Type = EnumJQueryResponseType.FieldOnly;
                        AjaxResponse.FieldName = "EmailAddress";
                        AjaxResponse.Message = "Email Address already exist in our records";
                        isAbleToUpdate = false;
                    }
                    if (isAbleToUpdate)
                    {
                        var UserRecord = Database.Users.FirstOrDefault(o => o.ID == modelRecord.ID);
                        UserRecord.Name = modelRecord.Name;
                        UserRecord.EmailAddress = modelRecord.EmailAddress;
                        if (!string.IsNullOrWhiteSpace(modelRecord.Password))
                        {
                            UserRecord.Password = Encrypt(modelRecord.Password);
                        }
                        AddSession(Session_User_Login, UserRecord);
                        Database.SaveChanges();
                        AjaxResponse.Type = EnumJQueryResponseType.MessageAndReloadWithDelay;
                        AjaxResponse.Message = "Profile Updated Successfully";
                        AjaxResponse.Success = true;
                    }
                }
                else
                {
                    AjaxResponse.Type = EnumJQueryResponseType.MessageAndRedirectWithDelay;
                    AjaxResponse.Message = "Session Expired";
                    AjaxResponse.TargetURL = ViewBag.WebsiteURL;
                }
            }
            catch (Exception ex)
            {
                AjaxResponse.Message = ex.Message;
            }
            return Json(AjaxResponse, "json");
        }
        public ActionResult Logout()
        {
            RemoveSession(Session_User_Login);
            return RedirectToAction("index", "account");
        }
        public ActionResult AccessUnauthorized()
        {
            return View("Unauthorized");
        }
    }
}