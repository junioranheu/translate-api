using MediatR;
using Translate.Application.Commands.UsuariosRoles.ListarUsuarioRole;
using Translate.Domain.Entities;
using Translate.Domain.Enums;
using Translate.Infrastructure.Repositories.UsuariosRoles;
using static junioranheu_utils_package.Fixtures.Get;

namespace Translate.Application.Handlers.UsuariosRoles.ListarUsuarioRole;

public class ListarUsuarioRoleHandler(IUsuarioRoleRepository repository) : IRequestHandler<ListarUsuarioRoleRequest, List<ListarUsuarioRoleResponse>>
{
    private readonly IUsuarioRoleRepository _repository = repository;

    public async Task<List<ListarUsuarioRoleResponse>> Handle(ListarUsuarioRoleRequest command, CancellationToken cancellationToken)
    {
        IEnumerable<UsuarioRole> linq = await _repository.Listar(command.Email);

        if (!linq.Any())
        {
            throw new Exception(ObterDescricaoEnum(CodigoErroEnum.NaoEncontrado));
        }

        List<ListarUsuarioRoleResponse> result = [];

        //var result = new ListarUsuarioRoleResponse()
        //{
        //    UsuarioId = linq.UsuarioId,
        //    NomeCompleto = linq.NomeCompleto,
        //    NomeUsuarioSistema = linq.NomeUsuarioSistema,
        //    Email = linq.Email,
        //    IsAtivo = linq.IsAtivo,
        //    Data = linq.Data,
        //    UsuarioRoles = linq.UsuarioRoles
        //};

        return result;
    }
}