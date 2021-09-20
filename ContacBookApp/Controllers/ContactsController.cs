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
    public class ContactsController : BaseController
    {
       readonly IContactServices services;
        public ContactsController()
        {
            services = new ContactServices();
        }
        // GET: Contacts
        public ActionResult Index()
        {
            
            return View("ContactList", services.getContacts());
        }
        public ActionResult Add()
        {
            FillDropDown();
            return View("AddContact");
        }

        [HttpPost]
        public ActionResult Add(ContactMeta model)
        {
            AjaxReponse ajaxResponse = new AjaxReponse();

            using(var Transaction = context.Database.BeginTransaction())
            {
                    
                try
                {
                        
                    ajaxResponse.Message = "Your Contact succuessfully Created";
                    ajaxResponse.Type = EnumJQueryResponseType.MessageAndRedirectWithDelay;
                    ajaxResponse.Status = true;
                    ajaxResponse.RedirectURL = ViewBag.WebsiteURL + "Contacts";

                    var currentUserData = GetUserData();
                    ContactMaster contactMaster = new ContactMaster()
                    {
                        FullName = model.FullName,
                        NickName = model.NickName,
                        Website = model.Website,
                        CompanyName = model.CompanyName,
                        JobTitle = model.JobTitle,
                        CreatedBy = currentUserData.ID,
                        CreatedDate = DateTime.Now,
                        Status = model.Status,
                            
                    };
                    if (model.lstContactEmails.Count > 0)
                    {
                        foreach (var emailRecord in model.lstContactEmails)
                        {
                            ContactEmail contactEmailRecord = new ContactEmail();
                            contactEmailRecord.CategoryId = emailRecord.CategoryId;
                            contactEmailRecord.ContactMasterId = contactMaster.ID;
                            contactEmailRecord.EmailAddress = emailRecord.EmailAddress;
                            services.createContactEmail(contactEmailRecord);
                        }
                    }
                    if (model.lstContactPhones.Count > 0)
                    {
                        foreach (var PhoneRecords in model.lstContactPhones)
                        {
                            ContactPhone contactPhone = new ContactPhone();
                            contactPhone.CategoryId = PhoneRecords.CategoryId;
                            contactPhone.ContactMasterId = contactMaster.ID;
                            contactPhone.Phone = PhoneRecords.Phone;
                            services.createContactPhone(contactPhone);
                        }
                    }
                    services.createContact(contactMaster);
                    services.Save();
                    Transaction.Commit();

                }
                catch (Exception ex)
                {
                    ajaxResponse.Message = ex.Message;
                    ajaxResponse.Type = EnumJQueryResponseType.MessageOnly;
                    ajaxResponse.Status = false;
                    Transaction.Rollback();
                }
            }
              
           

            return Json(ajaxResponse, JsonRequestBehavior.AllowGet);
        }



        public void FillDropDown()
        {
            using(var context = new ContactBook())
            {
               ViewBag.Categories = context.Categories.ToList();
            }

        }
    }
}