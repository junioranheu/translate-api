using Translate.Application.Commands.Usuarios.ObterUsuario;
using Translate.Domain.Enums;

namespace Translate.Application.Commands.Frases.ObterFrase;

public sealed class ObterFraseResponse
{
    public Guid FraseId { get; set; }
    public string Conteudo { get; set; } = string.Empty;
    public IdiomasEnum Idioma { get; set; }
    public ObterUsuarioResponse? Usuarios { get; init; }
    public DateTime Data { get; set; }
}