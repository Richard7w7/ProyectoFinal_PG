using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_PG.Models
{
    public class RecuperarContraseñaModel
    {
        [Display(Name ="Codigo de Colaborador")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string CodigoEmpleado { get; set; }
        [Display(Name = "Numero de Telefono")]
        [Required(ErrorMessage = "El campo {0} es requerido")] 
        public string TelefonoEmpleado { get; set; }
        [Display(Name = "Fecha de Nacimiento")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string ContrasenaHash { get; set; }
    }
}
