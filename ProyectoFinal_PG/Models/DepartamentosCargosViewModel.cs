using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_PG.Models
{
    public class DepartamentosCargosViewModel: TbEmpleados
    {
        
        public IEnumerable<SelectListItem>? Departamentos { get; set; }
        
        public IEnumerable<SelectListItem>? Cargos { get; set; }
    }
}
