using ContacBookApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContacBookApp.DAL.EntityFrameWork
{
    public class AccountManager : EntityMapper,IAccountManager
    {
        public void CreateUser(User model)
        {
            context.Users.Add(model);
           
        }

       
        public User ValidateUser(string EmailAddress, string encryptedPassword)
        {
            return context.Users.FirstOrDefault(x => x.Email.Equals(EmailAddress) && x.Password.Equals(encryptedPassword));
        }
    }
}