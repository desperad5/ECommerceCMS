using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Models.Api
{
    public class SocialLoginViewModel
    {
        //TODO:AuthsocialId
        public string UserId { get; set; }


        public string FirstName { get; set; }


        public string LastName { get; set; }


        public string EmailAddress { get; set; }


        public string PictureUrl { get; set; }

        public string Provider { get; set; }

        public string Password { get; set; }
    }
}
