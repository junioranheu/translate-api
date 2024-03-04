using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static junioranheu_utils_package.Fixtures.Validate;

namespace Translate.Domain.Entities;

[Index(nameof(Email))]
[Index(nameof(NomeUsuarioSistema))]
public sealed class Usuario
{
    public Usuario(Guid usuarioId, string nomeCompleto, string nomeUsuarioSistema, string email, string senha, bool isAtivo, bool isLatest, DateTime data)
    {
        ValidarParamsEntidade(GetType().Name, [nomeCompleto, nomeUsuarioSistema, senha], nameof(nomeCompleto), nameof(nomeUsuarioSistema), nameof(senha));

        UsuarioId = usuarioId;
        NomeCompleto = nomeCompleto;
        NomeUsuarioSistema = nomeUsuarioSistema;
        Email = email;
        Senha = senha;
        IsAtivo = isAtivo;
        IsLatest = isLatest;
        Data = data;
    }

    public Usuario(Guid usuarioId, string email)
    {
        UsuarioId = usuarioId;
        NomeCompleto = string.Empty;
        NomeUsuarioSistema = string.Empty;
        Email = email;
        Senha = string.Empty;
    }

    [Key]
    public Guid UsuarioId { get; private set; }

    public string NomeCompleto { get; private set; }

    public string NomeUsuarioSistema { get; private set; }

    public string Email { get; private set; }

    public string Senha { get; private set; }

    public bool IsAtivo { get; private set; }

    public bool IsLatest { get; private set; }

    public DateTime Data { get; private set; }

    public IEnumerable<UsuarioRole>? UsuarioRoles { get; init; }
}