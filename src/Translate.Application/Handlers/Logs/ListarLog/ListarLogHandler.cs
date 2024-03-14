using MediatR;
using Translate.Application.Commands.Logs.ListarLog;
using Translate.Application.Commands.Usuarios.ObterUsuario;
using Translate.Domain.Entities;
using Translate.Domain.Enums;
using Translate.Infrastructure.Repositories.Logs;
using static junioranheu_utils_package.Fixtures.Get;

namespace Translate.Application.Handlers.Logs.ListarLog;

public class ListarLogHandler(ILogRepository repository) : IRequestHandler<ListarLogRequest, List<ListarLogResponse>>
{
    private readonly ILogRepository _repository = repository;

    public async Task<List<ListarLogResponse>> Handle(ListarLogRequest command, CancellationToken cancellationToken)
    {
        IEnumerable<Log> linq = await _repository.Listar();

        if (!linq.Any())
        {
            throw new Exception(ObterDescricaoEnum(CodigoErroEnum.NaoEncontrado));
        }

        List<ListarLogResponse> list = [];

        foreach (var item in linq)
        {
            var resultUsuario = new ObterUsuarioResponse();

            if (item.Usuarios is not null)
            {
                resultUsuario = new ObterUsuarioResponse()
                {
                    UsuarioId = item.Usuarios.UsuarioId,
                    NomeCompleto = item.Usuarios.NomeCompleto,
                    NomeUsuarioSistema = item.Usuarios.NomeUsuarioSistema,
                    Email = item.Usuarios.Email,
                    IsAtivo = item.Usuarios.IsAtivo,
                    Data = item.Usuarios.Data,
                    UsuarioRoles = item.Usuarios.UsuarioRoles
                };
            }

            var result = new ListarLogResponse()
            {
                LogId = item.LogId,
                TipoRequisicao = item.TipoRequisicao,
                Endpoint = item.Endpoint,
                Parametros = item.Parametros,
                Descricao = item.Descricao,
                StatusResposta = item.StatusResposta,
                UsuarioId = item.UsuarioId,
                Usuarios = resultUsuario,
                Data = item.Data
            };

            list.Add(result);
        }

        return list;
    }
}