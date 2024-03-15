using Translate.Domain.Enums;

namespace Translate.Application.Commands.Frases.CriarFrase;

public sealed class CriarFraseResponse
{
    public string FraseOriginal { get;  set; } = string.Empty;

    public IdiomasEnum IdiomaOriginal { get;  set; }

    public int QtdCaracteresFraseOriginal { get;  set; }

    public string FraseTraduzida { get;  set; } = string.Empty;

    public IdiomasEnum IdiomaTraduzido { get;  set; }

    public int QtdCaracteresFraseTraduzida { get; set; }

    public DateTime Data { get; set; }
}