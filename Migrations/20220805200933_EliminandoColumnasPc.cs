using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoInventarioASP.Migrations
{
    public partial class EliminandoColumnasPc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscoDuroCap",
                table: "Computadoras");

            migrationBuilder.DropColumn(
                name: "DiscoDuroTipoCon",
                table: "Computadoras");

            migrationBuilder.DropColumn(
                name: "MemoriaRamCap",
                table: "Computadoras");

            migrationBuilder.DropColumn(
                name: "MemoriaRamTec",
                table: "Computadoras");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DiscoDuroCap",
                table: "Computadoras",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DiscoDuroTipoCon",
                table: "Computadoras",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MemoriaRamCap",
                table: "Computadoras",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MemoriaRamTec",
                table: "Computadoras",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
