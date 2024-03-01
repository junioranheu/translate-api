using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Translate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixEntitadeUsuarios2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UsuariosRoles",
                newName: "UsuarioRoleId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Usuarios",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Roles",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Frases",
                newName: "FraseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsuarioRoleId",
                table: "UsuariosRoles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Usuarios",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Roles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "FraseId",
                table: "Frases",
                newName: "Id");
        }
    }
}
