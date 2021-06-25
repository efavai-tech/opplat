﻿// <auto-generated />
using System;
using FinanzasWebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FinanzasWebApi.Migrations
{
    [DbContext(typeof(FinanzasDbContext))]
    [Migration("20200313165311_ImportExcels")]
    partial class ImportExcels
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("FinanzasWebApi.Models.CacheCuentaPeriodo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Acumulado");

                    b.Property<string>("Cuenta");

                    b.Property<DateTime>("FechaActualizado");

                    b.Property<int>("Mes");

                    b.Property<decimal>("Saldo");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("CachesCuentasEnPeriodos");
                });

            modelBuilder.Entity("FinanzasWebApi.Models.CacheEstadoFinanciero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Apertura");

                    b.Property<string>("Concepto");

                    b.Property<string>("EFE");

                    b.Property<DateTime>("FechaActualizado");

                    b.Property<string>("Grupo");

                    b.Property<int>("Mes");

                    b.Property<decimal>("PlanAnual");

                    b.Property<decimal>("Saldo");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("CachesEstadosFinancieros");
                });

            modelBuilder.Entity("FinanzasWebApi.Models.CacheSubElementoPeriodo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Acumulado");

                    b.Property<DateTime>("FechaActualizado");

                    b.Property<int>("Mes");

                    b.Property<decimal>("Saldo");

                    b.Property<int>("Year");

                    b.Property<string>("elemento");

                    b.Property<string>("subElemento");

                    b.HasKey("Id");

                    b.ToTable("CachesSubElementosPeriodos");
                });

            modelBuilder.Entity("FinanzasWebApi.Models.ConfiguracionFirmas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre");

                    b.Property<string>("Valor");

                    b.HasKey("Id");

                    b.ToTable("ConfiguracionesFirmas");
                });

            modelBuilder.Entity("FinanzasWebApi.Models.ConfiguracionPorciento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Porciento");

                    b.Property<string>("Titulo");

                    b.HasKey("Id");

                    b.ToTable("ConfiguracionesPorcientos");
                });

            modelBuilder.Entity("FinanzasWebApi.Models.ElementosDelReporteEF", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Celda");

                    b.Property<bool>("ColeccionSubElementos");

                    b.Property<string>("Descripcion");

                    b.Property<string>("ReporteEstadoFinancieroDescripcion");

                    b.Property<int>("ReporteEstadoFinancieroId");

                    b.Property<string>("Restar");

                    b.Property<string>("Sumar");

                    b.Property<string>("Tipo");

                    b.HasKey("Id");

                    b.HasIndex("ReporteEstadoFinancieroId");

                    b.ToTable("ElementosDelReporteEFs");
                });

            modelBuilder.Entity("FinanzasWebApi.Models.ReporteEstadoFinanciero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion");

                    b.HasKey("Id");

                    b.ToTable("ReporteEstadoFinancieros");
                });

            modelBuilder.Entity("FinanzasWebApi.Models.SubElementosEfReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion");

                    b.Property<string>("ElementosDelReporteEFDescripcion");

                    b.Property<int>("ElementosDelReporteEFId");

                    b.HasKey("Id");

                    b.HasIndex("ElementosDelReporteEFId");

                    b.ToTable("SubElementosEfReports");
                });

            modelBuilder.Entity("FinanzasWebApi.Models.ElementosDelReporteEF", b =>
                {
                    b.HasOne("FinanzasWebApi.Models.ReporteEstadoFinanciero", "ReporteEstadoFinanciero")
                        .WithMany("ElementosDelReporteEF")
                        .HasForeignKey("ReporteEstadoFinancieroId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FinanzasWebApi.Models.SubElementosEfReport", b =>
                {
                    b.HasOne("FinanzasWebApi.Models.ElementosDelReporteEF", "ElementosDelReporteEF")
                        .WithMany("SubElementosEfReports")
                        .HasForeignKey("ElementosDelReporteEFId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}