using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_PG.Models
{
    public class AgregarCargosViewModel: TbEmpleados
    {
        [Display(Name ="Nombre del Cargo")]
        public string CargoNombreView { get; set; }
        [Display(Name = "Descripción del Cargo")]
        public string CargoDescripcionView { get; set; }
        [Display(Name = "Nombre del Departamento")]
        public int DeptoIdView { get; set; }
        public IEnumerable<SelectListItem>? tbDepartamentosSelect { get; set; }
        
    }
}
