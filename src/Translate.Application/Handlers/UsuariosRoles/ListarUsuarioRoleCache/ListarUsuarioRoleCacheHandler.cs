using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Translate.Application.Commands.UsuariosRoles.ListarUsuarioRole;
using Translate.Infrastructure.Repositories.UsuariosRoles;

namespace Translate.Application.Handlers.UsuariosRoles.ListarUsuarioRoleCache;

public sealed class ListarUsuarioRoleCacheHandler(IMemoryCache memoryCache, IUsuarioRoleRepository repository) : IRequestHandler<ListarUsuarioRoleRequest, List<ListarUsuarioRoleResponse>>
{
    private readonly IMemoryCache _memoryCache = memoryCache;
    private readonly IUsuarioRoleRepository _repository = repository;

    public async Task<List<ListarUsuarioRoleResponse>> Handle(ListarUsuarioRoleRequest command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(command.Email))
        {
            return [];
        }

        string keyCache = $"key_ListarUsuarioRoleHandler_{command.Email}";
        if (!_memoryCache.TryGetValue(keyCache, out List<ListarUsuarioRoleResponse>? listaUsuarioRoles))
        {
            var linq = await _repository.Listar(command.Email);

            foreach (var item in linq)
            {
                var result = new ListarUsuarioRoleResponse()
                {
                    UsuarioId = item.UsuarioId,
                    RoleId = item.RoleId,
                    Roles = item.Roles
                };

                listaUsuarioRoles?.Add(result);
            }

            _memoryCache.Set(keyCache, listaUsuarioRoles, TimeSpan.FromMinutes(1));
        }

        return listaUsuarioRoles ?? [];
    }
}