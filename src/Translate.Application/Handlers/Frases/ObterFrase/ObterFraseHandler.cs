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
            id: command.Id,
            conteudo: command.Conteudo,
            idioma: command.Idioma,
            data: DateTime.MinValue
        );

        var frase = await _repository.Obter(entidade) ?? throw new Exception(ObterDescricaoEnum(CodigoErroEnum.NaoEncontrado));

        var result = new ObterFraseResponse()
        {
            Id = frase.Id,
            Conteudo = frase.Conteudo,
            Idioma = frase.Idioma,
            Data = frase.Data
        };

        return result;
    }
}