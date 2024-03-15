using Translate.Application.Commands.Usuarios.ObterUsuario;
using Translate.Domain.Enums;

namespace Translate.Application.Commands.Frases.ObterFrase;

public sealed class ObterFraseResponse
{
    public Guid FraseId { get; set; }

    public string FraseOriginal { get; set; } = string.Empty;

    public IdiomasEnum IdiomaOriginal { get; set; }

    public int QtdCaracteresFraseOriginal { get; set; }

    public string FraseTraduzida { get; set; } = string.Empty;

    public IdiomasEnum IdiomaTraduzido { get; set; }

    public int QtdCaracteresFraseTraduzida { get; set; }

    public ObterUsuarioResponse? Usuarios { get; init; }

    public DateTime Data { get; set; }
}