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



}




