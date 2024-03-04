using MediatR;
using Translate.Application.Commands.Usuarios.ObterUsuario;
using Translate.Domain.Entities;
using Translate.Domain.Enums;
using static junioranheu_utils_package.Fixtures.Get;

namespace Translate.Application.Handlers.Usuarios.ObterUsuario;

public class ObterUsuarioHandler(IUsuarioRepository repository) : IRequestHandler<ObterUsuarioRequest, ObterUsuarioResponse>
{
    private readonly IUsuarioRepository _repository = repository;

    public async Task<ObterUsuarioResponse> Handle(ObterUsuarioRequest command, CancellationToken cancellationToken)
    {
        var entidade = new Usuario(
            usuarioId: command.UsuarioId,
            nomeCompleto: string.Empty,
            nomeUsuarioSistema: string.Empty,
            email: command.Email,
            senha: string.Empty,
            isAtivo: true,
            isLatest: true,
            data: DateTime.MinValue
        );

        var linq = await _repository.Obter(entidade) ?? throw new Exception(ObterDescricaoEnum(CodigoErroEnum.NaoEncontrado));

        var result = new ObterUsuarioResponse()
        {
            UsuarioId = linq.UsuarioId,
            NomeCompleto = linq.NomeCompleto,
            NomeUsuarioSistema = linq.NomeUsuarioSistema,
            Email = linq.Email,
            IsAtivo = linq.IsAtivo,
            Data = linq.Data,
            UsuarioRoles = linq.UsuarioRoles
        };

        return result;
    }
}