using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoInventarioASP.Migrations
{
    public partial class agregandoequipoabajs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Equipo",
                table: "Bajas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Equipo",
                table: "Bajas");
        }
    }
}
