using MediatR;
using Translate.Domain.Enums;

namespace Translate.Application.Commands.Frases.CriarFrase;

public sealed class CriarFraseRequest : IRequest<CriarFraseResponse>
{
    public string Conteudo { get; set; } = string.Empty;
    public IdiomasEnum Idioma { get; set; }
}