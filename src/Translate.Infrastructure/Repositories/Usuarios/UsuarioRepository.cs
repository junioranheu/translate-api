using Microsoft.EntityFrameworkCore;
using Translate.Domain.Entities;
using Translate.Infrastructure.Data;

namespace Translate.Infrastructure.Repositories.Usuarios;

public sealed class UsuarioRepository(TranslateContext context) : IUsuarioRepository
{
    private readonly TranslateContext _context = context;

    public async Task<Usuario?> Obter(Usuario input)
    {
        var linq = await _context.Usuarios.
                   Include(ur => ur.UsuarioRoles)!.ThenInclude(r => r.Roles).
                   Where(u =>
                      input.UsuarioId != Guid.Empty ? u.UsuarioId == input.UsuarioId : true &&
                      !string.IsNullOrEmpty(input.Email) ? u.Email == input.Email : true &&
                      u.IsLatest == true
                   ).AsNoTracking().FirstOrDefaultAsync();

        return linq;
    }

    public async Task<Usuario?> ObterUsuarioCondicaoArbitraria(Usuario input)
    {
        var byEmail = await _context.Usuarios.
                      Where(e => e.Email == input.Email).
                      Include(ur => ur.UsuarioRoles)!.ThenInclude(r => r.Roles).
                      AsNoTracking().FirstOrDefaultAsync();

        if (byEmail is null)
        {
            var byNomeUsuario = await _context.Usuarios.
                                Where(n => n.NomeUsuarioSistema == input.NomeUsuarioSistema).
                                Include(ur => ur.UsuarioRoles)!.ThenInclude(r => r.Roles).
                                AsNoTracking().FirstOrDefaultAsync();

            if (byNomeUsuario is null)
            {
                return new Usuario(usuarioId: Guid.Empty, email: string.Empty);
            }

            return byNomeUsuario;
        }

        return byEmail;
    }
}