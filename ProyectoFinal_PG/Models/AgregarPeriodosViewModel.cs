using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_PG.Models
{
    public class AgregarPeriodosViewModel: TbEmpleados
    {
        [Display(Name ="Codigo del empleado")]
        public int empleado_idView { get; set; }
        [Display(Name = "Periodo Vacacional a asignar")]
        public string periodo_vacacionalView { get; set; }
        [Display(Name = "Cantidad de dias a asignar")]
        public int periodo_cantidad_diasView { get; set; }
        [Display(Name = "Observaciones del periodo")]
        public string periodo_observacionesView { get; set; }
        public IEnumerable<SelectListItem>? tbempleadosView { get; set; }
    }
}
