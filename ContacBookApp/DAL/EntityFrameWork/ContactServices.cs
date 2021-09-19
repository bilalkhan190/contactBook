using ContacBookApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContacBookApp.DAL.EntityFrameWork
{
    public class ContactServices : EntityMapper, IContactServices
    {
        public void createContact(ContactMaster model)
        {
            context.ContactMasters.Add(model);
            Save();           
        }
    }
}