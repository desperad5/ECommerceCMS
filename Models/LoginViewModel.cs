using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  ECommerceCMS.Models
{
    /// <summary>
    /// Normally, I would create a ViewModel folder but since this is a small project, I'll just leave it in the model folder.
    /// </summary>
    public class LoginViewModel
    {

        //TODO:AuthsocialId
        public string UserId { get; set; }


        public string FirstName { get; set; }


        public string LastName { get; set; }


        public string EmailAddress { get; set; }


        public string PictureUrl { get; set; }

        public string Provider { get; set; }

        public string Password { get; set; }

        public string Code { get; set; }
    }
}
