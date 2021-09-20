using ContacBookApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContacBookApp.DAL.EntityFrameWork
{
    public class EntityMapper
    {
        protected readonly ContactBook context;
        public EntityMapper()
        {
            context = new ContactBook();
        }

        public void Save()
        {
            context.SaveChanges();
        }

    }
}