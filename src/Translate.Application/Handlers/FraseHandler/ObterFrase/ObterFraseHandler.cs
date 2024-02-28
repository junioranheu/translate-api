using MediatR;
using Translate.Application.Commands.Frase.ObterFrase;
using Translate.Domain.Enums;
using Translate.Domain.Entities;

namespace Translate.Application.Handlers.FraseHandler.ObterFrase;

public class ObterFraseHandler : IRequestHandler<ObterFraseRequest, ObterFraseResponse>
{
    //IFraseRepository _repository;

    //public ObterFraseHandler(IFraseRepository repository)
    //{
    //    _repository = repository;
    //}

    public Task<ObterFraseResponse> Handle(ObterFraseRequest command, CancellationToken cancellationToken)
    {
        // TODO: Lógica de leitura se houver;

        // Retorna o resultado;
        // return _repository.Obter(command.Id);

        var frase = new Frase(conteudo: "22", idioma: IdiomasEnum.BR);

        var result = new ObterFraseResponse()
        {
            Conteudo = frase.Conteudo,
            Idioma = frase.Idioma,
            QtdCaracteres = frase.QtdCaracteres,
            Data = frase.Data
        };

        return Task.FromResult(result);
    }
}