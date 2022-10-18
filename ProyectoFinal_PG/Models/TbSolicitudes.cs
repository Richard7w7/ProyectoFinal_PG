using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinal_PG.Models
{
    public partial class TbSolicitudes
    {
        public int? SolicitudId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Tipo de Solicitud")]
        public int TiposolicitudId { get; set; }
        public int EstadosolicitudId { get; set; }
        public int? EmpleadoId { get; set; }
        public int? PeriodoId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SolicitudFecha { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Detalle la solicitud")]
        public string SolicitudDetalles { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Fechas Seleccionadas")]
        public string SolicitudFechasSeleccionadas { get; set; }
        public int SolicitudCantidadDias { get; set; }
        [Display(Name = "Periodo del cual se tomaran los días a vacacionar")]
        public string SolicitudPeriodoVacas { get; set; }
        public string SolicitudComentario { get; set; }
        public string SolicitudEstadoSeleJefe { get; set; }
        public string SolicitudEstadoDirector { get; set; }
        public string SolicitudEstadoRrHh { get; set; }
        public int solicitud_depto_Id { get; set; }
        [NotMapped]
        public string nombreEmpleado { get; set; }
        [NotMapped]
        public string apellidoEmpleado { get; set; }
        [NotMapped]
        public string departamentoNombre { get; set; }
        public virtual TbEmpleados Empleado { get; set; }
        public virtual TbEstadosolicitudes Estadosolicitud { get; set; }
        public virtual TbPeriodos Periodo { get; set; }
        public virtual TbTipossolicitudes Tiposolicitud { get; set; }
    }
}
