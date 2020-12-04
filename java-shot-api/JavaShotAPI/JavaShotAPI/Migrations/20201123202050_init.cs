using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JavaShotAPI.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pregunta",
                columns: table => new
                {
                    PreguntaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pregunta", x => x.PreguntaID);
                });

            migrationBuilder.CreateTable(
                name: "Respuesta",
                columns: table => new
                {
                    RespuestaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Contendio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Correcta = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respuesta", x => x.RespuestaID);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    UsuarioID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.UsuarioID);
                });

            migrationBuilder.CreateTable(
                name: "PreguntaRespuesta",
                columns: table => new
                {
                    PreguntaRespuestaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PreguntaID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RespuestaID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreguntaRespuesta", x => x.PreguntaRespuestaID);
                    table.ForeignKey(
                        name: "FK_PreguntaRespuesta_Pregunta_PreguntaID",
                        column: x => x.PreguntaID,
                        principalTable: "Pregunta",
                        principalColumn: "PreguntaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreguntaRespuesta_Respuesta_RespuestaID",
                        column: x => x.RespuestaID,
                        principalTable: "Respuesta",
                        principalColumn: "RespuestaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Partida",
                columns: table => new
                {
                    PartidaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partida", x => x.PartidaID);
                    table.ForeignKey(
                        name: "FK_Partida_Usuario_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PartidaPregunta",
                columns: table => new
                {
                    PartidaPreguntaID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContestadaCorrectamente = table.Column<bool>(type: "bit", nullable: true),
                    Contestada = table.Column<bool>(type: "bit", nullable: false),
                    FechaRespuesta = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PartidaID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PreguntaID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartidaPregunta", x => x.PartidaPreguntaID);
                    table.ForeignKey(
                        name: "FK_PartidaPregunta_Partida_PartidaID",
                        column: x => x.PartidaID,
                        principalTable: "Partida",
                        principalColumn: "PartidaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartidaPregunta_Pregunta_PreguntaID",
                        column: x => x.PreguntaID,
                        principalTable: "Pregunta",
                        principalColumn: "PreguntaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Partida_UsuarioID",
                table: "Partida",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_PartidaPregunta_PartidaID",
                table: "PartidaPregunta",
                column: "PartidaID");

            migrationBuilder.CreateIndex(
                name: "IX_PartidaPregunta_PreguntaID",
                table: "PartidaPregunta",
                column: "PreguntaID");

            migrationBuilder.CreateIndex(
                name: "IX_PreguntaRespuesta_PreguntaID",
                table: "PreguntaRespuesta",
                column: "PreguntaID");

            migrationBuilder.CreateIndex(
                name: "IX_PreguntaRespuesta_RespuestaID",
                table: "PreguntaRespuesta",
                column: "RespuestaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartidaPregunta");

            migrationBuilder.DropTable(
                name: "PreguntaRespuesta");

            migrationBuilder.DropTable(
                name: "Partida");

            migrationBuilder.DropTable(
                name: "Pregunta");

            migrationBuilder.DropTable(
                name: "Respuesta");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
