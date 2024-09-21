﻿using System.ComponentModel.DataAnnotations;

namespace Supershop.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string UserName { get; set; }
        
        
        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        
        public bool RememberMe { get; set; }
    }
}
