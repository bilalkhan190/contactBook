using ContacBookApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContacBookApp.DAL.EntityFrameWork
{
    public class EntityMapper
    {
        protected readonly ContactEntities context;
        public EntityMapper()
        {
            context = new ContactEntities();
        }
    }
}