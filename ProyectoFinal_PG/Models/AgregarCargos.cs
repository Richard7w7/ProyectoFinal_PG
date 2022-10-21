using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_PG.Models
{
    public class AgregarCargos
    {

        [Display(Name = "Nombre del departamento laboral")]
        public int DeptoId { get; set; }
        [Display(Name = "Nombre del cargo laboral")]
        public string CargoNombre { get; set; }
        [Display(Name = "Descripción del cargo laboral")]
        public string CargoDescripcion { get; set; }
    }
}
