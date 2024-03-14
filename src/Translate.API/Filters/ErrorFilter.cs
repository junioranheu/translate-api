using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Translate.API.Filters.Base;
using Translate.Domain.Entities;
using Translate.Infrastructure.Repositories.Logs;
using static junioranheu_utils_package.Fixtures.Get;

namespace Translate.API.Filters;

public sealed class ErrorFilter(ILogger<ErrorFilter> logger, ILogRepository logRepository) : ExceptionFilterAttribute
{
    private readonly ILogger _logger = logger;
    private readonly ILogRepository _logRepository = logRepository;

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
        var entidade = new Log(
            logId: Guid.NewGuid(),
            tipoRequisicao: context.HttpContext.Request.Method ?? string.Empty,
            endpoint: context.HttpContext.Request.Path.ToString() ?? string.Empty,
            parametros: string.Empty,
            descricao: mensagemErro,
            statusResposta: StatusCodes.Status500InternalServerError,
            usuarioId: usuarioId != Guid.Empty ? usuarioId : null
        );

        await _logRepository.Criar(entidade);
    }

    private void ExibirILogger(Exception ex, string mensagemErro)
    {
        _logger.LogError(ex, "{mensagemErro}", mensagemErro);
    }
}