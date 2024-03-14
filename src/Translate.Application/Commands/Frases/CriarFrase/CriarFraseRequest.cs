using MediatR;
using System.Text.Json.Serialization;
using Translate.Domain.Enums;

namespace Translate.Application.Commands.Frases.CriarFrase;

public sealed class CriarFraseRequest : IRequest<CriarFraseResponse>
{
    public string Conteudo { get; set; } = string.Empty;
    public IdiomasEnum Idioma { get; set; }

    [JsonIgnore]
    public Guid UsuarioId { get; set; } = Guid.Empty; 
}