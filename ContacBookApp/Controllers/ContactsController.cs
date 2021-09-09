using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContacBookApp.Controllers
{
    public class ContactsController : Controller
    {
        // GET: Contacts
        public ActionResult AddContacts()
        {
            return View();
        }
    }
}