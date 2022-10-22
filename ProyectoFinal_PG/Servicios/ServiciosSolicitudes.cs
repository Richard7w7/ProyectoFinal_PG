using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using ProyectoFinal_PG.Models;
using System.Collections.Generic;

namespace ProyectoFinal_PG.Servicios
{
    public interface IServiciosSolicitudes
    {
        Task<bool> ActualizarDatos(ActualizaDatos actualizaDatos, int empleadoId);
        Task<TbEmpleados> BuscarEmpleadoporCodigo(int codigoemp);
        Task<TbEmpleados> BuscarEmpleadoporID(int codigoemp);
        Task<List<TbPeriodos>> BuscarPeriodosPorId(int codigoemp);
        Task<bool> BuscarsolicitudesconEstadoEnviado(int codigoemp);
        Task<int> CrearSolicitud(TbSolicitudes modelo);
        Task<TbSolicitudes> DetalleSolicitudDepartamento(int id, TbEmpleados empleado);
        Task<bool> InsertarCargos(AgregarCargosViewModel agregarCargos);
        Task<bool> InsertarCodigo(AgregarCodigosdeEmpleado agregarCodigosdeEmpleado);
        Task<bool> InsertarDepartamento(AgregarDepartamentos agregarDepartamentos);
        Task<bool> InsertarPeriodo(AgregarPeriodosViewModel agregarPeriodos);
        Task<IEnumerable<TbSolicitudes>> ListadoSolicitudesDepartamento(int deptoId,TbEmpleados empleado);
        Task<IEnumerable<TbSolicitudes>> ListadoSolicitudesEmpleadoAprobadas(int empleadoId);
        Task<IEnumerable<TbSolicitudes>> ListadoSolicitudesEmpleadoDenegadas(int empleadoId);
        Task<IEnumerable<TbSolicitudes>> ListadoSolicitudesEmpleadoId(int empleadoId);
        Task<IEnumerable<TbSolicitudes>> ListarSolicitudesdeTodosDepartamentosparaAprobarDenegar();
        Task<bool> ModificarEstadoSolicitud(TbSolicitudes solicitud);
        Task<IEnumerable<TbEstadosolicitudes>> ObtenerEstadosSolicitudes(TbEmpleados empleado);
        Task<IEnumerable<TbPeriodos>> ObtenerPeriodosEmpleadoId(int empleadoId);
        Task<TbEmpleados> ObtenerPeriodosPorId(int empleadoId);
        Task<TbSolicitudes> ObtenerSolicitudDetalle(int? idsolicitud, int empleado);
        Task<IEnumerable<TbTipossolicitudes>> ObtenerTiposSolicitudes();
    }
    public class ServiciosSolicitudes: IServiciosSolicitudes
    {
        private readonly BD_ControlVacacionalContext db_context;
        private readonly IServicioEmpleados servicioEmpleados;
        

        public ServiciosSolicitudes(BD_ControlVacacionalContext dB_Control_Context,
            IServicioEmpleados servicioEmpleados)
        {
            this.db_context = dB_Control_Context;
            this.servicioEmpleados = servicioEmpleados;
        }

        public async Task<TbEmpleados> BuscarEmpleadoporCodigo(int codigoemp)
        {
            var empleado = new TbEmpleados();
            empleado = await db_context.TbEmpleados
                .Include(t => t.Cargo)
                .Include(t => t.Depto)
                .Where(emp => emp.EmpleadoCodigo == codigoemp.ToString()).FirstOrDefaultAsync();
            return empleado;
        }

        public async Task<TbEmpleados> BuscarEmpleadoporID(int codigoemp)
        {
            var empleado = new TbEmpleados();
            empleado = await db_context.TbEmpleados
                .Include(t => t.Cargo)
                .Where(emp => emp.EmpleadoId == codigoemp).FirstOrDefaultAsync();
            return empleado;
        }

        public async Task<List<TbPeriodos>> BuscarPeriodosPorId(int codigoemp)
        {
            List<TbPeriodos> periodos;
            periodos = await db_context.TbPeriodos.
                Where(pdo => pdo.EmpleadoId == codigoemp)
                .Where(pdo => pdo.PeriodoCantidadDiasPeriodo >0).ToListAsync();
            return periodos;
        }

