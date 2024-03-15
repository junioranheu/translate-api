using MediatR;
using System.Text.Json.Serialization;
using Translate.Domain.Enums;

namespace Translate.Application.Commands.Frases.CriarFrase;

public sealed class CriarFraseRequest : IRequest<CriarFraseResponse>
{
    public required string FraseOriginal { get; set; }

    public required IdiomasEnum IdiomaOriginal { get; set; }

    [JsonIgnore]
    public string FraseTraduzida { get; set; } = string.Empty;

    public IdiomasEnum IdiomaTraduzido { get; set; }

    [JsonIgnore]
    public Guid UsuarioId { get; set; } = Guid.Empty;
}