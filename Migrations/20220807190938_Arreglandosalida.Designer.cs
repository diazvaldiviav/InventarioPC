﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProyectoInventarioASP;

#nullable disable

namespace ProyectoInventarioASP.Migrations
{
    [DbContext(typeof(ComputadoraContext))]
    [Migration("20220807190938_Arreglandosalida")]
    partial class Arreglandosalida
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ProyectoInventarioASP.Models.Celular", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumInv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumSerie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<int>("estado")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Celulares");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.Computadora", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ImprNumInv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ImpresoraId")
                        .HasColumnType("int");

                    b.Property<string>("Mac")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MicroTecn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotherBoardId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MotherBoardMarca")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreArea")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreDepartamento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumInv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumIp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SO")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sello")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeclNumInv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TecladoId")
                        .HasColumnType("int");

                    b.Property<int>("UpsId")
                        .HasColumnType("int");

                    b.Property<string>("UpsInv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<int>("estado")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ImpresoraId");

                    b.HasIndex("MotherBoardId");

                    b.HasIndex("TecladoId");

                    b.HasIndex("UpsId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Computadoras");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.DiscoDuro", b =>
                {
                    b.Property<string>("NumSerieId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Capacidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotherBoardId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TipoConexion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("estado")
                        .HasColumnType("int");

                    b.HasKey("NumSerieId");

                    b.HasIndex("MotherBoardId");

                    b.ToTable("DiscosDuro");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.Display", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ComputadoraId")
                        .HasColumnType("int");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumInv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumInvPc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumSerie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<int>("estado")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ComputadoraId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Displays");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.Entrada", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Entrega")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Equipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaEntrega")
                        .HasColumnType("datetime2");

                    b.Property<string>("Lugar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumInvEquipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("observaciones")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Entradas");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.Impresora", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumInv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumSerie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<int>("estado")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Impresoras");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.Laptop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Mac")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreArea")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreDepartamento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumInv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumIp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SO")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<int>("estado")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Laptops");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.MemoriaRam", b =>
                {
                    b.Property<string>("KayNumSerieId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Capacidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MotherBoardId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Tecnologia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("estado")
                        .HasColumnType("int");

                    b.HasKey("KayNumSerieId");

                    b.HasIndex("MotherBoardId");

                    b.ToTable("MemoriasRam");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.MicroProcesador", b =>
                {
                    b.Property<string>("NumSerieId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tecnologia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("estado")
                        .HasColumnType("int");

                    b.HasKey("NumSerieId");

                    b.ToTable("MicroProcesadores");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.MotherBoard", b =>
                {
                    b.Property<string>("NumSerieId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MicroProcesadorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("estado")
                        .HasColumnType("int");

                    b.HasKey("NumSerieId");

                    b.HasIndex("MicroProcesadorId");

                    b.ToTable("MotherBoards");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.Salida", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("EntradaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaSalida")
                        .HasColumnType("datetime2");

                    b.Property<string>("observaciones")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("salida")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EntradaId")
                        .IsUnique();

                    b.ToTable("Salidas");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.Scanner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumInv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumSerie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<int>("estado")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Scanners");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.Teclado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumInv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumSerie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoConexion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<int>("estado")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Teclados");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.Telefono", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumInv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumSerie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<int>("estado")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Telefonos");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.Ups", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumInv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumSerie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<int>("estado")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Upss");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("permisos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Cargo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreArea")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreCompleto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreDepartamento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.Celular", b =>
                {
                    b.HasOne("ProyectoInventarioASP.Models.Usuario", null)
                        .WithMany("Celular")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.Computadora", b =>
                {
                    b.HasOne("ProyectoInventarioASP.Models.Impresora", "Impresora")
                        .WithMany("Computadora")
                        .HasForeignKey("ImpresoraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectoInventarioASP.Models.MotherBoard", "MotherBoard")
                        .WithMany("Computadora")
                        .HasForeignKey("MotherBoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectoInventarioASP.Models.Teclado", "Teclado")
                        .WithMany("Computadora")
                        .HasForeignKey("TecladoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectoInventarioASP.Models.Ups", "Ups")
                        .WithMany("Computadora")
                        .HasForeignKey("UpsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectoInventarioASP.Models.Usuario", "Usuario")
                        .WithMany("Computadora")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Impresora");

                    b.Navigation("MotherBoard");

                    b.Navigation("Teclado");

                    b.Navigation("Ups");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.DiscoDuro", b =>
                {
                    b.HasOne("ProyectoInventarioASP.Models.MotherBoard", "motherBoard")
                        .WithMany("Discos")
                        .HasForeignKey("MotherBoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("motherBoard");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.Display", b =>
                {
                    b.HasOne("ProyectoInventarioASP.Models.Computadora", "Computadora")
                        .WithMany()
                        .HasForeignKey("ComputadoraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProyectoInventarioASP.Models.Usuario", "Usuario")
                        .WithMany("Monitores")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Computadora");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.Impresora", b =>
                {
                    b.HasOne("ProyectoInventarioASP.Models.Usuario", "Usuario")
                        .WithMany("Impresora")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.Laptop", b =>
                {
                    b.HasOne("ProyectoInventarioASP.Models.Usuario", "Usuario")
                        .WithMany("Laptop")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.MemoriaRam", b =>
                {
                    b.HasOne("ProyectoInventarioASP.Models.MotherBoard", "MotherBoard")
                        .WithMany("Memorias")
                        .HasForeignKey("MotherBoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MotherBoard");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.MotherBoard", b =>
                {
                    b.HasOne("ProyectoInventarioASP.Models.MicroProcesador", "Micro")
                        .WithMany("MotherBoard")
                        .HasForeignKey("MicroProcesadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Micro");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.Salida", b =>
                {
                    b.HasOne("ProyectoInventarioASP.Models.Entrada", "entradas")
                        .WithOne("salidas")
                        .HasForeignKey("ProyectoInventarioASP.Models.Salida", "EntradaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("entradas");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.Scanner", b =>
                {
                    b.HasOne("ProyectoInventarioASP.Models.Usuario", "Usuario")
                        .WithMany("Scanner")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.Teclado", b =>
                {
                    b.HasOne("ProyectoInventarioASP.Models.Usuario", "Usuario")
                        .WithMany("Teclado")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.Telefono", b =>
                {
                    b.HasOne("ProyectoInventarioASP.Models.Usuario", "Usuario")
                        .WithMany("Telefono")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.Ups", b =>
                {
                    b.HasOne("ProyectoInventarioASP.Models.Usuario", "Usuario")
                        .WithMany("Ups")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.Entrada", b =>
                {
                    b.Navigation("salidas")
                        .IsRequired();
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.Impresora", b =>
                {
                    b.Navigation("Computadora");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.MicroProcesador", b =>
                {
                    b.Navigation("MotherBoard");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.MotherBoard", b =>
                {
                    b.Navigation("Computadora");

                    b.Navigation("Discos");

                    b.Navigation("Memorias");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.Teclado", b =>
                {
                    b.Navigation("Computadora");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.Ups", b =>
                {
                    b.Navigation("Computadora");
                });

            modelBuilder.Entity("ProyectoInventarioASP.Models.Usuario", b =>
                {
                    b.Navigation("Celular");

                    b.Navigation("Computadora");

                    b.Navigation("Impresora");

                    b.Navigation("Laptop");

                    b.Navigation("Monitores");

                    b.Navigation("Scanner");

                    b.Navigation("Teclado");

                    b.Navigation("Telefono");

                    b.Navigation("Ups");
                });
#pragma warning restore 612, 618
        }
    }
}