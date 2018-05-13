using Blogging_Times.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blogging_Times.Web.Models
{
    public class ResetPasswordViewModel 
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Present Password")]
        public string PresentPassword { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "The New Password must be 6-15 characters long.")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}