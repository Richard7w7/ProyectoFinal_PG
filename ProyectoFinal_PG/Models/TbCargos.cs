using System;
using System.Collections.Generic;

namespace ProyectoFinal_PG.Models
{
    public partial class TbCargos
    {
        public TbCargos()
        {
            TbEmpleados = new HashSet<TbEmpleados>();
        }

        public int CargoId { get; set; }
        public int DeptoId { get; set; }
        public string CargoNombre { get; set; }
        public string CargoDescripcion { get; set; }

        public virtual TbDepartamentosLaborales Depto { get; set; }
        public virtual ICollection<TbEmpleados> TbEmpleados { get; set; }
    }
}
