using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmptyProject.Models
{
    public class Institute
    {
        [Key]
        public int Inst_Id { get; set; }
        [Required]
        public string Inst_Code { get; set; }
        [Required]
        public string Inst_Name { get; set; }
        [Required]
        public string Inst_Address { get; set; }
    }
}