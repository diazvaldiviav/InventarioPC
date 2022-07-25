using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoInventarioASP.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Impresoras",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumSerie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumInv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Impresoras", x => x.Id);
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
                name: "Teclados",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumSerie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumInv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoConexion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teclados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreDepartamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreArea = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
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
                name: "Computadoras",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumInv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreDepartamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false),
                    Mac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImpresoraId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NombreUsuarioId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherBoardId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TecladoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImprNumInv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeclNumInv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherBoardMarca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscoDuroCap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscoDuroTipoCon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemoriaRamCap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemoriaRamTec = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MicroTecn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Computadoras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Computadoras_Impresoras_ImpresoraId",
                        column: x => x.ImpresoraId,
                        principalTable: "Impresoras",
                        principalColumn: "Id",
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Computadoras_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
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
                name: "Displays",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumInv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumSerie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComputadoraId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumInvPc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false)
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
                });

            migrationBuilder.InsertData(
                table: "Impresoras",
                columns: new[] { "Id", "Marca", "NumInv", "NumSerie", "estado" },
                values: new object[,]
                {
                    { "7bf90439-da5e-4734-ab1e-413f25d5056f", "HP", "2145", "jjdj778skld887f333f", 0 },
                    { "a415186c-9939-45c4-9272-c376fbfc7759", "Epson", "8919", "jjdj778sk66he4535ef", 0 }
                });

            migrationBuilder.InsertData(
                table: "MicroProcesadores",
                columns: new[] { "NumSerieId", "Marca", "Tecnologia", "estado" },
                values: new object[] { "90a5c486-a934-4b8d-8571-955724d3f930", "Intel", "CoreI3-9", 0 });

            migrationBuilder.InsertData(
                table: "Teclados",
                columns: new[] { "Id", "Marca", "NumInv", "NumSerie", "TipoConexion", "estado" },
                values: new object[,]
                {
                    { "2c4d17b1-cfde-454c-8297-9a9ebf5cbfaa", "DELL", "6312", "7h7g8f8fke9gtr54t6y67uyh", "usb", 0 },
                    { "f4d44982-7950-4193-b4de-59836f7140b3", "Delton", "6431", "67tun7588nd7y7y4t6y45rtg", "usb", 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Nombre", "password", "permisos", "username" },
                values: new object[] { "1", "juca@ki.com", "Victor Diaz", "123", "admin", "victor" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "NombreArea", "NombreCompleto", "NombreDepartamento", "NombreUsuario" },
                values: new object[,]
                {
                    { "3bf5ba68-50be-4e94-adb7-ac843a907fb0", "UEB Sancti-Spiritus", "Fernando Alonso", "Tecnico", "fernando" },
                    { "ec27f160-d8ac-4836-b590-255b7cf00898", "Economia", "Juan Perez", "Finanzas", "juan.perez" }
                });

            migrationBuilder.InsertData(
                table: "MotherBoards",
                columns: new[] { "NumSerieId", "Marca", "MicroProcesadorId", "estado" },
                values: new object[] { "604c5fde-0ede-44ec-b032-ae6fc4f904c0", "Asus", "90a5c486-a934-4b8d-8571-955724d3f930", 0 });

            migrationBuilder.InsertData(
                table: "Computadoras",
                columns: new[] { "Id", "DiscoDuroCap", "DiscoDuroTipoCon", "ImprNumInv", "ImpresoraId", "Mac", "MemoriaRamCap", "MemoriaRamTec", "MicroTecn", "MotherBoardId", "MotherBoardMarca", "Nombre", "NombreArea", "NombreDepartamento", "NombreUsuarioId", "NumInv", "NumIp", "SO", "TeclNumInv", "TecladoId", "UserName", "UsuarioId", "estado" },
                values: new object[,]
                {
                    { "03c9270a-e324-44ba-9c23-ef66038bc3e4", "256", "sata", "2145", "7bf90439-da5e-4734-ab1e-413f25d5056f", "3c4r55rf4g6622", "4", "DDR4", "CoreI3-9", "604c5fde-0ede-44ec-b032-ae6fc4f904c0", "Asus", "OFC-ECO-CAB", "UEB Economia", "Finanzas", "3bf5ba68-50be-4e94-adb7-ac843a907fb0", "56911", "172.19.229.11", "Win 7 32", "6431", "f4d44982-7950-4193-b4de-59836f7140b3", "fernando", null, 0 },
                    { "0634a829-7d07-43b2-9d81-6015d41eb902", "256", "sata", "2145", "7bf90439-da5e-4734-ab1e-413f25d5056f", "3c4r55rf4g6622", "4", "DDR4", "CoreI3-9", "604c5fde-0ede-44ec-b032-ae6fc4f904c0", "Asus", "OFC-ECO-CAB", "UEB Economia", "Finanzas", "3bf5ba68-50be-4e94-adb7-ac843a907fb0", "56911", "172.19.229.11", "Win 7 32", "6312", "2c4d17b1-cfde-454c-8297-9a9ebf5cbfaa", "fernando", null, 0 },
                    { "5d365484-fc9c-4f88-926e-0fa4ec352150", "256", "sata", "8919", "a415186c-9939-45c4-9272-c376fbfc7759", "3c4r55rf4g6622", "4", "DDR4", "CoreI3-9", "604c5fde-0ede-44ec-b032-ae6fc4f904c0", "Asus", "OFC-ECO-CAB", "UEB Economia", "Finanzas", "ec27f160-d8ac-4836-b590-255b7cf00898", "56911", "172.19.229.11", "Win 7 32", "6431", "f4d44982-7950-4193-b4de-59836f7140b3", "juan.perez", null, 0 },
                    { "96b6bae8-eeab-424e-b88c-10c5f4e746fd", "256", "sata", "8919", "a415186c-9939-45c4-9272-c376fbfc7759", "3c4r55rf4g6622", "4", "DDR4", "CoreI3-9", "604c5fde-0ede-44ec-b032-ae6fc4f904c0", "Asus", "OFC-ECO-CAB", "UEB Economia", "Finanzas", "ec27f160-d8ac-4836-b590-255b7cf00898", "56911", "172.19.229.11", "Win 7 32", "6312", "2c4d17b1-cfde-454c-8297-9a9ebf5cbfaa", "juan.perez", null, 0 },
                    { "a8e420e4-ee36-4432-b133-8cb11fa1b5fd", "256", "sata", "8919", "a415186c-9939-45c4-9272-c376fbfc7759", "3c4r55rf4g6622", "4", "DDR4", "CoreI3-9", "604c5fde-0ede-44ec-b032-ae6fc4f904c0", "Asus", "OFC-ECO-CAB", "UEB Economia", "Finanzas", "3bf5ba68-50be-4e94-adb7-ac843a907fb0", "56911", "172.19.229.11", "Win 7 32", "6312", "2c4d17b1-cfde-454c-8297-9a9ebf5cbfaa", "fernando", null, 0 },
                    { "c3f2934d-46ed-4393-b237-7d71a9430db9", "256", "sata", "8919", "a415186c-9939-45c4-9272-c376fbfc7759", "3c4r55rf4g6622", "4", "DDR4", "CoreI3-9", "604c5fde-0ede-44ec-b032-ae6fc4f904c0", "Asus", "OFC-ECO-CAB", "UEB Economia", "Finanzas", "3bf5ba68-50be-4e94-adb7-ac843a907fb0", "56911", "172.19.229.11", "Win 7 32", "6431", "f4d44982-7950-4193-b4de-59836f7140b3", "fernando", null, 0 },
                    { "ce2dfd46-782b-4c35-86da-2407d127ae3e", "256", "sata", "2145", "7bf90439-da5e-4734-ab1e-413f25d5056f", "3c4r55rf4g6622", "4", "DDR4", "CoreI3-9", "604c5fde-0ede-44ec-b032-ae6fc4f904c0", "Asus", "OFC-ECO-CAB", "UEB Economia", "Finanzas", "ec27f160-d8ac-4836-b590-255b7cf00898", "56911", "172.19.229.11", "Win 7 32", "6312", "2c4d17b1-cfde-454c-8297-9a9ebf5cbfaa", "juan.perez", null, 0 },
                    { "ed98be34-e0ef-4988-ab91-975b340d0149", "256", "sata", "2145", "7bf90439-da5e-4734-ab1e-413f25d5056f", "3c4r55rf4g6622", "4", "DDR4", "CoreI3-9", "604c5fde-0ede-44ec-b032-ae6fc4f904c0", "Asus", "OFC-ECO-CAB", "UEB Economia", "Finanzas", "ec27f160-d8ac-4836-b590-255b7cf00898", "56911", "172.19.229.11", "Win 7 32", "6431", "f4d44982-7950-4193-b4de-59836f7140b3", "juan.perez", null, 0 }
                });

            migrationBuilder.InsertData(
                table: "DiscosDuro",
                columns: new[] { "NumSerieId", "Capacidad", "Marca", "MotherBoardId", "TipoConexion", "estado" },
                values: new object[] { "f4ff99c8-922b-435c-9b50-870bb3c65e44", "256", "toshiba", "604c5fde-0ede-44ec-b032-ae6fc4f904c0", "sata", 0 });

            migrationBuilder.InsertData(
                table: "MemoriasRam",
                columns: new[] { "KayNumSerieId", "Capacidad", "Marca", "MotherBoardId", "Tecnologia", "estado" },
                values: new object[] { "eaa0e024-7070-4e55-a2f5-7e6e91944158", "4", "Kingston", "604c5fde-0ede-44ec-b032-ae6fc4f904c0", "DDR4", 0 });

            migrationBuilder.InsertData(
                table: "Displays",
                columns: new[] { "Id", "ComputadoraId", "Marca", "NumInv", "NumInvPc", "NumSerie", "estado" },
                values: new object[,]
                {
                    { "13fb5896-c170-4774-b4a3-f44b0f09bdf1", "0634a829-7d07-43b2-9d81-6015d41eb902", "Samsung", "4321", "56911", "7h7g8f8fke956rf67uuj43ed", 0 },
                    { "89a3e86e-2b44-462f-928b-3962c5c70350", "03c9270a-e324-44ba-9c23-ef66038bc3e4", "Samsung", "4321", "56911", "7h7g8f8fke956rf67uuj43ed", 0 },
                    { "ab6a5e7d-f048-4729-ab09-104b1da0fd3e", "ce2dfd46-782b-4c35-86da-2407d127ae3e", "Samsung", "4321", "56911", "7h7g8f8fke956rf67uuj43ed", 0 },
                    { "b2519b1b-ba4e-4022-a83d-222b23aef323", "a8e420e4-ee36-4432-b133-8cb11fa1b5fd", "Samsung", "4321", "56911", "7h7g8f8fke956rf67uuj43ed", 0 },
                    { "cc043efe-df72-4ab4-be4f-d06cd3994dc1", "c3f2934d-46ed-4393-b237-7d71a9430db9", "Samsung", "4321", "56911", "7h7g8f8fke956rf67uuj43ed", 0 },
                    { "d85ab63e-2fcf-4437-bb41-66dfb0312e98", "5d365484-fc9c-4f88-926e-0fa4ec352150", "Samsung", "4321", "56911", "7h7g8f8fke956rf67uuj43ed", 0 },
                    { "f3b5cb85-a2ed-4313-853a-cfc8f1eb331e", "96b6bae8-eeab-424e-b88c-10c5f4e746fd", "Samsung", "4321", "56911", "7h7g8f8fke956rf67uuj43ed", 0 },
                    { "f89ec1c8-c59e-4968-8a82-d96c0ad8686f", "ed98be34-e0ef-4988-ab91-975b340d0149", "Samsung", "4321", "56911", "7h7g8f8fke956rf67uuj43ed", 0 }
                });

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
                name: "IX_MemoriasRam_MotherBoardId",
                table: "MemoriasRam",
                column: "MotherBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_MotherBoards_MicroProcesadorId",
                table: "MotherBoards",
                column: "MicroProcesadorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscosDuro");

            migrationBuilder.DropTable(
                name: "Displays");

            migrationBuilder.DropTable(
                name: "MemoriasRam");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Computadoras");

            migrationBuilder.DropTable(
                name: "Impresoras");

            migrationBuilder.DropTable(
                name: "MotherBoards");

            migrationBuilder.DropTable(
                name: "Teclados");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "MicroProcesadores");
        }
    }
}
