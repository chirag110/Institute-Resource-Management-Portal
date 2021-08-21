using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmptyProject.Models
{

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Institute Code")]
        public string Inst_Code { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        public Inst RegisterAs { get; set; }
        [Required]
        [Display(Name = "Institute Name")]
        public string Inst_Code { get; set; }
        //[NotMapped]
        //public List<Institute> Inst { get; set; }


        [Required]
        [Display(Name = "First name")]
        public string Fname { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string Lname { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Mobile No.")]
        public string Mobile { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
