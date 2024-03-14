using Translate.Domain.Entities;

namespace Translate.Infrastructure.Repositories.Logs
{
    public interface ILogRepository
    {
        Task<Log> Criar(Log input);
    }
}