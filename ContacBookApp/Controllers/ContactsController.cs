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
        public ActionResult Add(int? Id)
        {
            FillDropDown();
            ContactMeta contactMeta;
            if (Id > 0)
                contactMeta = services.GetUserCompleteRecord(Convert.ToInt32(Id));
            else
                contactMeta = new ContactMeta();
            return View("AddContact",contactMeta);
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
                    ContactMaster contactMaster = new ContactMaster();
                    if (model.ID > 0)
                    {
                        contactMaster = context.ContactMasters.FirstOrDefault(x => x.ID.Equals(model.ID));
                        contactMaster.ModifiedBy = currentUserData.ID;
                        contactMaster.ModifiedDate = DateTime.Now;
                        ajaxResponse.Message = "Your Contact succuessfully Updated";

                    }
                    contactMaster.FullName = model.FullName;
                    contactMaster.NickName = model.NickName;
                    contactMaster.Website = model.Website;
                    contactMaster.CompanyName = model.CompanyName;
                    contactMaster.JobTitle = model.JobTitle;
                    contactMaster.Status = model.Status;
                    if (!(model.ID > 0))
                    {
                        contactMaster.CreatedBy = currentUserData.ID;
                        contactMaster.CreatedDate = DateTime.Now;
                        services.createContact(contactMaster);
                    }
                    if (model.lstContactEmails.Count > 0)
                    {
                        foreach (var emailRecord in model.lstContactEmails)
                        {
                            bool isRecordWillAdded = false;
                            ContactEmail contactEmailRecord = context.ContactEmails.FirstOrDefault(x => x.ID.Equals(emailRecord.ID));
                            if(contactEmailRecord == null)
                            {
                                contactEmailRecord = new ContactEmail();
                                isRecordWillAdded = true;
                                
                            }
                            contactEmailRecord.CategoryId = emailRecord.CategoryId;
                            contactEmailRecord.ContactMasterId = contactMaster.ID;
                            contactEmailRecord.EmailAddress = emailRecord.EmailAddress;
                            if (isRecordWillAdded)
                            {                               
                                services.createContactEmail(contactEmailRecord);
                            }
                            services.Save();
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