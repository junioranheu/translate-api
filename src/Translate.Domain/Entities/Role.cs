using System.ComponentModel.DataAnnotations;
using Translate.Domain.Enums;
using static junioranheu_utils_package.Fixtures.Validate;

namespace Translate.Domain.Entities;

public sealed class Role
{
    public Role(UsuarioRoleEnum roleId, string tipo, string descricao, bool isAtivo, DateTime data)
    {
        ValidarParamsEntidade(GetType().Name, [roleId, tipo], nameof(roleId), nameof(tipo));

        RoleId = roleId;
        Tipo = tipo;
        Descricao = descricao;
        IsAtivo = isAtivo;
        Data = data;
    }

    [Key]
    public UsuarioRoleEnum RoleId { get; private set; }

    public string Tipo { get; private set; }

    public string Descricao { get; private set; }

    public bool IsAtivo { get; private set; }

    public DateTime Data { get; private set; }
}