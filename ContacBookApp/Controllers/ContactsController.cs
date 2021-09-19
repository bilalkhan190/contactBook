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
            //services = new ContactServices();
        }
        // GET: Contacts
        public ActionResult Index()
        {
            ContactMeta contactMeta = new ContactMeta();
            return View("ContactList");
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

            using (var Context = new ContactEntities())
            {
                using(var Transaction = context.Database.BeginTransaction())
                {
                    
                    try
                    {
                        
                        ajaxResponse.Message = "Your Contact succuessfully Created";
                        ajaxResponse.Type = EnumJQueryResponseType.MessageOnly;
                        ajaxResponse.Status = true;
                        ajaxResponse.RedirectURL = ViewBag.WebsiteURL + "Contacts";
                        ContactMaster contact = new ContactMaster()
                        {
                            FullName = model.FullName,
                            ContactEmails = model.lstContactEmails,
                            Website = model.Website,
                            ContactPhones = model.lstContactPhones,
                            CompanyName = model.CompanyName,
                            JobTitle = model.JobTitle,
                            CreatedDate = DateTime.Now,
                            Status = model.Status,
                            
                        };
                        services.createContact(contact);                      
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
              
            }

            return Json(ajaxResponse, JsonRequestBehavior.AllowGet);
        }



        public void FillDropDown()
        {
            using(var context = new ContactEntities())
            {
               ViewBag.Categories = context.Categories.ToList();
            }

        }
    }
}