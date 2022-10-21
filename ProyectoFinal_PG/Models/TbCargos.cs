using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_PG.Models
{
    public partial class TbCargos
    {
        public TbCargos()
        {
            TbEmpleados = new HashSet<TbEmpleados>();
        }

        public int? CargoId { get; set; }
        public int DeptoId { get; set; }
        [Display(Name ="Nombre del cargo laboral")]
        public string CargoNombre { get; set; }
        public string CargoDescripcion { get; set; }

        public virtual TbDepartamentosLaborales Depto { get; set; }
        public virtual ICollection<TbEmpleados> TbEmpleados { get; set; }
    }
}
