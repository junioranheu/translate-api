using MediatR;
using Translate.Domain.Handlers.ObterFrase.Commands.Responses;

namespace Translate.Domain.Handlers.ObterFrase.Commands.Requests;

public class ObterFraseRequest: IRequest<ObterFraseResponse>
{
    public Guid Id { get; set; }
    public string Conteudo { get; set; } = string.Empty;
}