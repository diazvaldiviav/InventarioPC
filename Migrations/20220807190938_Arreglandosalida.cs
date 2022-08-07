using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoInventarioASP.Migrations
{
    public partial class Arreglandosalida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Salidas",
                table: "Salidas");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Salidas",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Salidas",
                table: "Salidas",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Salidas_EntradaId",
                table: "Salidas",
                column: "EntradaId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Salidas",
                table: "Salidas");

            migrationBuilder.DropIndex(
                name: "IX_Salidas_EntradaId",
                table: "Salidas");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Salidas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Salidas",
                table: "Salidas",
                column: "EntradaId");
        }
    }
}
