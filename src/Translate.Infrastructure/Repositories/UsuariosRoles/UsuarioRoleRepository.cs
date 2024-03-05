using Microsoft.EntityFrameworkCore;
using Translate.Domain.Entities;
using Translate.Infrastructure.Data;

namespace Translate.Infrastructure.Repositories.UsuariosRoles;

public sealed class UsuarioRoleRepository(TranslateContext context) : IUsuarioRoleRepository
{
    private readonly TranslateContext _context = context;

    public async Task<IEnumerable<UsuarioRole>> Listar(string email)
    {
        var linq = await _context.UsuariosRoles.
                   Include(u => u.Usuarios).
                   Where(u => u.Usuarios!.Email == email && u.Usuarios.IsAtivo == true).
                   AsNoTracking().ToListAsync();

        return linq;
    }
}