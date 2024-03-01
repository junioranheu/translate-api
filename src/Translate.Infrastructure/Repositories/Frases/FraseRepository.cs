using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Translate.Domain.Entities;
using Translate.Infrastructure.Data;

namespace Translate.Infrastructure.Repositories.Frases;

public class FraseRepository(TranslateContext context) : IFraseRepository
{
    private readonly TranslateContext _context = context;

    public async Task<Frase> Criar(Frase input)
    {
        await _context.Frases.AddAsync(input);
        await _context.SaveChangesAsync();

        return input;
    }

    public async Task Deletar(Guid id)
    {
        var item = await _context.Frases.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();

        if (item is null)
        {
            return;
        }

        _context.Frases.Remove(item);
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<Frase>> ObterTodos()
    {
        return await _context.Frases.AsNoTracking().ToListAsync();
    }

    public async Task<Frase?> Obter(Frase entidade)
    {
        var result = await _context.Frases.
                     Where(x =>
                        (entidade.Id == Guid.Empty || x.Id == entidade.Id) &&
                        (entidade.Conteudo.IsNullOrEmpty() || x.Conteudo.Contains(entidade.Conteudo)) &&
                        (!Enum.IsDefined(entidade.Idioma) || x.Idioma == entidade.Idioma)
                     ).AsNoTracking().FirstOrDefaultAsync();

        return result;
    }

    public async Task Atualizar(Guid id)
    {
        var item = await _context.Frases.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();

        if (item is null)
        {
            return;
        }

        // item.Name = name;
        // item.Email = email;

        await _context.SaveChangesAsync();
    }
}