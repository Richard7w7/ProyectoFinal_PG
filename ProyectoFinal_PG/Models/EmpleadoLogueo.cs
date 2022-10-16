using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_PG.Models
{
    public class EmpleadoLogueo
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Codigo de Empleado")]
        public string EmpleadoCodigo { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Contraseña")]
        public string EmpleadoContrasena { get; set; }
        public bool Recuerdame { get; set; }
    }
}
