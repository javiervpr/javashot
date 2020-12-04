using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace JavaShotAPI.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class addexplicacionrespuesta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExplicacionRespuesta",
                table: "Pregunta",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExplicacionRespuesta",
                table: "Pregunta");
        }
    }
}
