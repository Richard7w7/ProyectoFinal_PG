using System;
using System.Collections.Generic;

namespace ProyectoFinal_PG.Models
{
    public partial class TbEmpleados
    {
        public TbEmpleados()
        {
            TbPeriodos = new HashSet<TbPeriodos>();
            TbSolicitudes = new HashSet<TbSolicitudes>();
        }

        public int EmpleadoId { get; set; }
        public string EmpleadoCodigo { get; set; }
        public int CargoId { get; set; }
        public int DeptoId { get; set; }
        public string EmpleadoContrasena { get; set; }
        public string EmpleadoNombre1 { get; set; }
        public string EmpleadoNombre2 { get; set; }
        public string EmpleadoApellido1 { get; set; }
        public string EmpleadoApellido2 { get; set; }
        public string EmpleadoApellidoCasada { get; set; }
        public DateOnly EmpleadoFechaNacimiento { get; set; }
        public string EmpleadoTelefono { get; set; }
        public string EmpleadoDireccion { get; set; }
        public int EmpleadoEstadoVacional { get; set; }
        public DateOnly FechaIngresoLaboral { get; set; }

        public virtual TbCargos Cargo { get; set; }
        public virtual TbDepartamentosLaborales Depto { get; set; }
        public virtual ICollection<TbPeriodos> TbPeriodos { get; set; }
        public virtual ICollection<TbSolicitudes> TbSolicitudes { get; set; }
    }
}
