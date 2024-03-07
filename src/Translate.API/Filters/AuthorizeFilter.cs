using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Translate.API.Filters.Base;
using Translate.Application.Commands.UsuariosRoles.ListarUsuarioRole;
using Translate.Application.Handlers.UsuariosRoles.ListarUsuarioRoleCache;
using Translate.Domain.Enums;
using static junioranheu_utils_package.Fixtures.Get;

namespace Translate.API.Filters;

public sealed class AuthAttribute : TypeFilterAttribute
{
    public AuthAttribute(params UsuarioRoleEnum[] roles) : base(typeof(AuthorizeFilter))
    {
        Arguments = [roles];
    }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public sealed class AuthorizeFilter(params UsuarioRoleEnum[] roles) : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private readonly int[] _rolesNecessarias = NormalizarRoles(roles);

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (IsUsuarioAutenticado(context))
        {
            IEnumerable<ListarUsuarioRoleResponse>? usuarioRoles = await ListarUsuarioRoles(context);
            IsUsuarioTemAcesso(context, usuarioRoles, _rolesNecessarias);
        }
    }

    private static bool IsUsuarioAutenticado(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity!.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return false;
        }

        return true;
    }

    private static async Task<IEnumerable<ListarUsuarioRoleResponse>?> ListarUsuarioRoles(AuthorizationFilterContext context)
    {
        var service = context.HttpContext.RequestServices.GetService<ListarUsuarioRoleCacheHandler>() ?? throw new Exception(ObterDescricaoEnum(CodigoErroEnum.ErroInterno));
        string? email = new BaseFilter().BaseObterUsuarioEmail(context);

        var request = new ListarUsuarioRoleRequest()
        {
            Email = email
        };

        IEnumerable<ListarUsuarioRoleResponse>? usuarioRoles = await service.Handle(request, new CancellationTokenSource().Token);
        return usuarioRoles;
    }

    private static bool IsUsuarioTemAcesso(AuthorizationFilterContext context, IEnumerable<ListarUsuarioRoleResponse>? usuarioRoles, int[] _rolesNecessarias)
    {
        if (_rolesNecessarias.Length == 0)
        {
            return true;
        }

        bool isUsuarioTemAcesso = usuarioRoles!.Any(x => _rolesNecessarias.Any(y => x.RoleId == (UsuarioRoleEnum)y));

        if (!isUsuarioTemAcesso)
        {
            context.Result = new ObjectResult("Você não tem permissão para acessar este recurso.")
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

            return false;
        }

        return true;
    }

    private static int[] NormalizarRoles(UsuarioRoleEnum[] roles)
    {
        List<int> r = [];

        foreach (var role in roles)
        {
            r.Add((int)role);
        }

        return [.. r];
    }
}