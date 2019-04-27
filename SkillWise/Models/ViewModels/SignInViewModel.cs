using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkillWise.Models.ViewModels
{
    public class SignInViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter your email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
