using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class tblContactViewModel
    {
        [Key]
        public int id_contact { get; set; }
        [Required]
        public int id_client { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string first_name { get; set; }
        [Required]
        public string web_address { get; set; }
        [Required]
        public string tel { get; set; }
        [Required]
        public string puesto { get; set; }
    }
}