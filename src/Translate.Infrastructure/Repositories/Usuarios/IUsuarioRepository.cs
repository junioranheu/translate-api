using Translate.Domain.Entities;

namespace Translate.Infrastructure.Repositories.Usuarios
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> Obter(Usuario entidade);
        Task<(Usuario? usuario, string senha)> ObterUsuarioCondicaoArbitraria(Usuario entidade);
    }
}