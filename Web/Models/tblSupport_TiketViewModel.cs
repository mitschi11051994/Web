using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class tblSupport_TiketViewModel
    {
        [Key]
        public int id_Support_Tickets { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string detalle { get; set; }
        [Required]
        public int id_user { get; set; }
        [Required]
        public int id_client { get; set; }
        [Required]
        public string estado { get; set; }
    }
}