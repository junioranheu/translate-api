using MediatR;
using Translate.Domain.Enums;

namespace Translate.Application.Commands.Frases.ObterFrase;

public sealed class ObterFraseRequest : IRequest<ObterFraseResponse>
{
    public Guid FraseId { get; set; }

    public string FraseOriginal { get; set; } = string.Empty;

    public IdiomasEnum IdiomaOriginal { get; set; }

    public string FraseTraduzida { get; set; } = string.Empty;

    public IdiomasEnum IdiomaTraduzido { get; set; }
}