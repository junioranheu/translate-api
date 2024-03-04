using MediatR;
using Translate.Domain.Enums;

namespace Translate.Application.Commands.Frases.ObterFrase;

public sealed class ObterFraseRequest : IRequest<ObterFraseResponse>
{
    public Guid FraseId { get; set; }
    public string Conteudo { get; set; } = string.Empty;
    public IdiomasEnum Idioma { get; set; }
}