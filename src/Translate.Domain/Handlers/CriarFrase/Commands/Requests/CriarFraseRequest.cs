using Translate.Domain.Enums;
using Translate.Domain.Handlers.CriarFase.Commands.Responses;
using MediatR;

namespace Translate.Domain.Handlers.CriarFrase.Commands.Requests;

public class CriarFraseRequest : IRequest<CriarFraseResponse>
{
    public string Conteudo { get; set; } = string.Empty;
    public IdiomasEnum Idioma { get; set; }
}