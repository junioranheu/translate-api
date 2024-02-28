using Translate.Domain.Commands.Requests;
using Translate.Domain.Commands.Responses;

namespace Translate.Domain.Handlers;

public interface ICriarFraseHandler
{
    CriarFraseResponse Handle(CriarFraseRequest command);
}