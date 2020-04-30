using SorubankCMS.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;

namespace SorubankCMS.Data.Entity
{

    public class CMSUser : BaseEntity
    {

        //TODO MUST BE REQUIRED.
        public string RegistrationNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public string PictureUrl { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime PasswordChangedDate { get; set; }
        public string LanguagePreference { get; set; }
        public int? TenantId { get; set; }
        public string ForgotPasswordToken { get; set; }
        public virtual Tenant Tenant { get; set; }
    }
}