﻿// <auto-generated />
using System;
using ImportadorDatos.Models.EnlaceVersat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ImportadorDatos.Migrations
{
    [DbContext(typeof(EnlaceVersatDbContext))]
    partial class EnlaceVersatDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("ImportadorDatos.Models.EnlaceVersat.CentroDeCosto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CentroId");

                    b.Property<int>("CentroVersatId");

                    b.Property<DateTime>("Fecha");

                    b.HasKey("Id");

                    b.ToTable("CentrosDeCostos");
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

            modelBuilder.Entity("ImportadorDatos.Models.EnlaceVersat.ElementoDeGasto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ElementoId");

                    b.Property<int>("ElementoVersatId");

                    b.Property<DateTime>("Fecha");

                    b.HasKey("Id");

                    b.ToTable("ElementoDeGastos");
                });

            modelBuilder.Entity("ImportadorDatos.Models.EnlaceVersat.PartidaDeGasto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Fecha");

                    b.Property<int>("PartidaId");

                    b.Property<int>("PartidaVersatId");

                    b.HasKey("Id");

                    b.ToTable("PartidaDeGastos");
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

            modelBuilder.Entity("ImportadorDatos.Models.EnlaceVersat.RegistroDeGasto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Fecha");

                    b.Property<int>("RegistroId");

                    b.Property<int>("RegistroVersatId");

                    b.HasKey("Id");

                    b.ToTable("RegistroDeGastos");
                });

            modelBuilder.Entity("ImportadorDatos.Models.EnlaceVersat.SubElementoDeGasto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Fecha");

                    b.Property<int>("SubElementoId");

                    b.Property<int>("SubElementoVersatId");

                    b.HasKey("Id");

                    b.ToTable("SubElementoDeGastos");
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

            modelBuilder.Entity("ImportadorDatos.Models.EnlaceVersat.UnidadOrganizativa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AreaVersatId");

                    b.Property<DateTime>("Fecha");

                    b.Property<int>("UnidadOrganizativaId");

                    b.HasKey("Id");

                    b.ToTable("UnidadesOrganizativas");
                });
#pragma warning restore 612, 618
        }
    }
}
