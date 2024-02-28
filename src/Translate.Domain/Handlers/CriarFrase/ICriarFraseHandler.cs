using Translate.Domain.Handlers.CriarFase.Commands.Responses;
using Translate.Domain.Handlers.CriarFrase.Commands.Requests;

namespace Translate.Domain.Handlers.CriarFrase;

public interface ICriarFraseHandler
{
    CriarFraseResponse Handle(CriarFraseRequest command);
}