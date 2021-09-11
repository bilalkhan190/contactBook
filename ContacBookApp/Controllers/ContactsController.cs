using ContacBookApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContacBookApp.Controllers
{
    public class ContactsController : BaseController
    {
        // GET: Contacts
        public ActionResult Index()
        {
            FillDropDown();
            return View("AddContacts");
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