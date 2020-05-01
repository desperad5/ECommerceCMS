using System.ComponentModel.DataAnnotations;

namespace SorubankCMS.Data.Entity
{
    public class Customer : BaseEntity
    {

        public string SocialAuthId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        public string UserName { get; set; }

        public string PictureUrl { get; set; }

        public string Provider { get; set; }

        public string Password { get; set; }
        public string PasswordSalt { get; set; }

        public string ForgotPasswordToken { get; set; }
        public string Fullname()
        {
            return FirstName + " " + LastName;
        }
    }
}
