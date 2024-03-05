using Translate.Domain.Entities;

namespace Translate.Infrastructure.Repositories.UsuariosRoles
{
    public interface IUsuarioRoleRepository
    {
        Task<IEnumerable<UsuarioRole>> Listar(string email);
    }
}