using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Translate.API.Filters.Base;
using Translate.Application.Commands.Logs.CriarLog;
using Translate.Application.Handlers.Logs.CriarLog;
using static junioranheu_utils_package.Fixtures.Get;

namespace Translate.API.Filters;

public sealed class ErrorFilter(ILogger<ErrorFilter> logger, CriarLogHandler criarLogHandler) : ExceptionFilterAttribute
{
    private readonly ILogger _logger = logger;
    private readonly CriarLogHandler _criarLogHandler = criarLogHandler;

    public override async Task OnExceptionAsync(ExceptionContext context)
    {
        Exception ex = context.Exception;
        string mensagemErroCompleta = $"Ocorreu um erro ao processar sua requisição. Data: {ObterDetalhesDataHora()}. Caminho: {context.HttpContext.Request.Path}. {(!string.IsNullOrEmpty(ex.InnerException?.Message) ? $"Mais informações: {ex.InnerException.Message}" : $"Mais informações: {ex.Message}")}";
        string mensagemErroSimples = !string.IsNullOrEmpty(ex.InnerException?.Message) ? ex.InnerException.Message : ex.Message;

        var detalhes = new BadRequestObjectResult(new
        {
            Codigo = StatusCodes.Status500InternalServerError,
            Data = ObterDetalhesDataHora(),
            Caminho = context.HttpContext.Request.Path,
            Mensagens = new string[] { mensagemErroSimples },
            IsErro = true
        });

        Guid usuarioId = await new BaseFilter().BaseObterUsuarioId(context);
        await CriarLog(context, mensagemErroCompleta, usuarioId);

        mensagemErroCompleta += $" - usuarioId: {usuarioId}";
        ExibirILogger(ex, mensagemErroCompleta);

        context.Result = detalhes;
        context.ExceptionHandled = true;
    }

    private async Task CriarLog(ExceptionContext context, string mensagemErro, Guid? usuarioId)
    {
        CriarLogRequest request = new()
        {
            TipoRequisicao = context.HttpContext.Request.Method ?? string.Empty,
            Endpoint = context.HttpContext.Request.Path.ToString() ?? string.Empty,
            Parametros = string.Empty,
            Descricao = mensagemErro,
            StatusResposta = StatusCodes.Status500InternalServerError,
            UsuarioId = usuarioId != Guid.Empty ? usuarioId : null
        };

        await _criarLogHandler.Handle(request, new CancellationTokenSource().Token);
    }

    private void ExibirILogger(Exception ex, string mensagemErro)
    {
        _logger.LogError(ex, "{mensagemErro}", mensagemErro);
    }
}