//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class tblClient
    {
        public int id_client { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string web_page { get; set; }
        [Required]
        public string direccion { get; set; }
        [Required]
        public string tel { get; set; }
        [Required]
        public string puesto { get; set; }
    }
}
