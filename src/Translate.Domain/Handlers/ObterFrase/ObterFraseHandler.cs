using MediatR;
using Translate.Domain.Handlers.ObterFrase.Commands.Requests;
using Translate.Domain.Handlers.ObterFrase.Commands.Responses;

namespace Translate.Domain.Handlers.ObterFrase;

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
        //return _repository.Obter(command.Id);

        var result = new ObterFraseResponse() { Conteudo = "22" };

        return Task.FromResult(result);
    }
}