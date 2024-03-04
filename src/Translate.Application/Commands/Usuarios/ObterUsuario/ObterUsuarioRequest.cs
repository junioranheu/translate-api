using MediatR;

namespace Translate.Application.Commands.Usuarios.ObterUsuario;

public sealed class ObterUsuarioRequest : IRequest<ObterUsuarioResponse>
{
    public Guid UsuarioId { get; set; }
    public string Email { get; set; } = string.Empty;
}