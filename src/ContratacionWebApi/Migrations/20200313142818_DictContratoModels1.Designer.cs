﻿// <auto-generated />
using System;
using ContratacionWebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ContratacionWebApi.Migrations
{
    [DbContext(typeof(ContratacionDbContext))]
    [Migration("20200313142818_DictContratoModels1")]
    partial class DictContratoModels1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ContratacionWebApi.Models.Contrato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AdminContratoId");

                    b.Property<int>("EntidadId");

                    b.Property<DateTime?>("FechaDeFirmado");

                    b.Property<DateTime>("FechaDeLlegada");

                    b.Property<DateTime?>("FechaDeVencimiento");

                    b.Property<decimal?>("MontoCuc");

                    b.Property<decimal?>("MontoCup");

                    b.Property<string>("Nombre");

                    b.Property<string>("Numero");

                    b.Property<string>("ObjetoDeContrato");

                    b.Property<int>("TerminoDePago");

                    b.Property<int>("Tipo");

                    b.HasKey("Id");

                    b.HasIndex("EntidadId");

                    b.ToTable("Contratos");
                });

            modelBuilder.Entity("ContratacionWebApi.Models.DictContrato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContratoId");

                    b.Property<int>("EconomicoId");

                    b.Property<int>("JuridicoId");

                    b.HasKey("Id");

                    b.HasIndex("ContratoId");

                    b.ToTable("DictId_ContratoId");
                });

            modelBuilder.Entity("ContratacionWebApi.Models.Entidad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CodigoReup")
                        .IsRequired();

                    b.Property<string>("Correo");

                    b.Property<string>("CtaBancariaCuc");

                    b.Property<string>("CtaBancariaMn");

                    b.Property<string>("Direccion")
                        .IsRequired();

                    b.Property<string>("Fax");

                    b.Property<string>("Nit")
                        .IsRequired();

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.Property<string>("NombreCtaCuc");

                    b.Property<string>("NombreCtaMn");

                    b.Property<string>("Telefono");

                    b.HasKey("Id");

                    b.ToTable("Entidades");
                });

            modelBuilder.Entity("ContratacionWebApi.Models.EspecialistaExterno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Cargo");

                    b.Property<int?>("ContratoId");

                    b.Property<int?>("DictContratoId");

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("DictContratoId");

                    b.ToTable("EspecialistasExternos");
                });

            modelBuilder.Entity("ContratacionWebApi.Models.FormaDePago", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ContratoId");

                    b.Property<string>("Nombre");

                    b.HasKey("Id");

                    b.HasIndex("ContratoId");

                    b.ToTable("FormasDePagos");
                });

            modelBuilder.Entity("ContratacionWebApi.Models.HistoricoEstadoContrato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContratoId");

                    b.Property<int>("Estado");

                    b.Property<DateTime>("Fecha");

                    b.Property<string>("Usuario");

                    b.HasKey("Id");

                    b.HasIndex("ContratoId");

                    b.ToTable("HistoricosEstadoContratos");
                });

            modelBuilder.Entity("ContratacionWebApi.Models.Contrato", b =>
                {
                    b.HasOne("ContratacionWebApi.Models.Entidad", "Entidad")
                        .WithMany()
                        .HasForeignKey("EntidadId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ContratacionWebApi.Models.DictContrato", b =>
                {
                    b.HasOne("ContratacionWebApi.Models.Contrato", "Contrato")
                        .WithMany()
                        .HasForeignKey("ContratoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ContratacionWebApi.Models.EspecialistaExterno", b =>
                {
                    b.HasOne("ContratacionWebApi.Models.DictContrato")
                        .WithMany("EspecialistaExterno")
                        .HasForeignKey("DictContratoId");
                });

            modelBuilder.Entity("ContratacionWebApi.Models.FormaDePago", b =>
                {
                    b.HasOne("ContratacionWebApi.Models.Contrato")
                        .WithMany("FormasDePago")
                        .HasForeignKey("ContratoId");
                });

            modelBuilder.Entity("ContratacionWebApi.Models.HistoricoEstadoContrato", b =>
                {
                    b.HasOne("ContratacionWebApi.Models.Contrato", "Contrato")
                        .WithMany("Estados")
                        .HasForeignKey("ContratoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
