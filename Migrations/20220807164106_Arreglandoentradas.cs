﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoInventarioASP.Migrations
{
    public partial class Arreglandoentradas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entradas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Equipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumInvEquipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entrega = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lugar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    observaciones = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entradas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MicroProcesadores",
                columns: table => new
                {
                    NumSerieId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tecnologia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MicroProcesadores", x => x.NumSerieId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    permisos = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreDepartamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cargo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Salidas",
                columns: table => new
                {
                    EntradaId = table.Column<int>(type: "int", nullable: false),
                    salida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaSalida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    observaciones = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salidas", x => x.EntradaId);
                    table.ForeignKey(
                        name: "FK_Salidas_Entradas_EntradaId",
                        column: x => x.EntradaId,
                        principalTable: "Entradas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MotherBoards",
                columns: table => new
                {
                    NumSerieId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MicroProcesadorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotherBoards", x => x.NumSerieId);
                    table.ForeignKey(
                        name: "FK_MotherBoards_MicroProcesadores_MicroProcesadorId",
                        column: x => x.MicroProcesadorId,
                        principalTable: "MicroProcesadores",
                        principalColumn: "NumSerieId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "Impresoras",
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
                    table.PrimaryKey("PK_Impresoras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Impresoras_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "Teclados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumSerie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumInv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoConexion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teclados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teclados_Usuarios_UsuarioId",
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

            migrationBuilder.CreateTable(
                name: "Upss",
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
                    table.PrimaryKey("PK_Upss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Upss_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscosDuro",
                columns: table => new
                {
                    NumSerieId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoConexion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherBoardId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscosDuro", x => x.NumSerieId);
                    table.ForeignKey(
                        name: "FK_DiscosDuro_MotherBoards_MotherBoardId",
                        column: x => x.MotherBoardId,
                        principalTable: "MotherBoards",
                        principalColumn: "NumSerieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemoriasRam",
                columns: table => new
                {
                    KayNumSerieId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tecnologia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherBoardId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemoriasRam", x => x.KayNumSerieId);
                    table.ForeignKey(
                        name: "FK_MemoriasRam_MotherBoards_MotherBoardId",
                        column: x => x.MotherBoardId,
                        principalTable: "MotherBoards",
                        principalColumn: "NumSerieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Computadoras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumInv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreDepartamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sello = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpsInv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false),
                    Mac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImpresoraId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    MotherBoardId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TecladoId = table.Column<int>(type: "int", nullable: false),
                    UpsId = table.Column<int>(type: "int", nullable: false),
                    ImprNumInv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeclNumInv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherBoardMarca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MicroTecn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Computadoras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Computadoras_Impresoras_ImpresoraId",
                        column: x => x.ImpresoraId,
                        principalTable: "Impresoras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Computadoras_MotherBoards_MotherBoardId",
                        column: x => x.MotherBoardId,
                        principalTable: "MotherBoards",
                        principalColumn: "NumSerieId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Computadoras_Teclados_TecladoId",
                        column: x => x.TecladoId,
                        principalTable: "Teclados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Computadoras_Upss_UpsId",
                        column: x => x.UpsId,
                        principalTable: "Upss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Computadoras_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Displays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumInv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumSerie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComputadoraId = table.Column<int>(type: "int", nullable: false),
                    NumInvPc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Displays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Displays_Computadoras_ComputadoraId",
                        column: x => x.ComputadoraId,
                        principalTable: "Computadoras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Displays_Usuarios_UsuarioId",
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
                name: "IX_Computadoras_ImpresoraId",
                table: "Computadoras",
                column: "ImpresoraId");

            migrationBuilder.CreateIndex(
                name: "IX_Computadoras_MotherBoardId",
                table: "Computadoras",
                column: "MotherBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Computadoras_TecladoId",
                table: "Computadoras",
                column: "TecladoId");

            migrationBuilder.CreateIndex(
                name: "IX_Computadoras_UpsId",
                table: "Computadoras",
                column: "UpsId");

            migrationBuilder.CreateIndex(
                name: "IX_Computadoras_UsuarioId",
                table: "Computadoras",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscosDuro_MotherBoardId",
                table: "DiscosDuro",
                column: "MotherBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Displays_ComputadoraId",
                table: "Displays",
                column: "ComputadoraId");

            migrationBuilder.CreateIndex(
                name: "IX_Displays_UsuarioId",
                table: "Displays",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Impresoras_UsuarioId",
                table: "Impresoras",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Laptops_UsuarioId",
                table: "Laptops",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_MemoriasRam_MotherBoardId",
                table: "MemoriasRam",
                column: "MotherBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_MotherBoards_MicroProcesadorId",
                table: "MotherBoards",
                column: "MicroProcesadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Scanners_UsuarioId",
                table: "Scanners",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Teclados_UsuarioId",
                table: "Teclados",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Telefonos_UsuarioId",
                table: "Telefonos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Upss_UsuarioId",
                table: "Upss",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Celulares");

            migrationBuilder.DropTable(
                name: "DiscosDuro");

            migrationBuilder.DropTable(
                name: "Displays");

            migrationBuilder.DropTable(
                name: "Laptops");

            migrationBuilder.DropTable(
                name: "MemoriasRam");

            migrationBuilder.DropTable(
                name: "Salidas");

            migrationBuilder.DropTable(
                name: "Scanners");

            migrationBuilder.DropTable(
                name: "Telefonos");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Computadoras");

            migrationBuilder.DropTable(
                name: "Entradas");

            migrationBuilder.DropTable(
                name: "Impresoras");

            migrationBuilder.DropTable(
                name: "MotherBoards");

            migrationBuilder.DropTable(
                name: "Teclados");

            migrationBuilder.DropTable(
                name: "Upss");

            migrationBuilder.DropTable(
                name: "MicroProcesadores");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}