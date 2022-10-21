using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_PG.Models
{
    public partial class TbPeriodos
    {
        public TbPeriodos()
        {
            TbSolicitudes = new HashSet<TbSolicitudes>();
        }

        public int? PeriodoId { get; set; }
        public int EmpleadoId { get; set; }
        [Display(Name = "Periodo vacacional")]
        public string PeriodoVacacional { get; set; }
        [Display(Name = "Cantidad de días asignados al periodo vacacional")]
        public int PeriodoCantidadDiasPeriodo { get; set; }
        [Display(Name = "Observaciones del periodo")]
        public string PeriodoObservaciones { get; set; }

        public virtual TbEmpleados Empleado { get; set; }
        public virtual ICollection<TbSolicitudes> TbSolicitudes { get; set; }
    }
}
