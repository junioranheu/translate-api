using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Translate.API.Filters.Base;
using Translate.Domain.Entities;
using Translate.Infrastructure.Repositories.Logs;

namespace Translate.API.Filters;

public sealed class RequestFilter(ILogRepository logRepository) : ActionFilterAttribute
{
    private readonly ILogRepository _logRepository = logRepository;

    public override async Task OnActionExecutionAsync(ActionExecutingContext filterContextExecuting, ActionExecutionDelegate next)
    {
        if (filterContextExecuting.HttpContext.RequestAborted.IsCancellationRequested)
        {
            filterContextExecuting.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
            return;
        }

        ActionExecutedContext filterContextExecuted = await next();
        HttpRequest request = filterContextExecuted.HttpContext.Request;
        int? statusResposta = (filterContextExecuted.Result as ObjectResult)?.StatusCode;

        Guid usuarioId = await new BaseFilter().BaseObterUsuarioId(filterContextExecuted);
        string parametros = ObterParametrosRequisicao(filterContextExecuting);
        string parametrosNormalizados = NormalizarParametros(parametros);

        await CriarLog(request, statusResposta, parametrosNormalizados, usuarioId);
    }

    private static string ObterParametrosRequisicao(ActionExecutingContext filterContextExecuting)
    {
        var parametros = filterContextExecuting.ActionArguments.FirstOrDefault().Value ?? string.Empty;

        try
        {
            string parametrosSerialiazed = !string.IsNullOrEmpty(parametros.ToString()) ? JsonConvert.SerializeObject(parametros) : string.Empty;
            return parametrosSerialiazed;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }

    private static string NormalizarParametros(string parametros)
    {
        try
        {
            if (!string.IsNullOrEmpty(parametros))
            {
                string[] _listaKeysParaOcultarLog = ["Senha", "Password"];
                bool isNecessitaOcultarKeys = _listaKeysParaOcultarLog.Any(x => parametros.Contains("\"" + x + "\":", StringComparison.OrdinalIgnoreCase));

                if (isNecessitaOcultarKeys)
                {
                    JObject? parametrosJson = JsonConvert.DeserializeObject<JObject>(parametros);

                    foreach (var item in _listaKeysParaOcultarLog)
                    {
                        OcultarKeyEmParametro(parametrosJson, item);
                    }

                    string? parametrosJsonStr = parametrosJson?.ToString(Formatting.Indented);

                    if (string.IsNullOrEmpty(parametrosJsonStr))
                    {
                        return string.Empty;
                    }

                    return parametrosJsonStr.Replace("\r\n", "") ?? string.Empty;
                }
            }

            return parametros;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }

    private static void OcultarKeyEmParametro(JObject? parametroJson, string key)
    {
        if (parametroJson is not null && parametroJson[key] is not null)
        {
            parametroJson?.Property(key)?.Remove();
        }
    }

    private async Task CriarLog(HttpRequest request, int? statusResposta, string parametrosNormalizados, Guid? usuarioId)
    {
        var entidade = new Log(
            logId: Guid.NewGuid(),
            tipoRequisicao: request.Method ?? string.Empty,
            endpoint: request.Path.Value ?? string.Empty,
            parametros: parametrosNormalizados,
            descricao: string.Empty,
            statusResposta: statusResposta > 0 ? (int)statusResposta : StatusCodes.Status500InternalServerError,
            usuarioId: usuarioId != Guid.Empty ? usuarioId : null
       );

        await _logRepository.Criar(entidade);
    }
}