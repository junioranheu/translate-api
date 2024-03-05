using MediatR;

namespace Translate.Application.Commands.UsuariosRoles.ListarUsuarioRole;

public sealed class ListarUsuarioRoleRequest : IRequest<List<ListarUsuarioRoleResponse>>
{
    public string Email { get; set; } = string.Empty;
}