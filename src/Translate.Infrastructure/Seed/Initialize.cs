using Microsoft.EntityFrameworkCore;
using Translate.Domain.Entities;
using Translate.Domain.Enums;
using Translate.Infrastructure.Data;
using static junioranheu_utils_package.Fixtures.Encrypt;
using static junioranheu_utils_package.Fixtures.Get;

namespace Translate.Infrastructure.Seed;

public static class DbInitializer
{
    public static async Task Initialize(TranslateContext context, bool isAplicarMigrations, bool isResetar)
    {
        context.Database.SetCommandTimeout(600);
        // string script : context.Database.GenerateCreateScript();

        if (isAplicarMigrations)
        {
            await context.Database.MigrateAsync();
        }
        else if (isResetar)
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }

        if (isAplicarMigrations || isResetar)
        {
           await Seed(context, GerarHorarioBrasilia());
        }
    }

    public static async Task Seed(TranslateContext context, DateTime dataAgora)
    {
        #region seed_usuarios           
        if (!await context.Roles.AnyAsync())
        {
            await context.Roles.AddAsync(new Role(roleId: UsuarioRoleEnum.Administrador, tipo: ObterDescricaoEnum(UsuarioRoleEnum.Administrador), descricao: "Administrador do sistema com acesso total", isAtivo: true, data: dataAgora));
            await context.Roles.AddAsync(new Role(roleId: UsuarioRoleEnum.Comum, tipo: ObterDescricaoEnum(UsuarioRoleEnum.Comum), descricao: "Usuário comum com acesso limitado", isAtivo: true, data: dataAgora));
        }

        Guid usuarioId1 = Guid.NewGuid();
        Guid usuarioId2 = Guid.NewGuid();

        if (!await context.Usuarios.AnyAsync())
        {
            await context.Usuarios.AddAsync(new Usuario(usuarioId: usuarioId1, nomeCompleto: "Administrador", nomeUsuarioSistema: "adm", email: "adm@Hotmail.com", senha: Criptografar("123"), isAtivo: true, isLatest: true, data: GerarHorarioBrasilia()));
            await context.Usuarios.AddAsync(new Usuario(usuarioId: usuarioId2, nomeCompleto: "Junior Souza", nomeUsuarioSistema: "junioranheu", email: "junioranheu@Hotmail.com", senha: Criptografar("123"), isAtivo: true, isLatest: true, data: GerarHorarioBrasilia()));
        }

        if (!await context.UsuariosRoles.AnyAsync())
        {
            await context.UsuariosRoles.AddAsync(new UsuarioRole(usuarioRoleId: Guid.NewGuid(), usuarioId: usuarioId1, roleId: UsuarioRoleEnum.Administrador, data: GerarHorarioBrasilia()));
            await context.UsuariosRoles.AddAsync(new UsuarioRole(usuarioRoleId: Guid.NewGuid(), usuarioId: usuarioId1, roleId: UsuarioRoleEnum.Comum, data: GerarHorarioBrasilia()));
            await context.UsuariosRoles.AddAsync(new UsuarioRole(usuarioRoleId: Guid.NewGuid(), usuarioId: usuarioId2, roleId: UsuarioRoleEnum.Comum, data: GerarHorarioBrasilia()));
        }
        #endregion

        await context.SaveChangesAsync();
    }
}