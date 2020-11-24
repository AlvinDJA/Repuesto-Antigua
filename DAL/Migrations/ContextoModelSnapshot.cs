﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(Contexto))]
    partial class ContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Entidades.Compras", b =>
                {
                    b.Property<int>("CompraId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.Property<float>("Itbis")
                        .HasColumnType("REAL");

                    b.Property<int>("ProveedorId")
                        .HasColumnType("INTEGER");

                    b.Property<float>("TotalCompra")
                        .HasColumnType("REAL");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CompraId");

                    b.ToTable("Compras");
                });

            modelBuilder.Entity("Entidades.Facturas", b =>
                {
                    b.Property<int>("FacturaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClienteId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.Property<double>("Itbis")
                        .HasColumnType("REAL");

                    b.Property<double>("Total")
                        .HasColumnType("REAL");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("FacturaId");

                    b.ToTable("Facturas");
                });

            modelBuilder.Entity("Entidades.Marcas", b =>
                {
                    b.Property<int>("MarcaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombres")
                        .HasColumnType("TEXT");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("MarcaId");

                    b.ToTable("Marcas");
                });

            modelBuilder.Entity("Entidades.Productos", b =>
                {
                    b.Property<int>("ProductoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("Cantidad")
                        .HasColumnType("REAL");

                    b.Property<float>("Costo")
                        .HasColumnType("REAL");

                    b.Property<string>("Descripcion")
                        .HasColumnType("TEXT");

                    b.Property<int>("MarcaId")
                        .HasColumnType("INTEGER");

                    b.Property<float>("MargenGanancia")
                        .HasColumnType("REAL");

                    b.Property<int>("NoSerie")
                        .HasColumnType("INTEGER");

                    b.Property<float>("PorcentajeITBIS")
                        .HasColumnType("REAL");

                    b.Property<float>("Precio")
                        .HasColumnType("REAL");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProductoId");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("Entidades.Proveedores", b =>
                {
                    b.Property<int>("ProveedorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Correo")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombres")
                        .HasColumnType("TEXT");

                    b.Property<int>("RNC")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Telefono")
                        .HasColumnType("TEXT");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProveedorId");

                    b.ToTable("Proveedores");
                });

            modelBuilder.Entity("Entidades.Usuarios", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Clave")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombres")
                        .HasColumnType("TEXT");

                    b.Property<string>("Usuario")
                        .HasColumnType("TEXT");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuarios");

                    b.HasData(
                        new
                        {
                            UsuarioId = 1,
                            Clave = "admin",
                            Fecha = new DateTime(2020, 11, 24, 12, 37, 40, 175, DateTimeKind.Local).AddTicks(2881),
                            Usuario = "admin"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
