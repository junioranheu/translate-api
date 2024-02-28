using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static junioranheu_utils_package.Fixtures.Get;

namespace Translate.API.Filters;

public sealed class ErrorFilter(ILogger<ErrorFilter> logger) : ExceptionFilterAttribute
{
    private readonly ILogger _logger = logger;

    public override void OnException(ExceptionContext context)
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

        // int usuarioId = await new BaseFilter().BaseObterUsuarioId(context);
        // await CriarLog(context, mensagemErroCompleta, usuarioId);
        ExibirILogger(ex, mensagemErroCompleta);

        context.Result = detalhes;
        context.ExceptionHandled = true;
    }

    //private async Task CriarLog(ExceptionContext context, string mensagemErro, int? usuarioId)
    //{
    //    LogInput log = new()
    //    {
    //        TipoRequisicao = context.HttpContext.Request.Method ?? string.Empty,
    //        Endpoint = context.HttpContext.Request.Path.ToString() ?? string.Empty,
    //        Parametros = string.Empty,
    //        Descricao = mensagemErro,
    //        StatusResposta = StatusCodes.Status500InternalServerError,
    //        UsuarioId = usuarioId > 0 ? usuarioId : null
    //    };

    //    await _criarLogUseCase.Execute(log);
    //}

    private void ExibirILogger(Exception ex, string mensagemErro)
    {
        _logger.LogError(ex, "{mensagemErro}", mensagemErro);
    }
}