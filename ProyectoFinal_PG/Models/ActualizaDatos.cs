using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_PG.Models
{
    public class ActualizaDatos
    {
        [Display(Name = "Numero de telefono")]
        public string EmpleadoTelefono { get; set; }

        [Display(Name = "Direccion de Residencia")]
        public string EmpleadoDireccion { get; set; }
    }
}
