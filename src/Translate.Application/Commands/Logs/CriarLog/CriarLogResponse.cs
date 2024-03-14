namespace Translate.Application.Commands.Logs.CriarLog;

public sealed class CriarLogResponse
{
    public string TipoRequisicao { get; set; } = string.Empty;
    public string Endpoint { get; set; } = string.Empty;
    public string? Parametros { get; set; } = string.Empty;
    public string? Descricao { get; set; } = string.Empty;
    public int StatusResposta { get; set; } = 0;
    public Guid? UsuarioId { get; set; } = Guid.Empty;
    public DateTime Data { get; set; }
}