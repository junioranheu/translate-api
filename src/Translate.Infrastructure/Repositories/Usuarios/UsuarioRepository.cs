using Microsoft.EntityFrameworkCore;
using Translate.Domain.Entities;
using Translate.Infrastructure.Data;

namespace Translate.Infrastructure.Repositories.Usuarios;

public sealed class UsuarioRepository(TranslateContext context) : IUsuarioRepository
{
    private readonly TranslateContext _context = context;

    public async Task<Usuario?> Obter(Usuario entidade)
    {
        var linq = await _context.Usuarios.
                   Include(ur => ur.UsuarioRoles)!.ThenInclude(r => r.Roles).
                   Where(u =>
                      entidade.UsuarioId != Guid.Empty ? u.UsuarioId == entidade.UsuarioId : true &&
                      !string.IsNullOrEmpty(entidade.Email) ? u.Email == entidade.Email : true &&
                      u.IsLatest == true
                   ).AsNoTracking().FirstOrDefaultAsync();

        return linq;
    }

    public async Task<(Usuario? usuario, string senha)> ObterUsuarioCondicaoArbitraria(Usuario entidade)
    {
        var byEmail = await _context.Usuarios.
                      Where(e => e.Email == entidade.Email).
                      Include(ur => ur.UsuarioRoles)!.ThenInclude(r => r.Roles).
                      AsNoTracking().FirstOrDefaultAsync();

        if (byEmail is null)
        {
            var byNomeUsuario = await _context.Usuarios.
                                Where(n => n.NomeUsuarioSistema == entidade.NomeUsuarioSistema).
                                Include(ur => ur.UsuarioRoles)!.ThenInclude(r => r.Roles).
                                AsNoTracking().FirstOrDefaultAsync();

            if (byNomeUsuario is null)
            {
                return (null, string.Empty);
            }

            return (byNomeUsuario, byNomeUsuario.Senha ?? string.Empty);
        }

        return (byEmail, byEmail.Senha ?? string.Empty);
    }
}