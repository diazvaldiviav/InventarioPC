using Microsoft.EntityFrameworkCore;
using ProyectoInventarioASP.Models;

namespace ProyectoInventarioASP;

public class ComputadoraContext : DbContext
{
    public DbSet<DiscoDuro> DiscosDuro { get; set; }
    public DbSet<Computadora> Computadoras { get; set; }
    public DbSet<MicroProcesador> MicroProcesadores { get; set; }
    public DbSet<MemoriaRam> MemoriasRam { get; set; }
    public DbSet<MotherBoard> MotherBoards { get; set; }
    public DbSet<Teclado> Teclados { get; set; }
    public DbSet<Display> Displays { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Impresora> Impresoras { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Ups> Upss { get; set; }

    public ComputadoraContext(DbContextOptions<ComputadoraContext> options) : base(options) { }
}

//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         base.OnModelCreating(modelBuilder);
//         List<MicroProcesador> MicroProcesadorInit = new List<MicroProcesador>();
//         MicroProcesadorInit.Add(new MicroProcesador() { NumSerieId = Guid.NewGuid().ToString(), Marca = "Intel", Tecnologia = "CoreI3-9", estado = Estado.activo });


//         //cargar una board por micro
//         var boards = CargarMicros(MicroProcesadorInit);

//         //cargar telcados 
//         var teclados = CargarTeclados();

        //cargar Ups
        // var ups = CargarUps();

        // //cargar impresora
        // var impresoras = CargarImpresoras();

//         //cargar usuario
//         var usuarios = CargarUsuarios();

//         //cargar una memoria por motherboard

//         var memorias = CargarMemorias(boards);

//         //cargar un disco por motherboard
//         var discos = CargarDiscos(boards);

//         //cargar una computadora por cada motherboard, teclado,impresora,usuario
//         var computadora = CargarComputadoras(boards, impresoras, usuarios, teclados, discos, memorias, MicroProcesadorInit);

//         //cargar monitor por cada computadora
//         var monitores = CargarMonitor(computadora);

        //cargar usernames

        // var users = cargarUserNames();



//         //Inyeccion de datos
//         modelBuilder.Entity<DiscoDuro>().HasData(discos.ToArray());
//         modelBuilder.Entity<MicroProcesador>().HasData(MicroProcesadorInit);
//         modelBuilder.Entity<Display>().HasData(monitores.ToArray());
//         modelBuilder.Entity<MotherBoard>().HasData(boards.ToArray());
//         modelBuilder.Entity<Teclado>().HasData(teclados.ToArray());
//         modelBuilder.Entity<Computadora>().HasData(computadora.ToArray());
//         modelBuilder.Entity<Usuario>().HasData(usuarios);
//         modelBuilder.Entity<Impresora>().HasData(impresoras.ToArray());
//         modelBuilder.Entity<MemoriaRam>().HasData(memorias.ToArray());
//     }

//     private List<Usuario> CargarUsuarios()
//     {
//         List<Usuario> ListaUsuarios = new List<Usuario>();
//         ListaUsuarios.Add(new Usuario() { Id = Guid.NewGuid().ToString(), NombreUsuario = "juan.perez", NombreCompleto = "Juan Perez", NombreArea = "Economia", NombreDepartamento = "Finanzas" });
//         ListaUsuarios.Add(new Usuario() { Id = Guid.NewGuid().ToString(), NombreUsuario = "fernando", NombreCompleto = "Fernando Alonso", NombreArea = "UEB Sancti-Spiritus", NombreDepartamento = "Tecnico" });

//         return ListaUsuarios;
//     }

//     private List<Impresora> CargarImpresoras()
//     {
//         List<Impresora> ListaImpresora = new List<Impresora>();
//         ListaImpresora.Add(new Impresora() { Id = Guid.NewGuid().ToString(), NumInv = "8919", NumSerie = "jjdj778sk66he4535ef", Marca = "Epson", estado = Estado.activo });
//         ListaImpresora.Add(new Impresora() { Id = Guid.NewGuid().ToString(), NumInv = "2145", NumSerie = "jjdj778skld887f333f", Marca = "HP", estado = Estado.activo });

//         return ListaImpresora;
//     }

//     private List<Teclado> CargarTeclados()
//     {
//         List<Teclado> ListaTeclados = new List<Teclado>();
//         ListaTeclados.Add(new Teclado() { Id = Guid.NewGuid().ToString(), NumInv = "6312", NumSerie = "7h7g8f8fke9gtr54t6y67uyh", Marca = "DELL", TipoConexion = "usb", estado = Estado.activo });
//         ListaTeclados.Add(new Teclado() { Id = Guid.NewGuid().ToString(), NumInv = "6431", NumSerie = "67tun7588nd7y7y4t6y45rtg", Marca = "Delton", TipoConexion = "usb", estado = Estado.activo });

//         return ListaTeclados;
//     }

//     private List<Display> CargarMonitor(List<Computadora> computadoras)
//     {
//         List<Display> ListaCompleta = new List<Display>();
//         foreach (var monitor in computadoras)
//         {
//             var ListaTemp = new List<Display>
//             {
//                 new Display()
//                 {
//                 Id = Guid.NewGuid().ToString(),
//                 NumInv = "4321",
//                 NumSerie = "7h7g8f8fke956rf67uuj43ed",
//                 ComputadoraId = monitor.Id,
//                 NumInvPc = monitor.NumInv,
//                 Marca = "Samsung",
//                 estado = Estado.activo
//                 }
//             };

//             ListaCompleta.AddRange(ListaTemp);

//         }

//         return ListaCompleta;
//     }

//     private List<MotherBoard> CargarMicros(List<MicroProcesador> microProcesadorInit)
//     {
//         List<MotherBoard> ListaCompleta = new List<MotherBoard>();
//         foreach (var micro in microProcesadorInit)
//         {
//             var ListaTemp = new List<MotherBoard>{
//                new MotherBoard() {
//                NumSerieId = Guid.NewGuid().ToString(),
//                Marca = "Asus",
//                MicroProcesadorId = micro.NumSerieId,
//                estado = Estado.activo }
//             };

//             ListaCompleta.AddRange(ListaTemp);
//         }

//         return ListaCompleta;
//     }

//     private List<Computadora> CargarComputadoras()
//     {
//         List<Computadora> ListaCompleta = new List<Computadora>();
//         var ListaTemp = new List<Computadora>
//         {
//             new Computadora()
//             {
//             Id = 1,
//             NumInv = "56911",
//             NombreDepartamento = "Finanzas",
//             NombreArea = "UEB Economia",
//             NumIp = "172.19.229.11",
//             SO = "Win 7 32",
//             Nombre = "OFC-ECO-CAB",
//             Mac = "3c4r55rf4g6622",
//             estado = Estado.activo,
//             MotherBoardId = pc.NumSerieId,
//             MotherBoardMarca = pc.Marca,
//             TecladoId = tecl.Id,
//             TeclNumInv = tecl.NumInv,
//             NombreUsuarioId = user.Id,
//             UserName = user.NombreUsuario,
//             ImpresoraId = imp,
//             ImprNumInv = "7665",
//             MicroTecn = "Core I5",
//             MemoriaRamCap = "4",
//             MemoriaRamTec = "DDR4",
//             DiscoDuroCap = "500",
//             DiscoDuroTipoCon = "sata",
//             };

//         }

//         ListaCompleta.AddRange(ListaTemp);
    
        

//         return ListaCompleta;


//     }

// private List<DiscoDuro> CargarDiscos(List<MotherBoard> boards)
// {
//     List<DiscoDuro> ListaCompleta = new List<DiscoDuro>();

//     foreach (var disco in boards)
//     {
//         var ListaTemp = new List<DiscoDuro>{
//                new DiscoDuro() {
//                 NumSerieId = Guid.NewGuid().ToString(),
//                 Marca = "toshiba",
//                 TipoConexion = "sata",
//                 Capacidad = "256",
//                 MotherBoardId = disco.NumSerieId,
//                 estado = Estado.activo }

//             };

//         ListaCompleta.AddRange(ListaTemp);
//     }

//     return ListaCompleta;

// }

// private static List<MemoriaRam> CargarMemorias(List<MotherBoard> boards)
// {
//     List<MemoriaRam> ListaCompleta = new List<MemoriaRam>();
//     foreach (var memoria in boards)
//     {
//         var ListaTemp = new List<MemoriaRam>{
//                 new MemoriaRam() { KayNumSerieId = Guid.NewGuid().ToString(), Marca = "Kingston", Tecnologia = "DDR4", Capacidad = "4", estado = Estado.activo, MotherBoardId = memoria.NumSerieId },
//               };


//         ListaCompleta.AddRange(ListaTemp);


//     }


//     return ListaCompleta;
// }

// }
