using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace belt1.Models
{
    public class UserViewModel: BaseEntity
    {
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [CustomPasswordType(ErrorMessage = "Password must contain at least one uppercase letter, one number, and one special character")]
        //ADD VALIDATION FOR 1 LETTER 1 NUMBER AND A SPECIAL CHARACTER
        public string Password { get; set; }

        [Required]
        [MinLength(8)]
        [Compare("Password", ErrorMessage = "Password and Password Confirmation must match")]
        [Display(Name = "PasswordConfirmation")]        
        public string PasswordConfirmation { get; set; }
    }

    public class CustomPasswordTypeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string password = (string)value;
            if(password.Any(char.IsUpper) && password.Any(char.IsNumber)) {
                return true;
            } else {
                return false;
            }
        }
    }  



}