using Translate.Domain.Enums;

namespace Translate.Domain.Handlers.ObterFrase.Commands.Responses;

public class ObterFraseResponse
{
    public string Conteudo { get; set; } = string.Empty;
    public IdiomasEnum Idioma { get; set; }
    public int QtdCaracteres { get; set; }
    public DateTime Data { get; set; }
}