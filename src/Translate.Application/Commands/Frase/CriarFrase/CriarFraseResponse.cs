using Translate.Domain.Enums;

namespace Translate.Application.Commands.Frase.CriarFrase;

public class CriarFraseResponse
{
    public string Conteudo { get; set; } = string.Empty;
    public IdiomasEnum Idioma { get; set; }
    public int QtdCaracteres { get; set; }
    public DateTime Data { get; set; }
}