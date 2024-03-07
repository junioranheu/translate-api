using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Translate.Domain.Enums;
using static junioranheu_utils_package.Fixtures.Validate;

namespace Translate.Domain.Entities;

public sealed class Frase
{
    public Frase(Guid fraseId, string conteudo, IdiomasEnum idioma, Guid usuarioId, DateTime data)
    {
        ValidarParamsEntidade(GetType().Name, [conteudo, idioma], nameof(conteudo), nameof(idioma));

        FraseId = fraseId;
        Conteudo = conteudo;
        Idioma = idioma;
        QtdCaracteres = conteudo.Length;
        UsuarioId = usuarioId;
        Data = data;
    }

    [Key]
    public Guid FraseId { get; private set; }

    public string Conteudo { get; private set; }

    public IdiomasEnum Idioma { get; private set; }

    public int QtdCaracteres { get; private set; }

    [ForeignKey(nameof(UsuarioId))]
    public Guid UsuarioId { get; private set; }
    public Usuario? Usuarios { get; init; }

    public DateTime Data { get; private set; }
}