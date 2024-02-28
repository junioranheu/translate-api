using Translate.Domain.Handlers.CriarFrase.Commands.Requests;
using Translate.Domain.Handlers.CriarFrase.Commands.Responses;

namespace Translate.Domain.Handlers.CriarFrase;

public interface ICriarFraseHandler
{
    CriarFraseResponse Handle(CriarFraseRequest command);
}