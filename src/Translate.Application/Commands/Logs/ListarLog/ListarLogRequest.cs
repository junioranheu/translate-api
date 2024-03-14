using MediatR;

namespace Translate.Application.Commands.Logs.ListarLog;

public sealed class ListarLogRequest : IRequest<List<ListarLogResponse>>
{

}