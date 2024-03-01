using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Translate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixEntitadeUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosRoles_Usuarios_UsuariosId",
                table: "UsuariosRoles");

            migrationBuilder.DropIndex(
                name: "IX_UsuariosRoles_UsuariosId",
                table: "UsuariosRoles");

            migrationBuilder.DropColumn(
                name: "UsuariosId",
                table: "UsuariosRoles");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioId",
                table: "UsuariosRoles",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRoles_UsuarioId",
                table: "UsuariosRoles",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosRoles_Usuarios_UsuarioId",
                table: "UsuariosRoles",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosRoles_Usuarios_UsuarioId",
                table: "UsuariosRoles");

            migrationBuilder.DropIndex(
                name: "IX_UsuariosRoles_UsuarioId",
                table: "UsuariosRoles");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "UsuariosRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuariosId",
                table: "UsuariosRoles",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRoles_UsuariosId",
                table: "UsuariosRoles",
                column: "UsuariosId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosRoles_Usuarios_UsuariosId",
                table: "UsuariosRoles",
                column: "UsuariosId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
