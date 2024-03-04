using Microsoft.EntityFrameworkCore;
using Translate.Domain.Entities;
using Translate.Infrastructure.Data;

namespace Translate.Infrastructure.Repositories.Usuarios;

public class UsuarioRepository(TranslateContext context) : IUsuarioRepository
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
}