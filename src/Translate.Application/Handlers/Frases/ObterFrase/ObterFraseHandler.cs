using MediatR;
using Translate.Application.Commands.Frases.ObterFrase;
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

        var result = new ObterFraseResponse()
        {
            FraseId = linq.FraseId,
            Conteudo = linq.Conteudo,
            Idioma = linq.Idioma,
            Data = linq.Data
        };

        return result;
    }
}