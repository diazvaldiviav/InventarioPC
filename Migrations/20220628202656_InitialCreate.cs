using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoInventario.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Discos Duro",
                columns: table => new
                {
                    NumSerieId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TipoConexion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacidad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discos Duro", x => x.NumSerieId);
                });

            migrationBuilder.CreateTable(
                name: "Impresoras",
                columns: table => new
                {
                    NumInvId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumSerie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Impresoras", x => x.NumInvId);
                });

            migrationBuilder.CreateTable(
                name: "Memorias Ram",
                columns: table => new
                {
                    NumSerieId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tecnologia = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memorias Ram", x => x.NumSerieId);
                });

            migrationBuilder.CreateTable(
                name: "Microprocesadores",
                columns: table => new
                {
                    NumSerieId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tecnologia = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Microprocesadores", x => x.NumSerieId);
                });

            migrationBuilder.CreateTable(
                name: "Monitores",
                columns: table => new
                {
                    NumInvId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumSerie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monitores", x => x.NumInvId);
                });

            migrationBuilder.CreateTable(
                name: "MotherBoards",
                columns: table => new
                {
                    NumSerieId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotherBoards", x => x.NumSerieId);
                });

            migrationBuilder.CreateTable(
                name: "Teclados",
                columns: table => new
                {
                    NumInvId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumSerie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoConexion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teclados", x => x.NumInvId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    NombreUsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreDepartamento = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    NombreArea = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.NombreUsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Computadoras",
                columns: table => new
                {
                    NumInvId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NombreDepartamento = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    NombreArea = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    MemoriaRamId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Mac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImpresoraId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NombreUsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DiscoDuroId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MicroProcesadorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MotherBoardId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MonitorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TecladoId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Computadoras", x => x.NumInvId);
                    table.ForeignKey(
                        name: "FK_Computadoras_Discos Duro_DiscoDuroId",
                        column: x => x.DiscoDuroId,
                        principalTable: "Discos Duro",
                        principalColumn: "NumSerieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Computadoras_Impresoras_ImpresoraId",
                        column: x => x.ImpresoraId,
                        principalTable: "Impresoras",
                        principalColumn: "NumInvId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Computadoras_Memorias Ram_MemoriaRamId",
                        column: x => x.MemoriaRamId,
                        principalTable: "Memorias Ram",
                        principalColumn: "NumSerieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Computadoras_Microprocesadores_MicroProcesadorId",
                        column: x => x.MicroProcesadorId,
                        principalTable: "Microprocesadores",
                        principalColumn: "NumSerieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Computadoras_Monitores_MonitorId",
                        column: x => x.MonitorId,
                        principalTable: "Monitores",
                        principalColumn: "NumInvId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Computadoras_MotherBoards_MotherBoardId",
                        column: x => x.MotherBoardId,
                        principalTable: "MotherBoards",
                        principalColumn: "NumSerieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Computadoras_Teclados_TecladoId",
                        column: x => x.TecladoId,
                        principalTable: "Teclados",
                        principalColumn: "NumInvId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Computadoras_Usuarios_NombreUsuarioId",
                        column: x => x.NombreUsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "NombreUsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Discos Duro",
                columns: new[] { "NumSerieId", "Capacidad", "Marca", "TipoConexion" },
                values: new object[,]
                {
                    { "2813qwd8q", "256gb", "toshiba", "sata" },
                    { "58523eeee", "500gb", "samsung", "sata" }
                });

            migrationBuilder.InsertData(
                table: "Impresoras",
                columns: new[] { "NumInvId", "Marca", "NumSerie" },
                values: new object[,]
                {
                    { "67854", "Epson", "jjdj778sk66he4535ef" },
                    { "78354", "HP", "jjdj778skld887f333f" }
                });

            migrationBuilder.InsertData(
                table: "Memorias Ram",
                columns: new[] { "NumSerieId", "Capacidad", "Marca", "Tecnologia" },
                values: new object[,]
                {
                    { "67tun7588nd7y7y4t6yac78", "2gb", "HyperX", "DD3" },
                    { "7h7g8f8fke9gtr54t6yac52", "4gb", "Kingston", "DD4" }
                });

            migrationBuilder.InsertData(
                table: "Microprocesadores",
                columns: new[] { "NumSerieId", "Marca", "Tecnologia" },
                values: new object[,]
                {
                    { "fe2de405c38e4c9034rt", "AMD", "Core-I-20" },
                    { "fe2de405c38e4c90ac52", "Intel", "Core-I-5" }
                });

            migrationBuilder.InsertData(
                table: "Monitores",
                columns: new[] { "NumInvId", "Marca", "NumSerie" },
                values: new object[,]
                {
                    { "4321", "Samsung", "7h7g8f8fke956rf67uuj43ed" },
                    { "7843", "Acer", "7h7g8f8fke9gtr54t6yac52" }
                });

            migrationBuilder.InsertData(
                table: "MotherBoards",
                columns: new[] { "NumSerieId", "Marca" },
                values: new object[,]
                {
                    { "67tun7588nd7y7y4t6yrf54", "DELL" },
                    { "7h7g8f8fke964744t6yac12", "Asus" }
                });

            migrationBuilder.InsertData(
                table: "Teclados",
                columns: new[] { "NumInvId", "Marca", "NumSerie", "TipoConexion" },
                values: new object[,]
                {
                    { "6731", "DELL", "7h7g8f8fke9gtr54t6y67uyh", "usb" },
                    { "67344", "Delton", "67tun7588nd7y7y4t6y45rtg", "usb" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "NombreUsuarioId", "NombreArea", "NombreCompleto", "NombreDepartamento" },
                values: new object[,]
                {
                    { "fernando", "UEB Sancti-Spiritus", "Fernando Alonso", "Tecnico" },
                    { "juan.perez", "Economia", "Juan Perez", "Finanzas" }
                });

            migrationBuilder.InsertData(
                table: "Computadoras",
                columns: new[] { "NumInvId", "DiscoDuroId", "ImpresoraId", "Mac", "MemoriaRamId", "MicroProcesadorId", "MonitorId", "MotherBoardId", "Nombre", "NombreArea", "NombreDepartamento", "NombreUsuarioId", "NumIp", "TecladoId" },
                values: new object[] { "563411", "2813qwd8q", "67854", "3c-4r-55-rf-4g-66-22", "7h7g8f8fke9gtr54t6yac52", "fe2de405c38e4c90ac52", "7843", "7h7g8f8fke964744t6yac12", "OFC-ECO-CAB", "UEB Economia", "Finanzas", "juan.perez", "172.19.229.111", "6731" });

            migrationBuilder.InsertData(
                table: "Computadoras",
                columns: new[] { "NumInvId", "DiscoDuroId", "ImpresoraId", "Mac", "MemoriaRamId", "MicroProcesadorId", "MonitorId", "MotherBoardId", "Nombre", "NombreArea", "NombreDepartamento", "NombreUsuarioId", "NumIp", "TecladoId" },
                values: new object[] { "89064", "58523eeee", "78354", "3c-4f-00-4f-4d-43-d3", "67tun7588nd7y7y4t6yac78", "fe2de405c38e4c9034rt", "4321", "67tun7588nd7y7y4t6yrf54", "SSP-Tec-IOO", "UEB Sancti Spiritus", "Tecnico", "fernando", "172.19.229.111", "67344" });

            migrationBuilder.CreateIndex(
                name: "IX_Computadoras_DiscoDuroId",
                table: "Computadoras",
                column: "DiscoDuroId");

            migrationBuilder.CreateIndex(
                name: "IX_Computadoras_ImpresoraId",
                table: "Computadoras",
                column: "ImpresoraId");

            migrationBuilder.CreateIndex(
                name: "IX_Computadoras_MemoriaRamId",
                table: "Computadoras",
                column: "MemoriaRamId");

            migrationBuilder.CreateIndex(
                name: "IX_Computadoras_MicroProcesadorId",
                table: "Computadoras",
                column: "MicroProcesadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Computadoras_MonitorId",
                table: "Computadoras",
                column: "MonitorId");

            migrationBuilder.CreateIndex(
                name: "IX_Computadoras_MotherBoardId",
                table: "Computadoras",
                column: "MotherBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Computadoras_NombreUsuarioId",
                table: "Computadoras",
                column: "NombreUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Computadoras_TecladoId",
                table: "Computadoras",
                column: "TecladoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Computadoras");

            migrationBuilder.DropTable(
                name: "Discos Duro");

            migrationBuilder.DropTable(
                name: "Impresoras");

            migrationBuilder.DropTable(
                name: "Memorias Ram");

            migrationBuilder.DropTable(
                name: "Microprocesadores");

            migrationBuilder.DropTable(
                name: "Monitores");

            migrationBuilder.DropTable(
                name: "MotherBoards");

            migrationBuilder.DropTable(
                name: "Teclados");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
