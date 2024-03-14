using MediatR;
using Translate.Application.Commands.Frases.ObterFrase;
using Translate.Application.Commands.Usuarios.ObterUsuario;
using Translate.Domain.Entities;
using Translate.Domain.Enums;
using Translate.Infrastructure.Repositories.Frases;
using static junioranheu_utils_package.Fixtures.Get;

namespace Translate.Application.Handlers.Frases.ObterFrase;

public class ObterFraseHandler(IFraseRepository repository) : IRequestHandler<ObterFraseRequest, ObterFraseResponse>
{
    private readonly IFraseRepository _repository = repository;

    public async Task<ObterFraseResponse> Handle(ObterFraseRequest command, CancellationToken cancellationToken)
    {
        var entidade = new Frase(
            fraseId: command.FraseId,
            conteudo: command.Conteudo,
            idioma: command.Idioma,
            usuarioId: Guid.Empty,
            data: DateTime.MinValue
        );

        var linq = await _repository.Obter(entidade) ?? throw new Exception(ObterDescricaoEnum(CodigoErroEnum.NaoEncontrado));

        var resultUsuario = new ObterUsuarioResponse();

        if (linq.Usuarios is not null)
        {
            resultUsuario = new ObterUsuarioResponse()
            {
                UsuarioId = linq.Usuarios.UsuarioId,
                NomeCompleto = linq.Usuarios.NomeCompleto,
                NomeUsuarioSistema = linq.Usuarios.NomeUsuarioSistema,
                Email = linq.Usuarios.Email,
                IsAtivo = linq.Usuarios.IsAtivo,
                Data = linq.Usuarios.Data,
                UsuarioRoles = linq.Usuarios.UsuarioRoles
            };
        }

        var result = new ObterFraseResponse()
        {
            FraseId = linq.FraseId,
            Conteudo = linq.Conteudo,
            Idioma = linq.Idioma,
            Usuarios = resultUsuario,
            Data = linq.Data
        };

        return result;
    }
}