﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistencia.Context;

namespace Persistencia.Migrations
{
    [DbContext(typeof(PruebaContext))]
    [Migration("20190301163617_Migracion1")]
    partial class Migracion1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Persistencia.Entidades.Tareas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion");

                    b.Property<string>("EstadoTarea");

                    b.Property<DateTime>("FechaCreacion");

                    b.Property<DateTime>("FechaVencimiento");

                    b.Property<int>("UsuarioRefId");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioRefId");

                    b.ToTable("Tareas");
                });

            modelBuilder.Entity("Persistencia.Entidades.Usuarios", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Apellidos");

                    b.Property<string>("Ciudad");

                    b.Property<string>("Contrasena");

                    b.Property<string>("Nombre");

                    b.Property<string>("Usuario");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Persistencia.Entidades.Tareas", b =>
                {
                    b.HasOne("Persistencia.Entidades.Usuarios", "Usuarios")
                        .WithMany()
                        .HasForeignKey("UsuarioRefId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
