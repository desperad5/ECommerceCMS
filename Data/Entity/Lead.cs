﻿using System.ComponentModel.DataAnnotations;

namespace SorubankCMS.Data.Entity
{
    public class Lead : BaseEntity
    {

        [Required]
        public string Code { get; set; }

        [Required]
        public string Email { get; set; }

        public bool HasRegistered { get; set; }

    }
}