using Microsoft.AspNetCore.Identity;
using ProyectoFinal_PG.Models;

namespace ProyectoFinal_PG.Servicios
{
    public class UsuarioStore : IUserStore<Usuario>, IUserPasswordStore<Usuario>
    {
        public async Task<IdentityResult> CreateAsync(Usuario user, CancellationToken cancellationToken)
        {
            return IdentityResult.Success;
        }

        public Task<IdentityResult> DeleteAsync(Usuario user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }

        public async Task<Usuario> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(Usuario user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.passwordHash);
        }

        public Task<string> GetUserIdAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmpleadoId.ToString());
        }

        public Task<string> GetUserNameAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmpleadoCodigo);
        }

        public Task<bool> HasPasswordAsync(Usuario user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(Usuario user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(Usuario user, string passwordHash, CancellationToken cancellationToken)
        {
            user.passwordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(Usuario user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(Usuario user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
