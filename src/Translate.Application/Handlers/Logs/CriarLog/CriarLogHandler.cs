using MediatR;
using Translate.Application.Commands.Logs.CriarLog;
using Translate.Domain.Entities;
using static junioranheu_utils_package.Fixtures.Get;

namespace Translate.Application.Handlers.Logs.CriarLog;

public class CriarLogHandler(ILogRepository repository) : IRequestHandler<CriarLogRequest, CriarLogResponse>
{
    private readonly ILogRepository _repository = repository;

    public async Task<CriarLogResponse> Handle(CriarLogRequest command, CancellationToken cancellationToken)
    {
        var entidade = new Log(
            logId: Guid.NewGuid(),
            tipoRequisicao: command.TipoRequisicao,
            endpoint: command.Endpoint,
            parametros: command.Parametros,
            descricao: command.Descricao,
            statusResposta: command.StatusResposta,
            usuarioId: command.UsuarioId
        );

        await _repository.Criar(entidade);

        var result = new CriarLogResponse
        {
            TipoRequisicao = entidade.TipoRequisicao,
            Endpoint = entidade.Endpoint,
            Parametros = entidade.Parametros,
            Descricao = entidade.Descricao,
            StatusResposta = entidade.StatusResposta,
            UsuarioId = entidade.UsuarioId,
            Data = GerarHorarioBrasilia()
        };

        return result;
    }
}