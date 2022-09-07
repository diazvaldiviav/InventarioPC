using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoInventarioASP.Migrations
{
    public partial class sembrandoDatos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MicroProcesadores",
                columns: new[] { "NumSerieId", "Marca", "Tecnologia", "estado" },
                values: new object[] { "Sin Micro", "-", "-", 1 });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Cargo", "NombreArea", "NombreCompleto", "NombreDepartamento", "NombreUsuario" },
                values: new object[] { 1, "-", "-", "-", "-", "Sin Trabajador" });

            migrationBuilder.InsertData(
                table: "Impresoras",
                columns: new[] { "Id", "Marca", "NumInv", "NumSerie", "UserName", "UsuarioId", "estado" },
                values: new object[] { 1, "-", "Sin Impresora", "-", "Sin Trabajador", 1, 1 });

            migrationBuilder.InsertData(
                table: "MotherBoards",
                columns: new[] { "NumSerieId", "Marca", "MicroProcesadorId", "estado" },
                values: new object[] { "Sin Board", "-", "Sin Micro", 1 });

            migrationBuilder.InsertData(
                table: "Teclados",
                columns: new[] { "Id", "Marca", "NumInv", "NumSerie", "TipoConexion", "UserName", "UsuarioId", "estado" },
                values: new object[] { 1, "-", "-", "Sin Teclado", "-", "Sin Trabajador", 1, 1 });

            migrationBuilder.InsertData(
                table: "Upss",
                columns: new[] { "Id", "Marca", "NumInv", "NumSerie", "UserName", "UsuarioId", "estado" },
                values: new object[] { 1, "Marca", "Sin Ups", "-", "Sin Trabajador", 1, 1 });

            migrationBuilder.InsertData(
                table: "Computadoras",
                columns: new[] { "Id", "ImprNumInv", "ImpresoraId", "Mac", "MicroTecn", "MotherBoardId", "MotherBoardMarca", "Nombre", "NombreArea", "NombreDepartamento", "NumInv", "NumIp", "SO", "Sello", "TeclNumInv", "TecladoId", "UpsId", "UpsInv", "UserName", "UsuarioId", "estado" },
                values: new object[] { 1, "Sin Impresora", 1, "-", "-", "Sin Board", "-", "-", "-", "-", "Sin Computadora", "-", "-", "-", "-", 1, 1, "Sin Ups", "Sin Trabajador", 1, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Computadoras",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Impresoras",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MotherBoards",
                keyColumn: "NumSerieId",
                keyValue: "Sin Board");

            migrationBuilder.DeleteData(
                table: "Teclados",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Upss",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MicroProcesadores",
                keyColumn: "NumSerieId",
                keyValue: "Sin Micro");

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
