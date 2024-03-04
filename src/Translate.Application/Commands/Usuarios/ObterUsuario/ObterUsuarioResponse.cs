using Translate.Domain.Entities;

namespace Translate.Application.Commands.Usuarios.ObterUsuario;

public sealed class ObterUsuarioResponse
{
    public Guid UsuarioId { get; set; }

    public string? NomeCompleto { get; set; }

    public string? NomeUsuarioSistema { get; set; } 

    public string? Email { get; set; } 

    public bool IsAtivo { get; set; }

    public DateTime Data { get; set; }

    public IEnumerable<UsuarioRole>? UsuarioRoles { get; init; }
}
