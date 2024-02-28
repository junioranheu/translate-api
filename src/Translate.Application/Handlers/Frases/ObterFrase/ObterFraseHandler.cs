using MediatR;
using Translate.Application.Commands.Frases.ObterFrase;
using Translate.Domain.Enums;
using Translate.Infrastructure.Repositories.Frases;
using static junioranheu_utils_package.Fixtures.Get;

namespace Translate.Application.Handlers.Frases.ObterFrase;

public class ObterFraseHandler(IFraseRepository repository) : IRequestHandler<ObterFraseRequest, ObterFraseResponse>
{
    private readonly IFraseRepository _repository = repository;

    public async Task<ObterFraseResponse> Handle(ObterFraseRequest command, CancellationToken cancellationToken)
    {
        var frase = await _repository.Obter(command.Id) ?? throw new Exception(ObterDescricaoEnum(CodigoErroEnum.NaoEncontrado));

        var result = new ObterFraseResponse()
        {
            Conteudo = frase.Conteudo,
            Idioma = frase.Idioma,
            QtdCaracteres = frase.QtdCaracteres,
            Data = frase.Data
        };

        return result;
    }
}