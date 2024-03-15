using MediatR;

namespace Translate.Application.Commands.Logs.CriarLog;

public sealed class CriarLogRequest : IRequest<CriarLogResponse>
{
    public required string TipoRequisicao { get; set; }
    public required string Endpoint { get;  set; }
    public string? Parametros { get;  set; } = string.Empty;
    public string? Descricao { get;  set; } = string.Empty;
    public required int StatusResposta { get; set; }
    public Guid? UsuarioId { get; set; } = Guid.Empty;
}