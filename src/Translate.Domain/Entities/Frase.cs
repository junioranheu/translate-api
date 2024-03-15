using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Translate.Domain.Enums;
using static junioranheu_utils_package.Fixtures.Validate;

namespace Translate.Domain.Entities;

public sealed class Frase
{
    public Frase(Guid fraseId, string fraseOriginal, IdiomasEnum idiomaOriginal, string fraseTraduzida, IdiomasEnum idiomaTraduzido, Guid usuarioId, DateTime data)
    {
        ValidarParamsEntidade(GetType().Name, [fraseOriginal, idiomaOriginal, fraseTraduzida, idiomaTraduzido], nameof(fraseOriginal), nameof(idiomaOriginal), nameof(fraseTraduzida), nameof(fraseOriginal));

        FraseId = fraseId;
        FraseOriginal = fraseOriginal;
        IdiomaOriginal = idiomaOriginal;
        QtdCaracteresFraseOriginal = fraseOriginal.Length;
        FraseTraduzida = fraseTraduzida;
        IdiomaTraduzido = idiomaTraduzido;
        QtdCaracteresFraseTraduzida = fraseTraduzida.Length;
        UsuarioId = usuarioId;
        Data = data;
    }

    public Frase(string fraseOriginal, IdiomasEnum idiomaOriginal, string fraseTraduzida, IdiomasEnum idiomaTraduzido)
    {
        FraseOriginal = fraseOriginal;
        IdiomaOriginal = idiomaOriginal;
        QtdCaracteresFraseOriginal = fraseOriginal.Length;
        FraseTraduzida = fraseTraduzida;
        IdiomaTraduzido = idiomaTraduzido;
        QtdCaracteresFraseTraduzida = fraseTraduzida.Length;
    }

    [Key]
    public Guid FraseId { get; private set; }

    public string FraseOriginal { get; private set; }

    public IdiomasEnum IdiomaOriginal { get; private set; }

    public int QtdCaracteresFraseOriginal { get; private set; }

    public string FraseTraduzida { get; private set; }

    public IdiomasEnum IdiomaTraduzido { get; private set; }

    public int QtdCaracteresFraseTraduzida { get; private set; }

    [ForeignKey(nameof(UsuarioId))]
    public Guid UsuarioId { get; private set; }
    public Usuario? Usuarios { get; init; }

    public DateTime Data { get; private set; }
}