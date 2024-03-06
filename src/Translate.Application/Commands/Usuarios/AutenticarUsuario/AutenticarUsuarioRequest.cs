using MediatR;

namespace Translate.Application.Commands.Usuarios.AutenticarUsuario;

public sealed class AutenticarUsuarioRequest : IRequest<AutenticarUsuarioResponse>
{
    public string? Login { get; set; } = string.Empty;
    public string? Senha { get; set; } = string.Empty;
}