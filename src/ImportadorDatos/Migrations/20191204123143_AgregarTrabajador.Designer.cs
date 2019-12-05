﻿// <auto-generated />
using System;
using ImportadorDatos.Models.EnlaceVersat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ImportadorDatos.Migrations
{
    [DbContext(typeof(EnlaceVersatDbContext))]
    [Migration("20191204123143_AgregarTrabajador")]
    partial class AgregarTrabajador
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ImportadorDatos.Models.EnlaceVersat.Asientos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AsientoId");

                    b.Property<int>("ComprobanteId");

                    b.Property<DateTime>("Fecha");

                    b.HasKey("Id");

                    b.ToTable("Asientos");
                });

            modelBuilder.Entity("ImportadorDatos.Models.EnlaceVersat.Cuentas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CuentaId");

                    b.Property<int>("CuentaVersatId");

                    b.Property<DateTime>("Fecha");

                    b.HasKey("Id");

                    b.ToTable("Cuentas");
                });

            modelBuilder.Entity("ImportadorDatos.Models.EnlaceVersat.PeriodosContables", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Fecha");

                    b.Property<int>("PeriodoId");

                    b.Property<int>("PeriodoVersatId");

                    b.HasKey("Id");

                    b.ToTable("PeriodosContables");
                });

            modelBuilder.Entity("ImportadorDatos.Models.EnlaceVersat.Trabajador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Ci");

                    b.Property<int>("TrabajadorId");

                    b.Property<int>("TrabajadorVersatId");

                    b.HasKey("Id");

                    b.ToTable("Trabajadores");
                });
#pragma warning restore 612, 618
        }
    }
}
