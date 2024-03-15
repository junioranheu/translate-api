using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Translate.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorFrase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QtdCaracteres",
                table: "Frases",
                newName: "QtdCaracteresFraseTraduzida");

            migrationBuilder.RenameColumn(
                name: "Idioma",
                table: "Frases",
                newName: "QtdCaracteresFraseOriginal");

            migrationBuilder.RenameColumn(
                name: "Conteudo",
                table: "Frases",
                newName: "FraseTraduzida");

            migrationBuilder.AddColumn<string>(
                name: "FraseOriginal",
                table: "Frases",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "IdiomaOriginal",
                table: "Frases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdiomaTraduzido",
                table: "Frases",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FraseOriginal",
                table: "Frases");

            migrationBuilder.DropColumn(
                name: "IdiomaOriginal",
                table: "Frases");

            migrationBuilder.DropColumn(
                name: "IdiomaTraduzido",
                table: "Frases");

            migrationBuilder.RenameColumn(
                name: "QtdCaracteresFraseTraduzida",
                table: "Frases",
                newName: "QtdCaracteres");

            migrationBuilder.RenameColumn(
                name: "QtdCaracteresFraseOriginal",
                table: "Frases",
                newName: "Idioma");

            migrationBuilder.RenameColumn(
                name: "FraseTraduzida",
                table: "Frases",
                newName: "Conteudo");
        }
    }
}
