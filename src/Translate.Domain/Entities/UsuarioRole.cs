using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Translate.Domain.Enums;
using static junioranheu_utils_package.Fixtures.Validate;

namespace Translate.Domain.Entities;

public sealed class UsuarioRole
{
    public UsuarioRole(Guid id, int usuarioId, UsuarioRoleEnum roleId, DateTime data)
    {
        ValidarParamsEntidade(GetType().Name, [usuarioId, roleId], nameof(usuarioId), nameof(roleId));

        Id = id;
        UsuarioId = usuarioId;
        RoleId = roleId;
        Data = data;
    }

    [Key]
    public Guid Id { get; private set; }

    public int UsuarioId { get; private set; }
    [JsonIgnore]
    public Usuario? Usuarios { get; private set; }

    public UsuarioRoleEnum RoleId { get; private set; }
    public Role? Roles { get; private set; }

    public DateTime Data { get; private set; }
}