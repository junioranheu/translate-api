using MediatR;

namespace Translate.Application.Commands.Usuarios.AutenticarUsuario;

public sealed class AutenticarUsuarioRequest : IRequest<AutenticarUsuarioResponse>
{
    public required string Login { get; set; }
    public required string Senha { get; set; }
}