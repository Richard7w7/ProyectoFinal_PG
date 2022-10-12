using Microsoft.EntityFrameworkCore;
using ProyectoFinal_PG.Models;

namespace ProyectoFinal_PG.Servicios
{
    public interface IServiciosRegistroLogueo
    {
        Task<IEnumerable<TbCargos>> ObtenerCargos(TbDepartamentosLaborales deptoId);
        Task<IEnumerable<TbDepartamentosLaborales>> ObtenerDepartamentos();
    }
    public class ServiciosRegistroLogueo: IServiciosRegistroLogueo
    {
        private readonly BD_ControlVacacionalContext bD_Control_Context;

        public ServiciosRegistroLogueo(BD_ControlVacacionalContext dB_Control_Context)
        {
            this.bD_Control_Context = dB_Control_Context;
        }

        public async Task<IEnumerable<TbDepartamentosLaborales>> ObtenerDepartamentos()
        {
            return await bD_Control_Context.TbDepartamentosLaborales.ToListAsync();
        }

        public async Task<IEnumerable<TbCargos>> ObtenerCargos(TbDepartamentosLaborales deptoId)
        {
            
            return await bD_Control_Context.TbCargos.Where(x => x.DeptoId == deptoId.DeptoId).ToListAsync();
        }
    }
}
