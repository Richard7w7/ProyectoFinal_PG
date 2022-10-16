using System.Security.Claims;

namespace ProyectoFinal_PG.Servicios
{
    public interface IServicioEmpleados
    {
        int ObtenerEmpleadoId();
    }
    public class ServicioEmpleados:IServicioEmpleados
    {
        private readonly HttpContext httpContext;

        public ServicioEmpleados(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContext = httpContextAccessor.HttpContext;
        }

        public int ObtenerEmpleadoId()
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                var IdClaim = httpContext
                    .User.Claims.Where(x => x.Type == ClaimTypes.Name)
                    .FirstOrDefault();
                var id = int.Parse(IdClaim.Value);
                var paso = id;
                return id;
            }
            else
            {
                throw new ApplicationException("El usuario no esta Autenticado");
            }
        }
    }
}
