
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PropertyManagementSystem.Models
{
    public class CustomerModel
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }
    public class UsersLeadListModel
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }
    public class StatusModel
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
    public class PriorityModel
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
    public class SourceModel
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
    public class ServicesModel
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
    public class BreadCrumbMenu
    {
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string AccessURL { get; set; }
    }
    public class LoginModel
    {
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Password { get; set; }
    }
    public class ProfileModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
    public class RoleModel
    {
        public int? ID { get; set; }
        [Required(ErrorMessage = "Required")]
        public int BranchID { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }
        public string Status { get; set; }
    }
    public class UserModel
    {
        public int? ID { get; set; }
        public int? BranchID { get; set; }
        [Required(ErrorMessage = "Required")]
        public int RoleID { get; set; }
        [Required(ErrorMessage = "Required")]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Required")]


        public string Name { get; set; }
        [Required(ErrorMessage = "Required")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Password { get; set; }
        public string ProfileImage { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Status { get; set; }
        [Required(ErrorMessage = "Required")]
        public bool IsDeleted { get; set; }
        [Required(ErrorMessage = "Required")]
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public DateTime DeletedDateTime { get; set; }
        [Required(ErrorMessage = "Required")]
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public int DeletedBy { get; set; }
    }
 

    public class DepartmentModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Required")]
        public string DepartmentName { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Status { get; set; }
        [Required(ErrorMessage = "Required")]
        public bool IsDeleted { get; set; }
        [Required(ErrorMessage = "Required")]
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public DateTime DeletedDateTime { get; set; }
        [Required(ErrorMessage = "Required")]
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public int DeletedBy { get; set; }
    }

    public class DepartmentPersonModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Required")]
        public string PersonName { get; set; }

        [Required(ErrorMessage = "Required")]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Required")]
        public string DepartmentName { get; set; }

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Required")]

        [DataType(DataType.PhoneNumber)]
        public string Contact { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string OtherContact { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Status { get; set; }
        [Required(ErrorMessage = "Required")]
        public bool IsDeleted { get; set; }
        [Required(ErrorMessage = "Required")]
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public DateTime DeletedDateTime { get; set; }
        [Required(ErrorMessage = "Required")]
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public int DeletedBy { get; set; }
    }

    public class RequestModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Required")]

        public int DepartmentID { get; set; }
        [Required(ErrorMessage = "Required")]

        public int ResourceID { get; set; }
        [Required(ErrorMessage = "Required")]
        public int DepartmentPersonID { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Priority { get; set; }
    

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public TimeSpan Time { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Required")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Required")]
        public string AttachedFileName { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Status { get; set; }
        [Required(ErrorMessage = "Required")]
        public bool IsDeleted { get; set; }
        [Required(ErrorMessage = "Required")]
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public DateTime DeletedDateTime { get; set; }
        [Required(ErrorMessage = "Required")]
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public int DeletedBy { get; set; }
    }


    public class ResourceModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Required")]
        public string ResourceName { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Status { get; set; }
        [Required(ErrorMessage = "Required")]
        public bool IsDeleted { get; set; }
        [Required(ErrorMessage = "Required")]
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public DateTime DeletedDateTime { get; set; }
        [Required(ErrorMessage = "Required")]
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public int DeletedBy { get; set; }
    }
    public class ReminderModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Required")]
        public int UserID { get; set; }
        [Required(ErrorMessage = "Required")]
        public int LeadID { get; set; }
        [Required(ErrorMessage = "Required")]
        public DateTime ReminderDate { get; set; }
        [Required(ErrorMessage = "Required")]
        public string ReminderTime { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Note { get; set; }
        [Required(ErrorMessage = "Required")]
        public DateTime CreatedDateTime { get; set; }
        [Required(ErrorMessage = "Required")]
        public int CreatedBy { get; set; }
    }
    
}