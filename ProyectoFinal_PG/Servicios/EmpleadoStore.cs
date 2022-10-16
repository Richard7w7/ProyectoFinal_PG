using Microsoft.AspNetCore.Identity;
using ProyectoFinal_PG.Models;

namespace ProyectoFinal_PG.Servicios
{
    
    public class EmpleadoStore : IUserStore<TbEmpleados>, IUserPasswordStore<TbEmpleados>
    {
        private readonly IServiciosRegistroLogueo serviciosrl;

        public EmpleadoStore(IServiciosRegistroLogueo serviciosrl)
        {
            this.serviciosrl = serviciosrl;
        }
        public async Task<IdentityResult> CreateAsync(TbEmpleados user, CancellationToken cancellationToken)
        {
            user.EmpleadoId = await serviciosrl.CrearEmpleado(user);
            return IdentityResult.Success;
        }

        public Task<IdentityResult> DeleteAsync(TbEmpleados user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }

        public Task<TbEmpleados> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<TbEmpleados> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {

            return await serviciosrl.BuscarPorCodigoEmpleado(normalizedUserName);
            
        }

        public Task<string> GetNormalizedUserNameAsync(TbEmpleados user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(TbEmpleados user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmpleadoContrasena);
        }

        public Task<string> GetUserIdAsync(TbEmpleados user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmpleadoId.ToString());
        }

        public Task<string> GetUserNameAsync(TbEmpleados user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmpleadoCodigo);
        }

        public Task<bool> HasPasswordAsync(TbEmpleados user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(TbEmpleados user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(TbEmpleados user, string passwordHash, CancellationToken cancellationToken)
        {
            user.EmpleadoContrasena = passwordHash;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(TbEmpleados user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(TbEmpleados user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
