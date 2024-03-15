using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Translate.Domain.Entities;
using Translate.Domain.Enums;
using Translate.Infrastructure.Data;

namespace Translate.Infrastructure.Repositories.Frases;

public sealed class FraseRepository(TranslateContext context) : IFraseRepository
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
        var linq = await _context.Frases.Where(f => f.FraseId == id).AsNoTracking().FirstOrDefaultAsync();

        if (linq is null)
        {
            return;
        }

        _context.Frases.Remove(linq);
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<Frase>> Listar()
    {
        return await _context.Frases.Include(f => f.Usuarios).AsNoTracking().ToListAsync();
    }

    public async Task<Frase?> Obter(Frase entidade)
    {
        var linq = await _context.Frases.
                   Include(f => f.Usuarios).
                   Where(f =>
                      (entidade.FraseId == Guid.Empty || f.FraseId == entidade.FraseId) &&
                      (entidade.FraseOriginal.IsNullOrEmpty() || f.FraseOriginal.Contains(entidade.FraseOriginal)) &&
                      (!Enum.IsDefined(entidade.IdiomaOriginal) || f.IdiomaOriginal == entidade.IdiomaOriginal) &&
                      (entidade.FraseTraduzida.IsNullOrEmpty() || f.FraseTraduzida.Contains(entidade.FraseTraduzida)) &&
                      (!Enum.IsDefined(entidade.IdiomaTraduzido) || f.IdiomaTraduzido == entidade.IdiomaTraduzido) 
                   ).AsNoTracking().FirstOrDefaultAsync();

        return linq;
    }

    public async Task Atualizar(Frase input)
    {
        var linq = await _context.Frases.Where(f => f.FraseId == input.FraseId).AsNoTracking().FirstOrDefaultAsync();

        if (linq is null)
        {
            return;
        }

        var update = new Frase(
            fraseId: linq.FraseId,
            fraseOriginal: input.FraseOriginal ?? linq.FraseOriginal,
            idiomaOriginal: input.IdiomaOriginal is not IdiomasEnum.Default ? input.IdiomaOriginal : linq.IdiomaOriginal,
            fraseTraduzida: input.FraseTraduzida ?? linq.FraseTraduzida,
            idiomaTraduzido: input.IdiomaTraduzido is not IdiomasEnum.Default ? input.IdiomaTraduzido : linq.IdiomaTraduzido,
            usuarioId: input.UsuarioId,
            data: linq.Data
        );

        _context.Update(update);
        await _context.SaveChangesAsync();
    }
}