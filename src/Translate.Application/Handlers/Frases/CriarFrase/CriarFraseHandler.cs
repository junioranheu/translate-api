using MediatR;
using Translate.Application.Commands.Frases.CriarFrase;
using Translate.Infrastructure.Repositories.Frases;
using static junioranheu_utils_package.Fixtures.Get;

namespace Translate.Application.Handlers.Frases.CriarFrase;

public class CriarFraseHandler(IFraseRepository repository) : IRequestHandler<CriarFraseRequest, CriarFraseResponse>
{
    private readonly IFraseRepository _repository = repository;

    public async Task<CriarFraseResponse> Handle(CriarFraseRequest command, CancellationToken cancellationToken)
    {
        var entidade = new Domain.Entities.Frase(
            fraseId: Guid.NewGuid(),
            conteudo: command.Conteudo,
            idioma: command.Idioma,
            data: GerarHorarioBrasilia()
        );

        await _repository.Criar(entidade);

        var result = new CriarFraseResponse
        {
            Conteudo = entidade.Conteudo,
            Idioma = entidade.Idioma,
            Data = entidade.Data
        };

        return result;
    }
}