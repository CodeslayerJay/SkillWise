using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkillWise.Models.ViewModels
{
    public class SignUpViewModel
    {
        [DataType(DataType.Text), Display(Name = "Full name")]
        [StringLength(60, MinimumLength = 2), Required(ErrorMessage = "Please enter your full name.")]
        public string FullName { get; set; }

        [StringLength(100, MinimumLength = 5), EmailAddress, Required(ErrorMessage = "Please enter your email address.")]
        public string Email { get; set; }
        
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter a new password.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Please confirm your password.")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        
    }
}

