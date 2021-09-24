using ContacBookApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ContacBookApp.Helper.ApplicationHelper;

namespace ContacBookApp.DAL.EntityFrameWork
{
    public class ContactServices : EntityMapper, IContactServices
    {
        public void createContact(ContactMaster model)
        {
            context.ContactMasters.Add(model);
                     
        }

        public void createContactEmail(ContactEmail model)
        {
            context.ContactEmails.Add(model);
           
        }

        public void createContactPhone(ContactPhone model)
        {
            context.ContactPhones.Add(model);
        }

        public ContactMaster getById(int id)
        {
            return context.ContactMasters.Where(x => x.Status.Equals(EnumStatus.Enable) && x.IsDeleted.Equals(false) && x.
                ID.Equals(id)).FirstOrDefault();
        }

        public IEnumerable<ContactMaster> getContacts()
        {
            return context.ContactMasters.Where(x => x.Status.Equals(EnumStatus.Enable) && x.
                IsDeleted.Equals(false)).ToList();
        }

        public ContactMeta GetUserCompleteRecord(int Id)
        {
            ContactMeta UserContactRecord = context.ContactMasters.Where(x => x.ID.Equals(Id))
                .Select(s => new ContactMeta { ID = s.ID , CompanyName = s.CompanyName, FullName = s.FullName,JobTitle = s.JobTitle ,
                NickName = s.NickName , Status = s.Status,Website = s.Website , lstContactEmails = s.ContactEmails.ToList() ,
                lstContactPhones = s.ContactPhones.ToList()}).FirstOrDefault();
            return UserContactRecord;
                
        }
    }
}