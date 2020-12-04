using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace JavaShotAPI.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class changeNameContendioForContenido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Contendio",
                table: "Respuesta",
                newName: "Contenido");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Contenido",
                table: "Respuesta",
                newName: "Contendio");
        }
    }
}
