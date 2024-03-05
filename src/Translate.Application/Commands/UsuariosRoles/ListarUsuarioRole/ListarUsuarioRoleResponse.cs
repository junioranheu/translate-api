using Translate.Domain.Entities;
using Translate.Domain.Enums;

namespace Translate.Application.Commands.UsuariosRoles.ListarUsuarioRole;

public sealed class ListarUsuarioRoleResponse
{
    public int UsuarioId { get; set; }

    public UsuarioRoleEnum RoleId { get; set; }
    public Role? Roles { get; set; }
}