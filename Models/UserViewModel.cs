using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace activity_planner.Models
{
    public class UserViewModel: BaseEntity
    {
        [Required]
        [MinLength(2, ErrorMessage = "Must be at least 2 letters long")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name can only contain letters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [Required]
        [MinLength(2, ErrorMessage = "Must be at least 2 letters long")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last name can only contain letters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Must be at least 8 characters long")]
        [CustomPasswordType(ErrorMessage = "Password must contain at least one uppercase letter, one number, one special character and no spaces")]
        //ADD VALIDATION FOR 1 LETTER 1 NUMBER AND A SPECIAL CHARACTER
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password and Password Confirmation must match")]
        [Display(Name = "Password Confirmation")]        
        public string PasswordConfirmation { get; set; }
    }

    //checks for uppercase, number, special character, and no spaces
    //TODO: split this messaging into more modular pieces, so user will know exactly what they did wrong
    public class CustomPasswordTypeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string password = (string)value;
            string specialCharsRegex = "[~`!@#$%^&*()_+={[}]|\\:;\"'<,>.?/-]";
            if (password != null) {
                if(password.Any(char.IsUpper) && password.Any(char.IsNumber) && Regex.IsMatch(password, specialCharsRegex)  && !password.Contains(' ')) {
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }
        }
    }  



}