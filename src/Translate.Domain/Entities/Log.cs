using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static junioranheu_utils_package.Fixtures.Get;
using static junioranheu_utils_package.Fixtures.Validate;

namespace Translate.Domain.Entities;

public sealed class Log
{
    public Log(Guid logId, string tipoRequisicao, string endpoint, string? parametros, string? descricao, int statusResposta, Guid? usuarioId)
    {
        ValidarParamsEntidade(GetType().Name, [tipoRequisicao, endpoint, statusResposta], nameof(tipoRequisicao), nameof(endpoint), nameof(statusResposta));

        LogId = logId;
        TipoRequisicao = tipoRequisicao;
        Endpoint = endpoint;
        Parametros = parametros;
        Descricao = descricao;
        StatusResposta = statusResposta;
        UsuarioId = usuarioId;
        Data = GerarHorarioBrasilia();
    }

    [Key]
    public Guid LogId { get; private set; }

    public string TipoRequisicao { get; private set; } 

    public string Endpoint { get; private set; }

    public string? Parametros { get; private set; } 

    public string? Descricao { get; private set; }

    public int StatusResposta { get; private set; }

    [ForeignKey(nameof(UsuarioId))]
    public Guid? UsuarioId { get; private set; }
    public Usuario? Usuarios { get; init; }

    public DateTime Data { get; private set; } 
}