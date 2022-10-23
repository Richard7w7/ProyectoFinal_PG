
using Microsoft.EntityFrameworkCore;
using ProyectoFinal_PG.Models;

namespace ProyectoFinal_PG.Servicios
{
    //declaracion de interface
    public interface IServiciosRegistroLogueo
    {
        Task<TbEmpleados> BuscarPorCodigoEmpleado(string codigoemp);
        Task<int> CrearEmpleado(TbEmpleados empleados);

        //inyectando metodos en la interface
        Task<IEnumerable<TbCargos>> ObtenerCargos(int deptoId);
        Task<IEnumerable<TbDepartamentosLaborales>> ObtenerDepartamentos();
        Task<IEnumerable<TbEmpleados>> ObtenerEmpleados();
        Task<TbEmpleados> ValidarDatos(RecuperarContraseñaModel recuperarContraseña);
    }
    public class ServiciosRegistroLogueo: IServiciosRegistroLogueo
    {
        //declaracion de variales de solo lectura
        private readonly BD_ControlVacacionalContext db_context;
        

        //declaracion de constructor
        public ServiciosRegistroLogueo(BD_ControlVacacionalContext dB_Control_Context)
        {
            this.db_context = dB_Control_Context;
        }

        //declaracion de metodos
        public async Task<IEnumerable<TbDepartamentosLaborales>> ObtenerDepartamentos()
        {
            return await db_context.TbDepartamentosLaborales.ToListAsync();
        }

        public async Task<IEnumerable<TbCargos>> ObtenerCargos(int deptoId)
        {
            
            return await db_context.TbCargos.Where(x => x.DeptoId == deptoId).ToListAsync();
        }

        public async Task<int> CrearEmpleado(TbEmpleados empleados)
        {
            
            await db_context.AddAsync(empleados);
            await db_context.SaveChangesAsync();
            var empId =(int) empleados.EmpleadoId;
            return empId;
        }

        public async Task<TbEmpleados> BuscarPorCodigoEmpleado(string codigoemp)
        {
            var empleado = new TbEmpleados();
            empleado = await db_context.TbEmpleados
                .Include(x => x.Cargo)
                .Where(emp => emp.EmpleadoCodigo == codigoemp).FirstOrDefaultAsync();
            return empleado;
        }

        public async Task<IEnumerable<TbEmpleados>> ObtenerEmpleados()
        {
            return await db_context.TbEmpleados.ToListAsync();
        }

        public async Task<TbEmpleados> ValidarDatos(RecuperarContraseñaModel recuperarContraseña)
        {
            var resultado = await db_context.TbEmpleados
                .Include(x => x.Cargo)
                .Where(emp => emp.EmpleadoCodigo == recuperarContraseña.CodigoEmpleado)
                .Where(emp => emp.EmpleadoTelefono == recuperarContraseña.TelefonoEmpleado)
                .Where(emp => emp.EmpleadoFechaNacimiento== recuperarContraseña.FechaNacimiento).FirstOrDefaultAsync();

            return resultado;
        }

        //public async Task<bool> CambiarContraseña(TbEmpleados empleados)
        //{
        //    var empleado = await BuscarPorCodigoEmpleado(empleados.EmpleadoContrasena);
        //    var contrahash = userManager.


        //}
    }
}
