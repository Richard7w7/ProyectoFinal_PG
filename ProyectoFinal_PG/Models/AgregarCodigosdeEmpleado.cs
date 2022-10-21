using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_PG.Models
{
    public class AgregarCodigosdeEmpleado
    {
        [Display(Name ="Código de Empleado")]
        public string CodigodeEmpleado { get; set; }
    }
}
