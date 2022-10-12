using System;
using System.Collections.Generic;

namespace ProyectoFinal_PG.Models
{
    public partial class TbSolicitudes
    {
        public int SolicitudId { get; set; }
        public int TiposolicitudId { get; set; }
        public int EstadosolicitudId { get; set; }
        public int EmpleadoId { get; set; }
        public int PeriodoId { get; set; }
        public DateOnly SolicitudFecha { get; set; }
        public string SolicitudDetalles { get; set; }
        public string SolicitudFechasSeleccionadas { get; set; }
        public int SolicitudCantidadDias { get; set; }
        public string SolicitudPeriodoVacas { get; set; }
        public string SolicitudComentario { get; set; }
        public string SolicitudEstadoSeleJefe { get; set; }
        public string SolicitudEstadoDirector { get; set; }
        public string SolicitudEstadoRrHh { get; set; }

        public virtual TbEmpleados Empleado { get; set; }
        public virtual TbEstadosolicitudes Estadosolicitud { get; set; }
        public virtual TbPeriodos Periodo { get; set; }
        public virtual TbTipossolicitudes Tiposolicitud { get; set; }
    }
}
