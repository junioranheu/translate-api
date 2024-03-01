using System.ComponentModel;

namespace Translate.Domain.Enums;

public enum UsuarioRoleEnum
{
    [Description("Administrador")]
    Administrador = 1,

    [Description("Usuário comum")]
    Comum = 2
}