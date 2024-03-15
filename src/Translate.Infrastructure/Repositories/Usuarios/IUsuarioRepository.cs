using Translate.Domain.Entities;

namespace Translate.Infrastructure.Repositories.Usuarios
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> Obter(Usuario input);
        Task<Usuario?> ObterUsuarioCondicaoArbitraria(Usuario input);
    }
}