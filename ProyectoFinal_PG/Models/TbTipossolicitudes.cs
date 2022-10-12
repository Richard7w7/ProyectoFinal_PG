using System;
using System.Collections.Generic;

namespace ProyectoFinal_PG.Models
{
    public partial class TbTipossolicitudes
    {
        public TbTipossolicitudes()
        {
            TbSolicitudes = new HashSet<TbSolicitudes>();
        }

        public int TiposolicitudId { get; set; }
        public string TiposolicitudNombre { get; set; }

        public virtual ICollection<TbSolicitudes> TbSolicitudes { get; set; }
    }
}
