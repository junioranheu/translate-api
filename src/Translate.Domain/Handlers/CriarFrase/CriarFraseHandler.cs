using Translate.Domain.Entities;
using Translate.Domain.Handlers.CriarFase.Commands.Responses;
using Translate.Domain.Handlers.CriarFrase.Commands.Requests;

namespace Translate.Domain.Handlers.CriarFrase;

public class CriarFraseHandler : ICriarFraseHandler
{
    //ICustomerRepository _repository;
    //IEmailService _emailService;

    //public CriarFraseHandler(ICustomerRepository repository, IEmailService emailService)
    //{
    //    _repository = repository;
    //    _emailService = emailService;
    //}

    public CriarFraseResponse Handle(CriarFraseRequest command)
    {
        // Aplicar Fail Fast Validations;

        // Cria a entidade;
        var frase = new Frase(conteudo: command.Conteudo, idioma: command.Idioma);

        //// Persiste a entidade no banco;
        //_repository.Save(customer);

        //// Envia E-mail de boas-vindas;
        //_emailService.Send(customer.Name, customer.Email);

        // Retorna a resposta;
        return new CriarFraseResponse
        {
            Conteudo = frase.Conteudo,
            Idioma = frase.Idioma,
            Data = frase.Data
        };
    }
}