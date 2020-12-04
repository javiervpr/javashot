using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JavaShotAPI.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class aact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartidaPregunta_Partida_PartidaID",
                table: "PartidaPregunta");

            migrationBuilder.DropForeignKey(
                name: "FK_PartidaPregunta_Pregunta_PreguntaID",
                table: "PartidaPregunta");

            migrationBuilder.AlterColumn<Guid>(
                name: "PreguntaID",
                table: "PartidaPregunta",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PartidaID",
                table: "PartidaPregunta",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "HistorialPunto",
                columns: table => new
                {
                    HistorialPuntoID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Puntos = table.Column<int>(type: "int", nullable: false),
                    PartidaID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialPunto", x => x.HistorialPuntoID);
                    table.ForeignKey(
                        name: "FK_HistorialPunto_Partida_PartidaID",
                        column: x => x.PartidaID,
                        principalTable: "Partida",
                        principalColumn: "PartidaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistorialPunto_PartidaID",
                table: "HistorialPunto",
                column: "PartidaID");

            migrationBuilder.AddForeignKey(
                name: "FK_PartidaPregunta_Partida_PartidaID",
                table: "PartidaPregunta",
                column: "PartidaID",
                principalTable: "Partida",
                principalColumn: "PartidaID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PartidaPregunta_Pregunta_PreguntaID",
                table: "PartidaPregunta",
                column: "PreguntaID",
                principalTable: "Pregunta",
                principalColumn: "PreguntaID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartidaPregunta_Partida_PartidaID",
                table: "PartidaPregunta");

            migrationBuilder.DropForeignKey(
                name: "FK_PartidaPregunta_Pregunta_PreguntaID",
                table: "PartidaPregunta");

            migrationBuilder.DropTable(
                name: "HistorialPunto");

            migrationBuilder.AlterColumn<Guid>(
                name: "PreguntaID",
                table: "PartidaPregunta",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "PartidaID",
                table: "PartidaPregunta",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_PartidaPregunta_Partida_PartidaID",
                table: "PartidaPregunta",
                column: "PartidaID",
                principalTable: "Partida",
                principalColumn: "PartidaID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartidaPregunta_Pregunta_PreguntaID",
                table: "PartidaPregunta",
                column: "PreguntaID",
                principalTable: "Pregunta",
                principalColumn: "PreguntaID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
