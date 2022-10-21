using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_PG.Models
{
    public partial class TbTipossolicitudes
    {
        public TbTipossolicitudes()
        {
            TbSolicitudes = new HashSet<TbSolicitudes>();
        }

        public int TiposolicitudId { get; set; }
        [Display(Name = "Tipo de solicitud")]
        public string TiposolicitudNombre { get; set; }

        public virtual ICollection<TbSolicitudes> TbSolicitudes { get; set; }
    }
}
