using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Translate.Domain.Entities;
using Translate.Domain.Enums;
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
        var linq = await _context.Frases.Where(x => x.FraseId == id).AsNoTracking().FirstOrDefaultAsync();

        if (linq is null)
        {
            return;
        }

        _context.Frases.Remove(linq);
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<Frase>> ObterTodos()
    {
        return await _context.Frases.AsNoTracking().ToListAsync();
    }

    public async Task<Frase?> Obter(Frase entidade)
    {
        var linq = await _context.Frases.
                   Where(x =>
                      (entidade.FraseId == Guid.Empty || x.FraseId == entidade.FraseId) &&
                      (entidade.Conteudo.IsNullOrEmpty() || x.Conteudo.Contains(entidade.Conteudo)) &&
                      (!Enum.IsDefined(entidade.Idioma) || x.Idioma == entidade.Idioma)
                   ).AsNoTracking().FirstOrDefaultAsync();

        return linq;
    }

    public async Task Atualizar(Frase input)
    {
        var linq = await _context.Frases.Where(x => x.FraseId == input.FraseId).AsNoTracking().FirstOrDefaultAsync();

        if (linq is null)
        {
            return;
        }

        var update = new Frase(
            fraseId: linq.FraseId,
            conteudo: input.Conteudo ?? linq.Conteudo,
            idioma: input.Idioma is not IdiomasEnum.Default ? input.Idioma : linq.Idioma,
            data: linq.Data
        );

        _context.Update(update);
        await _context.SaveChangesAsync();
    }
}