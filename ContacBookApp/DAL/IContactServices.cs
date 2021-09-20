using ContacBookApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContacBookApp.DAL
{
  public interface IContactServices
    {
        void createContact(ContactMaster model);
        void createContactEmail(ContactEmail model);

        void createContactPhone(ContactPhone model);
        IEnumerable<ContactMaster> getContacts();
        ContactMaster getById(int id);

        void Save();

    }
}
