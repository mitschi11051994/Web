using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class tblReunionViewModel
    {
        [Key]
        public int id_reunion { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public DateTime fecha_y_hora { get; set; }
        [Required]
        public int id_user { get; set; }
        [Required]
        public bool esVirtual { get; set; }
        [Required]
        public int id_client { get; set; }
    }
}