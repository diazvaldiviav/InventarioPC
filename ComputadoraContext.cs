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

    //cargar una board por cada memoria ram

    public ComputadoraContext(DbContextOptions<ComputadoraContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        List<DiscoDuro> DiscoDuroInit = new List<DiscoDuro>();
        DiscoDuroInit.Add(new DiscoDuro() { NumSerieId = "2813qwd8q", Marca = "toshiba", TipoConexion = "sata", Capacidad = "256gb", MotherBoardId = "7h7g8f8fke964744t6yac12", estado = Estado.activo });
        DiscoDuroInit.Add(new DiscoDuro() { NumSerieId = "58523eeee", Marca = "samsung", TipoConexion = "sata", Capacidad = "500gb", MotherBoardId = "67tun7588nd7y7y4t6yrf54", estado = Estado.activo });


        modelBuilder.Entity<DiscoDuro>(discoduro =>
        {
            discoduro.ToTable("Discos Duro");

            discoduro.HasKey(p => p.NumSerieId);

            discoduro.Property(p => p.Marca).IsRequired().HasMaxLength(150);

            discoduro.Property(p => p.TipoConexion).IsRequired();

            discoduro.Property(p => p.Capacidad);

            discoduro.HasOne(p => p.motherBoard).WithMany(p => p.Discos).HasForeignKey(p => p.MotherBoardId);

            discoduro.Property(p => p.estado);

            discoduro.HasData(DiscoDuroInit);

        });

        List<MicroProcesador> MicroProcesadorInit = new List<MicroProcesador>();
        MicroProcesadorInit.Add(new MicroProcesador() { NumSerieId = "fe2de405c38e4c90ac52", Marca = "Intel", Tecnologia = "CoreI3-9", estado = Estado.activo });
        MicroProcesadorInit.Add(new MicroProcesador() { NumSerieId = "fe2de405c38e4c9034rt", Marca = "AMD", Tecnologia = "coreI5-4", estado = Estado.activo });

        modelBuilder.Entity<MicroProcesador>(micro =>
        {
            micro.ToTable("Microprocesadores");
            micro.HasKey(p => p.NumSerieId);
            micro.Property(p => p.Marca).IsRequired();
            micro.Property(p => p.Tecnologia).IsRequired();
            micro.Property(p => p.estado).IsRequired();

            micro.HasData(MicroProcesadorInit);
        });

        List<Display> DisplayInit = new List<Display>();
        DisplayInit.Add(new Display() { NumInvId = "7843", NumSerie = "7h7g8f8fke9gtr54t6yac52", ComputadoraId = "563411", Marca = "Acer", estado = Estado.activo });
        DisplayInit.Add(new Display() { NumInvId = "4321", NumSerie = "7h7g8f8fke956rf67uuj43ed", ComputadoraId = "89064", Marca = "Samsung", estado = Estado.activo });

        modelBuilder.Entity<Display>(display =>
        {
            display.ToTable("Monitores");
            display.HasKey(p => p.NumInvId);
            display.Property(p => p.Marca).IsRequired();
            display.Property(p => p.NumSerie).IsRequired();
            display.Property(p => p.estado).IsRequired();
            display.HasOne(p => p.Computadora).WithMany(p => p.Display).HasForeignKey(p => p.ComputadoraId);

            display.HasData(DisplayInit);
        });


        List<MemoriaRam> MemoriaRamInit = new List<MemoriaRam>();

        List<MotherBoard> MotherBoardInit = new List<MotherBoard>();
        MotherBoardInit.Add(new MotherBoard() { NumSerieId = Guid.NewGuid().ToString(), Marca = "Asus", MicroProcesadorId = "fe2de405c38e4c90ac52", estado = Estado.activo });
        MotherBoardInit.Add(new MotherBoard() { NumSerieId = Guid.NewGuid().ToString(), Marca = "DELL", MicroProcesadorId = "fe2de405c38e4c9034rt", estado = Estado.activo });


        modelBuilder.Entity<MotherBoard>(board =>
        {
            board.ToTable("MotherBoards");

            board.HasKey(p => p.NumSerieId);
            board.Property(p => p.Marca).IsRequired();
            board.Property(p => p.estado).IsRequired();
            board.HasOne(p => p.Micro).WithMany(p => p.MotherBoard).HasForeignKey(p => p.MicroProcesadorId);

            board.HasData(MotherBoardInit);
        });


        List<Teclado> TecladoInit = new List<Teclado>();
        TecladoInit.Add(new Teclado() { NumInvId = "6731", NumSerie = "7h7g8f8fke9gtr54t6y67uyh", Marca = "DELL", TipoConexion = "usb", estado = Estado.activo });
        TecladoInit.Add(new Teclado() { NumInvId = "67344", NumSerie = "67tun7588nd7y7y4t6y45rtg", Marca = "Delton", TipoConexion = "usb", estado = Estado.activo });

        modelBuilder.Entity<Teclado>(teclado =>
        {
            teclado.ToTable("Teclados");
            teclado.HasKey(p => p.NumInvId);
            teclado.Property(p => p.NumSerie).IsRequired();
            teclado.Property(p => p.Marca).IsRequired();
            teclado.Property(p => p.TipoConexion).IsRequired();
            teclado.Property(p => p.estado).IsRequired();
            teclado.HasData(TecladoInit);
        });


        List<Computadora> ComputadoraInit = new List<Computadora>();
        ComputadoraInit.Add(new Computadora()
        {
            NumInvId = "563411",
            NombreDepartamento = "Finanzas",
            NombreArea = "UEB Economia",
            MotherBoardId = "7h7g8f8fke964744t6yac12",
            TecladoId = "6731",
            Nombre = "OFC-ECO-CAB",
            NombreUsuarioId = "juan.perez",
            ImpresoraId = "67854",
            Mac = "3c4r55rf4g6622",
            NumIp = "172.19.229.111",
            estado = Estado.activo
        });

        ComputadoraInit.Add(new Computadora()
        {
            NumInvId = "89064",
            NombreDepartamento = "Tecnico",
            NombreArea = "UEB Sancti Spiritus",
            MotherBoardId = "67tun7588nd7y7y4t6yrf54",
            TecladoId = "67344",
            Nombre = "SSP-Tec-IOO",
            NombreUsuarioId = "fernando",
            ImpresoraId = "78354",
            Mac = "3c4f004f4d43d3",
            NumIp = "172.19.229.111",
            estado = Estado.activo

        });


        modelBuilder.Entity<Computadora>(PC =>
        {
            PC.ToTable("Computadoras");
            PC.HasKey(p => p.NumInvId);
            PC.Property(p => p.NombreArea).IsRequired().HasMaxLength(250);
            PC.Property(p => p.Nombre).IsRequired().HasMaxLength(250);
            PC.Property(p => p.NombreDepartamento).IsRequired().HasMaxLength(250);
            PC.Property(p => p.Mac).IsRequired();
            PC.Property(p => p.NumIp).IsRequired();
            PC.Property(p => p.estado).IsRequired();
            PC.HasOne(p => p.Teclado).WithMany(p => p.Computadora).HasForeignKey(p => p.TecladoId);
            PC.HasOne(p => p.Usuario).WithMany(p => p.Computadora).HasForeignKey(p => p.NombreUsuarioId);
            PC.HasOne(p => p.Impresora).WithMany(p => p.Computadora).HasForeignKey(p => p.ImpresoraId);
            PC.HasOne(p => p.MotherBoard).WithMany(p => p.Computadora).HasForeignKey(p => p.MotherBoardId);

            PC.HasData(ComputadoraInit);
        });



        List<Usuario> UsuarioInit = new List<Usuario>();

        UsuarioInit.Add(new Usuario() { NombreUsuarioId = "juan.perez", NombreCompleto = "Juan Perez", NombreArea = "Economia", NombreDepartamento = "Finanzas" });
        UsuarioInit.Add(new Usuario() { NombreUsuarioId = "fernando", NombreCompleto = "Fernando Alonso", NombreArea = "UEB Sancti-Spiritus", NombreDepartamento = "Tecnico" });

        modelBuilder.Entity<Usuario>(usuario =>
        {
            usuario.ToTable("Usuarios");
            usuario.HasKey(p => p.NombreUsuarioId);
            usuario.Property(p => p.NombreCompleto);
            usuario.Property(p => p.NombreArea).IsRequired().HasMaxLength(250);
            usuario.Property(p => p.NombreDepartamento).IsRequired().HasMaxLength(250);

            usuario.HasData(UsuarioInit);
        });


        List<Impresora> ImpresoraInit = new List<Impresora>();

        ImpresoraInit.Add(new Impresora() { NumInvId = "67854", NumSerie = "jjdj778sk66he4535ef", Marca = "Epson", estado = Estado.activo });
        ImpresoraInit.Add(new Impresora() { NumInvId = "78354", NumSerie = "jjdj778skld887f333f", Marca = "HP", estado = Estado.activo });

        modelBuilder.Entity<Impresora>(impresora =>
        {
            impresora.ToTable("Impresoras");
            impresora.HasKey(p => p.NumInvId);
            impresora.Property(p => p.NumSerie).IsRequired();
            impresora.Property(p => p.estado).IsRequired();
            impresora.Property(p => p.Marca).IsRequired().HasMaxLength(250);

            impresora.HasData(ImpresoraInit);
        });

       var memorias = CargarMemorias(MotherBoardInit);

        modelBuilder.Entity<MemoriaRam>(memoria =>
        {
            memoria.ToTable("Memorias Ram");

            memoria.HasKey(p => p.KayNumSerieId);
            memoria.Property(p => p.Tecnologia).IsRequired();
            memoria.Property(p => p.Marca).IsRequired();
            memoria.Property(p => p.Capacidad).IsRequired();
            memoria.Property(p => p.estado).IsRequired();
            memoria.HasOne(p => p.MotherBoard).WithMany(p => p.Memorias).HasForeignKey(p => p.MotherBoardId);
            memoria.HasData(memorias);
        });



    }

    private static List<MemoriaRam> CargarMemorias(List<MotherBoard> boards)
    {
        List<MemoriaRam> ListaCompleta = new List<MemoriaRam>();
        foreach (var memoria in boards)
        {
            var ListaTemp = new List<MemoriaRam>{
                new MemoriaRam() { KayNumSerieId = Guid.NewGuid().ToString(), Marca = "Kingston", Tecnologia = "DDR4", Capacidad = "4", estado = Estado.activo, MotherBoardId = memoria.NumSerieId },
                new MemoriaRam() { KayNumSerieId = Guid.NewGuid().ToString(), Marca = "HyperX", Tecnologia = "DDR3", Capacidad = "2", estado = Estado.activo, MotherBoardId = memoria.NumSerieId }
              };


         ListaCompleta.AddRange(ListaTemp);


        }

       
        return ListaCompleta;
    }

}
