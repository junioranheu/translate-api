using Translate.Domain.Entities;
using Translate.Infrastructure.Data;

namespace Translate.Infrastructure.Repositories.Logs;

public sealed class LogRepository(TranslateContext context) : ILogRepository
{
    private readonly TranslateContext _context = context;

    public async Task<Log> Criar(Log input)
    {
        await _context.Logs.AddAsync(input);
        await _context.SaveChangesAsync();

        return input;
    }

    //public async Task<ICollection<Frase>> ObterTodos()
    //{
    //    return await _context.Frases.AsNoTracking().ToListAsync();
    //}
}