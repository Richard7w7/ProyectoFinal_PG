using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProyectoFinal_PG.Models;

namespace ProyectoFinal_PG
{
    public partial class BD_ControlVacacionalContext : DbContext
    {
        public BD_ControlVacacionalContext()
        {
        }

        public BD_ControlVacacionalContext(DbContextOptions<BD_ControlVacacionalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbCargos> TbCargos { get; set; }
        public virtual DbSet<TbCodigosempleadosmunicipales> TbCodigosempleadosmunicipales { get; set; }
        public virtual DbSet<TbDepartamentosLaborales> TbDepartamentosLaborales { get; set; }
        public virtual DbSet<TbEmpleados> TbEmpleados { get; set; }
        public virtual DbSet<TbEstadosolicitudes> TbEstadosolicitudes { get; set; }
        public virtual DbSet<TbPeriodos> TbPeriodos { get; set; }
        public virtual DbSet<TbSolicitudes> TbSolicitudes { get; set; }
        public virtual DbSet<TbTipossolicitudes> TbTipossolicitudes { get; set; }
        public virtual DbSet<TbVacaciones> TbVacaciones { get; set; }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbCargos>(entity =>
            {
                entity.HasKey(e => e.CargoId)
                    .HasName("tb_cargos_pkey");

                entity.ToTable("tb_cargos", "muni_villanueva");

                entity.Property(e => e.CargoId).HasColumnName("cargo_id");

                entity.Property(e => e.CargoDescripcion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("cargo_descripcion");

                entity.Property(e => e.CargoNombre)
                    .IsRequired()
                    .HasMaxLength(75)
                    .HasColumnName("cargo_nombre");

                entity.Property(e => e.DeptoId).HasColumnName("depto_id");

                entity.HasOne(d => d.Depto)
                    .WithMany(p => p.TbCargos)
                    .HasForeignKey(d => d.DeptoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_depto_id_tb_departamentos_laborales");
            });

            modelBuilder.Entity<TbCodigosempleadosmunicipales>(entity =>
            {
                entity.HasKey(e => e.CodigoemId)
                    .HasName("tb_tb_codigosempleadosmunicipales_pkey");

                entity.ToTable("tb_codigosempleadosmunicipales", "muni_villanueva");

                entity.HasIndex(e => e.Codigoempleadomunicipal, "tb_codigosempleadosmunicipales_codigoempleadomunicipal_key")
                    .IsUnique();

                entity.Property(e => e.CodigoemId).HasColumnName("codigoem_id");

                entity.Property(e => e.Codigoempleadomunicipal)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("codigoempleadomunicipal");
            });

            modelBuilder.Entity<TbDepartamentosLaborales>(entity =>
            {
                entity.HasKey(e => e.DeptoId)
                    .HasName("tb_departamentos_pkey");

                entity.ToTable("tb_departamentos_laborales", "muni_villanueva");

                entity.HasIndex(e => e.DeptoNombre, "tb_departamentos_laborales_depto_nombre_key")
                    .IsUnique();

                entity.Property(e => e.DeptoId).HasColumnName("depto_id");

                entity.Property(e => e.DeptoDescripcion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("depto_descripcion");

                entity.Property(e => e.DeptoNombre)
                    .IsRequired()
                    .HasMaxLength(75)
                    .HasColumnName("depto_nombre");
            });

            modelBuilder.Entity<TbEmpleados>(entity =>
            {
                entity.HasKey(e => e.EmpleadoId)
                    .HasName("tb_empleados_pkey");

                entity.ToTable("tb_empleados", "muni_villanueva");

                entity.HasIndex(e => e.EmpleadoCodigo, "tb_empleados_empleado_codigo_key")
                    .IsUnique();

                entity.HasIndex(e => e.EmpleadoTelefono, "tb_empleados_empleado_telefono_key")
                    .IsUnique();

                entity.Property(e => e.EmpleadoId).HasColumnName("empleado_id");

                entity.Property(e => e.CargoId).HasColumnName("cargo_id");

                entity.Property(e => e.DeptoId).HasColumnName("depto_id");

                entity.Property(e => e.EmpleadoApellido1)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("empleado_apellido1");

                entity.Property(e => e.EmpleadoApellido2)
                    .HasMaxLength(25)
                    .HasColumnName("empleado_apellido2");

                entity.Property(e => e.EmpleadoApellidoCasada)
                    .HasMaxLength(25)
                    .HasColumnName("empleado_apellido_casada");

                entity.Property(e => e.EmpleadoCodigo)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("empleado_codigo");

                entity.Property(e => e.EmpleadoContrasena)
                    .IsRequired()
                    .HasColumnName("empleado_contrasena");

                entity.Property(e => e.EmpleadoDireccion)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("empleado_direccion");

                entity.Property(e => e.EmpleadoEstadoVacional).HasColumnName("empleado_estado_vacional");

                entity.Property(e => e.EmpleadoFechaNacimiento).HasColumnName("empleado_fecha_nacimiento");

                entity.Property(e => e.EmpleadoNombre1)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("empleado_nombre1");

                entity.Property(e => e.EmpleadoNombre2)
                    .HasMaxLength(25)
                    .HasColumnName("empleado_nombre2");

                entity.Property(e => e.EmpleadoTelefono)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("empleado_telefono");

                entity.Property(e => e.FechaIngresoLaboral).HasColumnName("fecha_ingreso_laboral");

                entity.HasOne(d => d.Cargo)
                    .WithMany(p => p.TbEmpleados)
                    .HasForeignKey(d => d.CargoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cargo_id_tb_emepleados_tb_cargos");

                entity.HasOne(d => d.Depto)
                    .WithMany(p => p.TbEmpleados)
                    .HasForeignKey(d => d.DeptoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_depto_id_tb_empleados_tb_departamentos");
            });

            modelBuilder.Entity<TbEstadosolicitudes>(entity =>
            {
                entity.HasKey(e => e.EstadosolicitudId)
                    .HasName("tb_estadosolicitudes_pkey");

                entity.ToTable("tb_estadosolicitudes", "muni_villanueva");

                entity.HasIndex(e => e.EstadosNombre, "tb_estadosolicitudes_estados_nombre_key")
                    .IsUnique();

                entity.Property(e => e.EstadosolicitudId).HasColumnName("estadosolicitud_id");

                entity.Property(e => e.EstadosNombre)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("estados_nombre");
            });

            modelBuilder.Entity<TbPeriodos>(entity =>
            {
                entity.HasKey(e => e.PeriodoId)
                    .HasName("tb_periodos_pkey");

                entity.ToTable("tb_periodos", "muni_villanueva");

                entity.Property(e => e.PeriodoId).HasColumnName("periodo_id");

                entity.Property(e => e.EmpleadoId).HasColumnName("empleado_id");

                entity.Property(e => e.PeriodoCantidadDiasPeriodo).HasColumnName("periodo_cantidad_dias_periodo");

                entity.Property(e => e.PeriodoObservaciones)
                    .HasMaxLength(500)
                    .HasColumnName("periodo_observaciones");

                entity.Property(e => e.PeriodoVacacional)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("periodo_vacacional");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.TbPeriodos)
                    .HasForeignKey(d => d.EmpleadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tb_periodos_tb_empleados_empleado_id");
            });

            modelBuilder.Entity<TbSolicitudes>(entity =>
            {
                entity.HasKey(e => e.SolicitudId)
                    .HasName("tb_solicitudes_pkey");

                entity.ToTable("tb_solicitudes", "muni_villanueva");

                entity.Property(e => e.SolicitudId).HasColumnName("solicitud_id");

                entity.Property(e => e.EmpleadoId).HasColumnName("empleado_id");

                entity.Property(e => e.EstadosolicitudId).HasColumnName("estadosolicitud_id");

                entity.Property(e => e.PeriodoId).HasColumnName("periodo_id");

                entity.Property(e => e.SolicitudCantidadDias).HasColumnName("solicitud_cantidad_dias");

                entity.Property(e => e.SolicitudComentario)
                    .HasMaxLength(300)
                    .HasColumnName("solicitud_comentario");

                entity.Property(e => e.SolicitudDetalles)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("solicitud_detalles");

                entity.Property(e => e.SolicitudEstadoDirector)
                    .HasMaxLength(100)
                    .HasColumnName("solicitud_estado_director");

                entity.Property(e => e.SolicitudEstadoRrHh)
                    .HasMaxLength(100)
                    .HasColumnName("solicitud_estado_rr_hh");

                entity.Property(e => e.SolicitudEstadoSeleJefe)
                    .HasMaxLength(100)
                    .HasColumnName("solicitud_estado_sele_jefe");

                entity.Property(e => e.SolicitudFecha).HasColumnName("solicitud_fecha");

                entity.Property(e => e.SolicitudFechasSeleccionadas)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("solicitud_fechas_seleccionadas");

                entity.Property(e => e.SolicitudPeriodoVacas)
                    .HasMaxLength(50)
                    .HasColumnName("solicitud_periodo_vacas");

                entity.Property(e => e.TiposolicitudId).HasColumnName("tiposolicitud_id");

                entity.HasOne(d => d.Empleado)
                    .WithMany(p => p.TbSolicitudes)
                    .HasForeignKey(d => d.EmpleadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_empleado_id_tb_solicitudes_tb_empleados");

                entity.HasOne(d => d.Estadosolicitud)
                    .WithMany(p => p.TbSolicitudes)
                    .HasForeignKey(d => d.EstadosolicitudId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_estados_id_tb_solicitudes_tb_estadosolicitudes");

                entity.HasOne(d => d.Periodo)
                    .WithMany(p => p.TbSolicitudes)
                    .HasForeignKey(d => d.PeriodoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_periodo_id_tb_solicitudes_tb_periodos");

                entity.HasOne(d => d.Tiposolicitud)
                    .WithMany(p => p.TbSolicitudes)
                    .HasForeignKey(d => d.TiposolicitudId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tiposolicitud_id_tb_solicitudes_tb_tipossolicitudes");
            });

            modelBuilder.Entity<TbTipossolicitudes>(entity =>
            {
                entity.HasKey(e => e.TiposolicitudId)
                    .HasName("tb_tipossolicitudes_pkey");

                entity.ToTable("tb_tipossolicitudes", "muni_villanueva");

                entity.HasIndex(e => e.TiposolicitudNombre, "tb_tipossolicitudes_tiposolicitud_nombre_key")
                    .IsUnique();

                entity.Property(e => e.TiposolicitudId).HasColumnName("tiposolicitud_id");

                entity.Property(e => e.TiposolicitudNombre)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("tiposolicitud_nombre");
            });

            modelBuilder.Entity<TbVacaciones>(entity =>
            {
                entity.HasKey(e => e.VacacionesId)
                    .HasName("tb_vacaciones_pkey");

                entity.ToTable("tb_vacaciones", "muni_villanueva");

                entity.HasIndex(e => e.VacacionesAnosLaborales, "tb_vacaciones_vacaciones_anos_laborales_key")
                    .IsUnique();

                entity.Property(e => e.VacacionesId).HasColumnName("vacaciones_id");

                entity.Property(e => e.VacacionesAnosLaborales)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("vacaciones_anos_laborales");

                entity.Property(e => e.VacacionesDiasAsignados).HasColumnName("vacaciones_dias_asignados");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