        public async Task<bool> BuscarsolicitudesconEstadoEnviado(int codigoemp)
        {
            
            var solicitud = await db_context.TbSolicitudes
                .Include(soli => soli.Empleado)
                .Include(soli => soli.Tiposolicitud)
                .Include(soli => soli.Estadosolicitud)
                .Include(soli => soli.Periodo)
                .Where(soli => soli.EmpleadoId == codigoemp)
                .Where(soli => soli.EstadosolicitudId == (int)enumEstadoSolicitud.Enviada).FirstOrDefaultAsync();
            
            if(solicitud != null)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        //Metodo para obtener el periodo mas antiguo
        public async Task<TbEmpleados>ObtenerPeriodosPorId(int empleadoId)
        {
            
            var empleado = new TbEmpleados();
            List<TbPeriodos> periodos;
            string anosperiodos = null;
            empleado = await BuscarEmpleadoporID(empleadoId);
            periodos = await BuscarPeriodosPorId(empleadoId);

            for (int i = 0; i < periodos.Count(); i++)
            {
                anosperiodos += periodos[i].PeriodoVacacional.ToString() + ",";
            }
            string[] separaperiodos = anosperiodos.Split(',');
            string final = null;
            foreach (var sepa in separaperiodos)
            {
                final += sepa + "/";
            }
            string[] finali = final.Split('/');
            finali = finali.Take(finali.Length - 2).ToArray();
            int[] numeros = Array.ConvertAll(finali, n => int.Parse(n));
            var menor = numeros.Min().ToString();
            for (int i = 0; i < periodos.Count(); i++)
            {
                string ano = periodos[i].PeriodoVacacional.Substring(0, 4);
                if (menor.Equals(ano))
                {

                    empleado.Periodo = periodos[i].PeriodoVacacional;
                    empleado.cantidad_dias = periodos[i].PeriodoCantidadDiasPeriodo;
                    empleado.periodo_id= (int)periodos[i].PeriodoId;

                }
            }

            return (empleado);
        }

        public async Task<IEnumerable<TbTipossolicitudes>> ObtenerTiposSolicitudes()
        {
            return await db_context.TbTipossolicitudes.ToListAsync();

        }

        public async Task<int> CrearSolicitud(TbSolicitudes modelo)
        {
            var empleado = new TbEmpleados();
            var periodoVencer = new TbEmpleados();
            string[] cantidadDias = modelo.SolicitudFechasSeleccionadas.Split(',');
            var empleadoId = servicioEmpleados.ObtenerEmpleadoId();
            
            empleado = await BuscarEmpleadoporCodigo(empleadoId);
            int idemp = (int)empleado.EmpleadoId;
            periodoVencer = await ObtenerPeriodosPorId(idemp);
            modelo.EstadosolicitudId = (int)enumEstadoSolicitud.Enviada;
            modelo.EmpleadoId = empleado.EmpleadoId;
            modelo.PeriodoId = periodoVencer.periodo_id;
            modelo.SolicitudPeriodoVacas = periodoVencer.Periodo;
            modelo.SolicitudFecha = DateTime.Today.Date;
            modelo.SolicitudCantidadDias = cantidadDias.Length;
            if (modelo.TiposolicitudId != (int)EnumtipoSolicitud.Vacacional)
            {
                modelo.PeriodoId = null;
                modelo.SolicitudPeriodoVacas = "Ninguno";
            }
           

            db_context.Add(modelo);
            await db_context.SaveChangesAsync();
            int idsolicitud =(int) modelo.SolicitudId;
            return idsolicitud;

        }

        public async Task<TbSolicitudes> ObtenerSolicitudDetalle(int? idsolicitud,int empleado)
        {
            var solicitud = new TbSolicitudes();
            solicitud = await db_context.TbSolicitudes
                .Include(soli => soli.Empleado)
                .Include(soli => soli.Tiposolicitud)
                .Include(soli => soli.Estadosolicitud)
                .Include(soli => soli.Periodo)
                .Where(soli => soli.SolicitudId == idsolicitud)
                .Where(soli => soli.EmpleadoId == empleado).FirstOrDefaultAsync();

            return solicitud;
        }

        public async Task<IEnumerable<TbPeriodos>> ObtenerPeriodosEmpleadoId(int empleadoId)
        {
            
            var periodos = await db_context.TbPeriodos
                .Include(t => t.Empleado)
                .Where(periodo => periodo.EmpleadoId == empleadoId).ToListAsync();            
            return periodos;
        }

        public async Task<IEnumerable<TbSolicitudes>> ListadoSolicitudesEmpleadoId(int empleadoId)
        {
            var solicitudes = await db_context.TbSolicitudes
                .Include(t => t.Empleado)
                .Include(t => t.Tiposolicitud)
                .Include(t=> t.Estadosolicitud)
                .Where(t => t.EmpleadoId == empleadoId)
                .Where(t => t.EstadosolicitudId == (int)enumEstadoSolicitud.Enviada).ToListAsync();

            return solicitudes;
        }

        public async Task<IEnumerable<TbSolicitudes>> ListadoSolicitudesEmpleadoAprobadas(int empleadoId)
        {
            var solicitudes = await db_context.TbSolicitudes
                .Include(t => t.Empleado)
                .Include(t => t.Tiposolicitud)
                .Include(t => t.Estadosolicitud)
                .Where(t => t.EmpleadoId == empleadoId)
                .Where(t => t.EstadosolicitudId == (int)enumEstados.aproboDirectorRRHH).ToListAsync();

            return solicitudes;
        }

        public async Task<IEnumerable<TbSolicitudes>> ListadoSolicitudesEmpleadoDenegadas(int empleadoId)
        {
            var solicitudes = await db_context.TbSolicitudes
                .Include(t => t.Empleado)
                .Include(t => t.Tiposolicitud)
                .Include(t => t.Estadosolicitud)
                .Where(t => t.EmpleadoId == empleadoId)
                .Where(t => t.EstadosolicitudId == (int)enumEstados.DenegoJefeInmediato ||
                t.EstadosolicitudId == (int)enumEstados.DenegoDirector ||
                t.EstadosolicitudId == (int)enumEstados.DenegoDirectorRRHH
                ).ToListAsync();

            return solicitudes;
        }

        public async Task<IEnumerable<TbSolicitudes>> 
            ListadoSolicitudesDepartamento(int deptoId,TbEmpleados empleado)
        {
            
            switch (empleado.Cargo.CargoNombre)
            {
                case "Jefe Inmediato":
                     var solicitudes = await db_context.TbSolicitudes
                    .Include(t => t.Empleado)
                    .Include(t => t.Tiposolicitud)
                    .Include(t => t.Estadosolicitud)
                    .Where(t => t.solicitud_depto_Id == deptoId)
                    .Where(t => t.EstadosolicitudId==(int)enumEstados.enviada)
                    .ToListAsync();

                    return solicitudes;
                case "Director":
                    solicitudes = await db_context.TbSolicitudes
                    .Include(t => t.Empleado)
                    .Include(t => t.Tiposolicitud)
                    .Include(t => t.Estadosolicitud)
                    .Where(t => t.solicitud_depto_Id == deptoId)
                    .Where(t => t.EstadosolicitudId == (int)enumEstados.aproboJefeInmediato)
                    .ToListAsync();

                    return solicitudes;
                case "Director RRHH":
                    solicitudes = await db_context.TbSolicitudes
                    .Include(t => t.Empleado)
                    .Include(t => t.Tiposolicitud)
                    .Include(t => t.Estadosolicitud)
                    .Where(t => t.EstadosolicitudId == (int)enumEstados.aproboDirector)
                    .Where(t => t.solicitud_depto_Id == (int)enumDepartamentos.RecursosHumanos)
                    .ToListAsync();

                    return solicitudes;
                default:
                    solicitudes = await db_context.TbSolicitudes
                   .Include(t => t.Empleado)
                   .Include(t => t.Tiposolicitud)
                   .Include(t => t.Estadosolicitud)
                   .Where(t => t.solicitud_depto_Id == deptoId) 
                   .ToListAsync();

                    return solicitudes;
                    
            }

        }

        public async Task<IEnumerable<TbSolicitudes>> 
            ListarSolicitudesdeTodosDepartamentosparaAprobarDenegar()
        {
            var solicitudes = await db_context.TbSolicitudes
                  .Include(t => t.Empleado)
                  .Include(t => t.Tiposolicitud)
                  .Include(t => t.Estadosolicitud)
                  .Where(t => t.EstadosolicitudId == (int)enumEstados.aproboDirector)
                  .ToListAsync();

            return solicitudes;
        }
        public async Task<TbSolicitudes> DetalleSolicitudDepartamento(int id, TbEmpleados empleado)
        {
            var solicitud = new TbSolicitudes();
            solicitud = await db_context.TbSolicitudes
                       .Include(soli => soli.Empleado)
                       .Include(soli => soli.Tiposolicitud)
                       .Include(soli => soli.Estadosolicitud)
                       .Include(soli => soli.Periodo)
                       .Where(soli => soli.SolicitudId == id)
                       .FirstOrDefaultAsync();
            return solicitud;
            

        }

        public async Task<IEnumerable<TbEstadosolicitudes>> ObtenerEstadosSolicitudes(TbEmpleados empleado)
        {
            switch (empleado.Cargo.CargoNombre)
            {
                case "Jefe Inmediato":
                    var estados = await db_context.TbEstadosolicitudes
                      .Where(e => e.EstadosolicitudId == 
                      (int)enumEstados.aproboJefeInmediato || e.EstadosolicitudId == (int)enumEstados.DenegoJefeInmediato)
                      .ToListAsync();

                    return estados;

                case "Director":
                    estados = await db_context.TbEstadosolicitudes
                    .Where(e => e.EstadosolicitudId == 
                    (int)enumEstados.aproboDirector || e.EstadosolicitudId == (int)enumEstados.DenegoDirector)
                    .ToListAsync();

                    return estados;

                case "Director RRHH":
                    estados = await db_context.TbEstadosolicitudes
                    .Where(e => e.EstadosolicitudId == 
                    (int)enumEstados.aproboDirectorRRHH || e.EstadosolicitudId == (int)enumEstados.DenegoDirector)
                    .ToListAsync();

                    return estados;
                default:
                    estados = await db_context.TbEstadosolicitudes.ToListAsync();
                    return estados;

            }
        }

        public async Task<bool> ModificarEstadoSolicitud(TbSolicitudes solicitud)
        {
            int empleadoId = servicioEmpleados.ObtenerEmpleadoId();
            var empleado = await BuscarEmpleadoporCodigo(empleadoId);
            

            switch (empleado.Cargo.CargoNombre)
            {
                case "Jefe Inmediato":

                    var soli = await db_context.TbSolicitudes.
                                    Where(t => t.SolicitudId == solicitud.SolicitudId)
                                    .FirstOrDefaultAsync();

                    if (soli == null) return false;

                    soli.EstadosolicitudId = solicitud.EstadosolicitudId;
                    if(solicitud.EstadosolicitudId == (int)enumEstados.aproboJefeInmediato)
                    {
                        soli.SolicitudEstadoSeleJefe = "Aprobada "+empleado.EmpleadoNombre1+" "+empleado.EmpleadoApellido1;
                    }else if(solicitud.EstadosolicitudId == (int)enumEstados.DenegoJefeInmediato)
                    {
                        soli.SolicitudEstadoSeleJefe = "Denegada " + empleado.EmpleadoNombre1 + " " + empleado.EmpleadoApellido1;
                    }

                    await db_context.SaveChangesAsync();
                    return true;

                case "Director":
                     soli = await db_context.TbSolicitudes.
                                                    Where(t => t.SolicitudId == solicitud.SolicitudId).FirstOrDefaultAsync();
                    if (soli == null) return false;
                    soli.EstadosolicitudId = solicitud.EstadosolicitudId;
                    if (solicitud.EstadosolicitudId == (int)enumEstados.aproboDirector)
                    {
                        soli.SolicitudEstadoDirector = "Aprobada " + empleado.EmpleadoNombre1 + " " + empleado.EmpleadoApellido1;
                    }
                    else if (solicitud.EstadosolicitudId == (int)enumEstados.DenegoDirector)
                    {
                        soli.SolicitudEstadoDirector = "Denegada " + empleado.EmpleadoNombre1 + " " + empleado.EmpleadoApellido1;
                    }
                    await db_context.SaveChangesAsync();
                    return true;
                case "Director RRHH":
                     soli = await db_context.TbSolicitudes.
                                  Where(t => t.SolicitudId == solicitud.SolicitudId).FirstOrDefaultAsync();
                    if (soli == null) return false;
                    soli.EstadosolicitudId = solicitud.EstadosolicitudId;
                    if (solicitud.EstadosolicitudId == (int)enumEstados.aproboDirectorRRHH)
                    {
                        if(soli.SolicitudPeriodoVacas == null)
                        {
                            soli.SolicitudEstadoRrHh = "Aprobada " + empleado.EmpleadoNombre1 + " " + empleado.EmpleadoApellido1;
                            await db_context.SaveChangesAsync();
                            return true;
                        }
                        else
                        {
                            soli.SolicitudEstadoRrHh = "Aprobada " + empleado.EmpleadoNombre1 + " " + empleado.EmpleadoApellido1;
                            var periodo = await ObtenerPeriodoPorId((int)soli.PeriodoId);
                            periodo.PeriodoCantidadDiasPeriodo -= soli.SolicitudCantidadDias;
                            await db_context.SaveChangesAsync();
                            return true;
                        }

                    }
                    else if (solicitud.EstadosolicitudId == (int)enumEstados.DenegoDirectorRRHH)
                    {
                        soli.SolicitudEstadoRrHh = "Denegada " + empleado.EmpleadoNombre1 + " " + empleado.EmpleadoApellido1;
                    }
                    await db_context.SaveChangesAsync();
                    return true;
                default:
                    return false;

            }
        }

        public async Task<TbPeriodos> ObtenerPeriodoPorId(int id)
        {
            var periodo = await db_context.TbPeriodos.Where(p => p.PeriodoId == id).FirstOrDefaultAsync();
            return periodo;
        }

        public async Task<bool> ActualizarDatos(ActualizaDatos actualizaDatos,int empleadoId)
        {

            var empleado = await BuscarEmpleadoporCodigo(empleadoId);
            if(actualizaDatos.EmpleadoTelefono != null)
            {
                empleado.EmpleadoTelefono = actualizaDatos.EmpleadoTelefono;
            }
            if(actualizaDatos.EmpleadoDireccion!= null)
            {
                empleado.EmpleadoDireccion = actualizaDatos.EmpleadoDireccion;
            }
            
            await db_context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> InsertarDepartamento(AgregarDepartamentos agregarDepartamentos)
        {
            var depa = new TbDepartamentosLaborales();
            
            if(agregarDepartamentos != null)
            {
                depa.DeptoNombre = agregarDepartamentos.Departamento;
                depa.DeptoDescripcion = agregarDepartamentos.Descripcion;
                db_context.Add(depa);
                await db_context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> InsertarCodigo(AgregarCodigosdeEmpleado agregarCodigosdeEmpleado)
        {
            var codigo = new TbCodigosempleadosmunicipales();

            if (agregarCodigosdeEmpleado != null)
            {
                codigo.Codigoempleadomunicipal = agregarCodigosdeEmpleado.CodigodeEmpleado;
                db_context.Add(codigo);
                await db_context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> InsertarCargos(AgregarCargosViewModel agregarCargos)
        {
            var modelo = new TbCargos();
            if(agregarCargos.CargoNombreView != null)
            {
                modelo.CargoNombre = agregarCargos.CargoNombreView;
                modelo.CargoDescripcion = agregarCargos.CargoDescripcionView;
                modelo.DeptoId = agregarCargos.DeptoIdView;
                db_context.Add(modelo);
                await db_context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> InsertarPeriodo(AgregarPeriodosViewModel agregarPeriodos)
        {
            var modelo = new TbPeriodos
            {
                EmpleadoId = agregarPeriodos.empleado_idView,
                PeriodoVacacional = agregarPeriodos.periodo_vacacionalView,
                PeriodoCantidadDiasPeriodo = agregarPeriodos.periodo_cantidad_diasView,
                PeriodoObservaciones = agregarPeriodos.periodo_observacionesView

            };

            if(agregarPeriodos.periodo_vacacionalView != null)
            {
                db_context.Add(modelo);
                await db_context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
