using MediatR;
using Translate.Domain.Enums;

namespace Translate.Application.Commands.Frases.ListarFrase;

public sealed class ListarFraseRequest : IRequest<List<ListarFraseResponse>>
{
    public string FraseOriginal { get; set; } = string.Empty;

    public IdiomasEnum IdiomaOriginal { get; set; }

    public string FraseTraduzida { get; set; } = string.Empty;

    public IdiomasEnum IdiomaTraduzido { get; set; }
}