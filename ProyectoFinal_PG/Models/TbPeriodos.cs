using System;
using System.Collections.Generic;

namespace ProyectoFinal_PG.Models
{
    public partial class TbPeriodos
    {
        public TbPeriodos()
        {
            TbSolicitudes = new HashSet<TbSolicitudes>();
        }

        public int PeriodoId { get; set; }
        public int EmpleadoId { get; set; }
        public string PeriodoVacacional { get; set; }
        public int PeriodoCantidadDiasPeriodo { get; set; }
        public string PeriodoObservaciones { get; set; }

        public virtual TbEmpleados Empleado { get; set; }
        public virtual ICollection<TbSolicitudes> TbSolicitudes { get; set; }
    }
}
