using MediatR;
using Translate.Domain.Enums;

namespace Translate.Application.Commands.Frases.ObterFrase;

public class ObterFraseRequest : IRequest<ObterFraseResponse>
{
    public Guid Id { get; set; }
    public string Conteudo { get; set; } = string.Empty;
    public IdiomasEnum Idioma { get; set; }
}