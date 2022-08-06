using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoInventarioASP.Migrations
{
    public partial class AgregandoNuevasTablas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Displays_Computadoras_ComputadoraId",
                table: "Displays");

            migrationBuilder.DropIndex(
                name: "IX_Displays_ComputadoraId",
                table: "Displays");

            migrationBuilder.CreateTable(
                name: "Celulares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumSerie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumInv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Celulares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Celulares_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Entradas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Equipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumInvEquipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entrega = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    observaciones = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entradas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Laptops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumInv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreDepartamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false),
                    Mac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laptops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Laptops_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Salidas",
                columns: table => new
                {
                    EntradaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    salida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaSalida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    observaciones = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salidas", x => x.EntradaId);
                });

            migrationBuilder.CreateTable(
                name: "Scanners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumSerie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumInv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scanners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scanners_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Telefonos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumSerie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumInv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefonos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Telefonos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Celulares_UsuarioId",
                table: "Celulares",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Laptops_UsuarioId",
                table: "Laptops",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Scanners_UsuarioId",
                table: "Scanners",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Telefonos_UsuarioId",
                table: "Telefonos",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Celulares");

            migrationBuilder.DropTable(
                name: "Entradas");

            migrationBuilder.DropTable(
                name: "Laptops");

            migrationBuilder.DropTable(
                name: "Salidas");

            migrationBuilder.DropTable(
                name: "Scanners");

            migrationBuilder.DropTable(
                name: "Telefonos");

            migrationBuilder.CreateIndex(
                name: "IX_Displays_ComputadoraId",
                table: "Displays",
                column: "ComputadoraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Displays_Computadoras_ComputadoraId",
                table: "Displays",
                column: "ComputadoraId",
                principalTable: "Computadoras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
