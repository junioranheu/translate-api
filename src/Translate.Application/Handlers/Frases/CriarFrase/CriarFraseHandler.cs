using MediatR;
using Translate.Application.Commands.Frases.CriarFrase;
using Translate.Infrastructure.Repositories.Frases;

namespace Translate.Application.Handlers.Frases.CriarFrase;

public class CriarFraseHandler(IFraseRepository repository) : IRequestHandler<CriarFraseRequest, CriarFraseResponse>
{
    private readonly IFraseRepository _repository = repository;

    public async Task<CriarFraseResponse> Handle(CriarFraseRequest command, CancellationToken cancellationToken)
    {
        var frase = new Domain.Entities.Frase(conteudo: command.Conteudo, idioma: command.Idioma);
        await _repository.Criar(frase);

        //// Envia E-mail de boas-vindas;
        //_emailService.Send(customer.Name, customer.Email);

        // Retorna a resposta;
        var result = new CriarFraseResponse
        {
            Conteudo = frase.Conteudo,
            Idioma = frase.Idioma,
            Data = frase.Data
        };

        return result;
    }
}