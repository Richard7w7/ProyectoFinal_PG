using System;
using System.Collections.Generic;

namespace ProyectoFinal_PG.Models
{
    public partial class TbEstadosolicitudes
    {
        public TbEstadosolicitudes()
        {
            TbSolicitudes = new HashSet<TbSolicitudes>();
        }

        public int EstadosolicitudId { get; set; }
        public string EstadosNombre { get; set; }

        public virtual ICollection<TbSolicitudes> TbSolicitudes { get; set; }
    }
}
