using MediatR;
using Translate.Application.Commands.Frases.CriarFrase;
using Translate.Domain.Entities;
using Translate.Infrastructure.Repositories.Frases;
using static junioranheu_utils_package.Fixtures.Get;

namespace Translate.Application.Handlers.Frases.CriarFrase;

public class CriarFraseHandler(IFraseRepository repository) : IRequestHandler<CriarFraseRequest, CriarFraseResponse>
{
    private readonly IFraseRepository _repository = repository;

    public async Task<CriarFraseResponse> Handle(CriarFraseRequest command, CancellationToken cancellationToken)
    {
        var entidade = new Frase(
            fraseId: Guid.NewGuid(),
            fraseOriginal: command.FraseOriginal,
            idiomaOriginal: command.IdiomaOriginal,
            fraseTraduzida: command.FraseTraduzida,
            idiomaTraduzido: command.IdiomaTraduzido,
            usuarioId: command.UsuarioId,
            data: GerarHorarioBrasilia()
        );

        await _repository.Criar(entidade);

        var result = new CriarFraseResponse
        {
            FraseOriginal = entidade.FraseOriginal,
            IdiomaOriginal = entidade.IdiomaOriginal,
            QtdCaracteresFraseOriginal = entidade.FraseOriginal.Length,
            FraseTraduzida = entidade.FraseTraduzida,
            IdiomaTraduzido = entidade.IdiomaTraduzido,
            QtdCaracteresFraseTraduzida = entidade.FraseTraduzida.Length,
            Data = entidade.Data
        };

        return result;
    }
}