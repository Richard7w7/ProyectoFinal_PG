using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_PG.Models
{
    public partial class TbEmpleados
    {
        public TbEmpleados()
        {
            TbPeriodos = new HashSet<TbPeriodos>();
            TbSolicitudes = new HashSet<TbSolicitudes>();
        }

        public int? EmpleadoId { get; set; }
        [Required(ErrorMessage ="El campo {0} es requerido")]
        [Display(Name ="Codigo de Empleado")]
        public string EmpleadoCodigo { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Cargo Laboral")]
        public int CargoId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Departamento Laboral")]
        public int DeptoId { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Contraseña")]
        public string EmpleadoContrasena { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Primer Nombre")]
        public string EmpleadoNombre1 { get; set; }
        [Display(Name = "Segundo Nombre")]
        public string? EmpleadoNombre2 { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Primer Apellido")]
        public string EmpleadoApellido1 { get; set; }
        [Display(Name = "Segundo Apellido")]
        public string? EmpleadoApellido2 { get; set; }
       
        [Display(Name = "Apellido de Casada")]
        public string? EmpleadoApellidoCasada { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Fecha de Nacimiento")]
        public DateOnly EmpleadoFechaNacimiento { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Telefono Celular")]
        public string EmpleadoTelefono { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Direccion de Residencia")]
        public string EmpleadoDireccion { get; set; }
        public int EmpleadoEstadoVacional { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Primer Nombre")]
        public DateOnly FechaIngresoLaboral { get; set; }
        public virtual bool Recuerdame { get; set; }

        public virtual TbCargos Cargo { get; set; }
        public virtual TbDepartamentosLaborales Depto { get; set; }
        public virtual ICollection<TbPeriodos> TbPeriodos { get; set; }
        public virtual ICollection<TbSolicitudes> TbSolicitudes { get; set; }
    }
}
