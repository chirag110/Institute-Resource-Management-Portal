using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmptyProject.Models
{
    public class User
    {
        [Key]
        public int User_Id { get; set; }

        public string Inst_Code { get; set; }

        public string Inst_Name { get; set; }

        public string Inst_Address { get; set; }

        [Required]
        [EmailAddress]
        public string User_Email { get; set; }

        [Required]
        public string Fname { get; set; }

        [Required]
        public string Lname { get; set; }

        [Required]
        public string Mobile { get; set; }

        public Inst RegisterAs { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Passw { get; set; }
    }
}