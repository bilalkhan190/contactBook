//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ContacBookApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ContactPhone
    {
        public long ID { get; set; }
        public long ContactMasterId { get; set; }
        public long CategoryId { get; set; }
        public string Phone { get; set; }
    
        public virtual ContactMaster ContactMaster { get; set; }
    }
}
