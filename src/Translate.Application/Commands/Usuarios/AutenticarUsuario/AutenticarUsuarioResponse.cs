using Translate.Application.Commands.UsuariosRoles.ListarUsuarioRole;

namespace Translate.Application.Commands.Usuarios.AutenticarUsuario;

public sealed class AutenticarUsuarioResponse 
{
    public Guid UsuarioId { get; set; }
    public string? NomeCompleto { get; set; } = string.Empty;
    public string? NomeUsuarioSistema { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public bool IsAtivo { get; set; } 
    public DateTime Data { get; set; }
    public IEnumerable<ListarUsuarioRoleResponse>? UsuarioRoles { get; init; }

    // Propriedades extras;
    public string? Token { get; set; } = null;
}