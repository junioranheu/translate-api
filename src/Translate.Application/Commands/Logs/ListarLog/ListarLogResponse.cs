using Translate.Application.Commands.Usuarios.ObterUsuario;

namespace Translate.Application.Commands.Logs.ListarLog;

public sealed class ListarLogResponse
{
    public Guid LogId { get;  set; }

    public string TipoRequisicao { get;  set; } = string.Empty;

    public string Endpoint { get;  set; } = string.Empty;

    public string? Parametros { get;  set; }

    public string? Descricao { get;  set; }

    public int StatusResposta { get;  set; }

    public Guid? UsuarioId { get;  set; }
    public ObterUsuarioResponse? Usuarios { get; init; }

    public DateTime Data { get;  set; }
}
