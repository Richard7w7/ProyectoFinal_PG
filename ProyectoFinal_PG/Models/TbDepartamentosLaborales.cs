using System;
using System.Collections.Generic;

namespace ProyectoFinal_PG.Models
{
    public partial class TbDepartamentosLaborales
    {
        public TbDepartamentosLaborales()
        {
            TbCargos = new HashSet<TbCargos>();
            TbEmpleados = new HashSet<TbEmpleados>();
        }

        public int DeptoId { get; set; }
        public string DeptoNombre { get; set; }
        public string DeptoDescripcion { get; set; }

        public virtual ICollection<TbCargos> TbCargos { get; set; }
        public virtual ICollection<TbEmpleados> TbEmpleados { get; set; }
    }
}
