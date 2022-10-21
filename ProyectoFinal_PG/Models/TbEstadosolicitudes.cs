using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_PG.Models
{
    public partial class TbEstadosolicitudes
    {
        public TbEstadosolicitudes()
        {
            TbSolicitudes = new HashSet<TbSolicitudes>();
        }

        public int EstadosolicitudId { get; set; }
        [Display(Name = "Estado de la solicitud")]
        public string EstadosNombre { get; set; }

        public virtual ICollection<TbSolicitudes> TbSolicitudes { get; set; }
    }
}
