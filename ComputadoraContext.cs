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
    public DbSet<Celular> Celulares { get; set; }
    public DbSet<Scanner> Scanners { get; set; }
    public DbSet<Telefono> Telefonos { get; set; }
    public DbSet<Laptop> Laptops { get; set; }
    public DbSet<Entrada> Entradas { get; set; }
    public DbSet<Salida> Salidas { get; set; }
    public DbSet<Bajas> Bajas { get; set; }
    public DbSet<Equipos> Equipos { get; set; }


    public ComputadoraContext(DbContextOptions<ComputadoraContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var trabajador = CargarTrabajador();
        var teclado = CargarTeclado(trabajador);
        var ups = CargarUps(trabajador);
        var impresora = CargarImpresora(trabajador);
        var micro = CargarMicro();
        var board = CargarBoard(micro);
        var computadora = CargarComputadora(trabajador, teclado, ups, impresora, board);


        modelBuilder.Entity<Teclado>().HasData(teclado);
        modelBuilder.Entity<Usuario>().HasData(trabajador);
        modelBuilder.Entity<Ups>().HasData(ups);
        modelBuilder.Entity<Impresora>().HasData(impresora);
        modelBuilder.Entity<MicroProcesador>().HasData(micro);
        modelBuilder.Entity<MotherBoard>().HasData(board);
        modelBuilder.Entity<Computadora>().HasData(computadora);

        //var alumnos = CargarAlumnos(cursos);
        // modelBuilder.Entity<Escuela>().HasData(escuela);
    }

    private Computadora CargarComputadora(Usuario trabajador, Teclado teclado, Ups ups, Impresora impresora, MotherBoard board)
    {
        var pc = new Computadora
        {
            Id = 1,
            NumInv = "Sin Computadora",
            NombreArea = "-",
            NombreDepartamento = "-",
            Sello = "-",
            SO = "-",
            Nombre = "-",
            UpsInv = ups.NumInv,
            estado = Estado.inactivo,
            Mac = "-",
            NumIp = "-",
            ImpresoraId = impresora.Id,
            UsuarioId = trabajador.Id,
            MotherBoardId = board.NumSerieId,
            TecladoId = teclado.Id,
            UpsId = ups.Id,
            ImprNumInv = impresora.NumInv,
            TeclNumInv = teclado.NumInv,
            UserName = trabajador.NombreUsuario,
            MotherBoardMarca = board.Marca,
            MicroTecn = "-"
        };
        return pc;
    }

    private MotherBoard CargarBoard(MicroProcesador micro)
    {
        var board = new MotherBoard
        {
            NumSerieId = "Sin Board",
            Marca = "-",
            MicroProcesadorId = micro.NumSerieId,
            estado = Estado.inactivo
        };
        return board;
    }

    private MicroProcesador CargarMicro()
    {
        var micro = new MicroProcesador
        {
            NumSerieId = "Sin Micro",
            Marca = "-",
            Tecnologia = "-",
            estado = Estado.inactivo
        };
        return micro;
    }

    private Impresora CargarImpresora(Usuario trabajador)
    {
        var impresora = new Impresora
        {
            Id = 1,
            NumSerie = "-",
            NumInv = "Sin Impresora",
            Marca = "-",
            estado = Estado.inactivo,
            UsuarioId = trabajador.Id,
            UserName = trabajador.NombreUsuario
        };
        return impresora;
    }

    private Ups CargarUps(Usuario trabajador)
    {
        var ups = new Ups
        {
            Id = 1,
            NumSerie = "-",
            NumInv = "Sin Ups",
            Marca = "Marca",
            UsuarioId = trabajador.Id,
            UserName = trabajador.NombreUsuario,
            estado = Estado.inactivo

        };
        return ups;
    }

    private Usuario CargarTrabajador()
    {
        var trabajador = new Usuario
        {
            Id = 1,
            NombreCompleto = "-",
            NombreArea = "-",
            NombreDepartamento = "-",
            NombreUsuario = "Sin Trabajador",
            Cargo = "-"
        };

        return trabajador;
    }

    private Teclado CargarTeclado(Usuario trabajador)
    {
        var teclado = new Teclado
        {
            Id = 1,
            NumSerie = "Sin Teclado",
            TipoConexion = "-",
            NumInv = "-",
            Marca = "-",
            UsuarioId = trabajador.Id,
            UserName = trabajador.NombreUsuario,
            estado = Estado.inactivo

        };

        return teclado;
    }
}




