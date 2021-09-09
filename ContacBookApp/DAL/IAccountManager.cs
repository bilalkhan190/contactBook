using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContacBookApp.Models;

namespace ContacBookApp.DAL
{
    public interface IAccountManager
    {
        User ValidateUser(string EmailAddress,string encryptedPassword);
    }
}