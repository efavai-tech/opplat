﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RhWebApi.Data;

namespace RhWebApi.Migrations
{
    [DbContext(typeof(RhWebApiDbContext))]
    [Migration("20200125220842_null_CI")]
    partial class null_CI
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("RhWebApi.Models.ActividadContratoTrab", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActividadLaboralId");

                    b.Property<int>("ContratoTrabId");

                    b.Property<decimal>("PrecioCUC");

                    b.Property<decimal>("PrecioCUP");

                    b.HasKey("Id");

                    b.HasIndex("ActividadLaboralId");

                    b.HasIndex("ContratoTrabId");

                    b.ToTable("actividades_de_ContratoTrabs");
                });

            modelBuilder.Entity("RhWebApi.Models.ActividadLaboral", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Codigo");

                    b.HasKey("Id");

                    b.ToTable("actividades_laborales");
                });

            modelBuilder.Entity("RhWebApi.Models.Baja", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CausaDeBaja");

                    b.Property<DateTime>("Fecha");

                    b.Property<int>("TrabajadorId");

                    b.HasKey("Id");

                    b.HasIndex("TrabajadorId");

                    b.ToTable("bajas");
                });

            modelBuilder.Entity("RhWebApi.Models.Bolsa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Fecha");

                    b.Property<string>("Nombre_Referencia");

                    b.Property<int>("TrabajadorId");

                    b.HasKey("Id");

                    b.HasIndex("TrabajadorId");

                    b.ToTable("Bolsa");
                });

            modelBuilder.Entity("RhWebApi.Models.CaracteristicasTrab", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ColorDeOjos");

                    b.Property<int>("ColorDePiel");

                    b.Property<byte[]>("Foto");

                    b.Property<string>("OtrasCaracteristicas");

                    b.Property<double?>("TallaCalzado");

                    b.Property<int>("TallaDeCamisa");

                    b.Property<string>("TallaPantalon");

                    b.Property<int?>("TrabajadorId");

                    b.HasKey("Id");

                    b.HasIndex("TrabajadorId")
                        .IsUnique();

                    b.ToTable("caracteristicas_del_trabjador");
                });

            modelBuilder.Entity("RhWebApi.Models.Cargo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("GrupoEscalaId");

                    b.Property<int?>("JefeId");

                    b.Property<string>("Nombre");

                    b.Property<string>("Sigla");

                    b.HasKey("Id");

                    b.HasIndex("GrupoEscalaId");

                    b.HasIndex("JefeId");

                    b.ToTable("Cargo");
                });

            modelBuilder.Entity("RhWebApi.Models.CategoriaOcupacional", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("categorias_ocupacionales");
                });

            modelBuilder.Entity("RhWebApi.Models.ContratoTrab", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CentroDeCosto");

                    b.Property<string>("Descripcion");

                    b.Property<int>("EmpresaId");

                    b.Property<DateTime>("FechaInicio");

                    b.Property<DateTime?>("FechaTerminado");

                    b.Property<decimal>("MontoCUC");

                    b.Property<decimal>("MontoCUP");

                    b.Property<bool>("Pagado");

                    b.Property<bool>("Sobregirado");

                    b.HasKey("Id");

                    b.ToTable("ContratoTrabs");
                });

            modelBuilder.Entity("RhWebApi.Models.Entrada", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CargoId");

                    b.Property<DateTime>("Fecha");

                    b.Property<int>("TrabajadorId");

                    b.Property<int?>("UnidadOrganizativaId");

                    b.HasKey("Id");

                    b.HasIndex("CargoId");

                    b.HasIndex("TrabajadorId");

                    b.HasIndex("UnidadOrganizativaId");

                    b.ToTable("Entradas");
                });

            modelBuilder.Entity("RhWebApi.Models.Funciones", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CargoId");

                    b.Property<string>("Descripcion");

                    b.HasKey("Id");

                    b.HasIndex("CargoId");

                    b.ToTable("Funciones");
                });

            modelBuilder.Entity("RhWebApi.Models.GrupoEscala", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoriaOcupacionalId");

                    b.Property<string>("Codigo");

                    b.Property<bool>("SalarioDiferenciado");

                    b.Property<decimal>("SalarioEscala");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaOcupacionalId");

                    b.ToTable("grupos_escalas");
                });

            modelBuilder.Entity("RhWebApi.Models.HistoricoPuestoDeTrabajo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("FechaInicio");

                    b.Property<DateTime?>("FechaTerminado");

                    b.Property<int?>("PuestoDeTrabajoId");

                    b.Property<int>("TrabajadorId");

                    b.HasKey("Id");

                    b.HasIndex("PuestoDeTrabajoId");

                    b.HasIndex("TrabajadorId");

                    b.ToTable("historicos_de_puestos_de_trabajos");
                });

            modelBuilder.Entity("RhWebApi.Models.Municipio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre");

                    b.Property<int>("ProvinciaId");

                    b.HasKey("Id");

                    b.HasIndex("ProvinciaId");

                    b.ToTable("municipios");
                });

            modelBuilder.Entity("RhWebApi.Models.OtroMovimiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Desde");

                    b.Property<int>("Estado");

                    b.Property<DateTime>("Fecha");

                    b.Property<DateTime>("Hasta");

                    b.Property<string>("Nombre");

                    b.Property<int>("TrabajadorId");

                    b.HasKey("Id");

                    b.HasIndex("TrabajadorId");

                    b.ToTable("OtrosMovimientos");
                });

            modelBuilder.Entity("RhWebApi.Models.Plantilla", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CargoId");

                    b.Property<int>("PlantillaAprobada");

                    b.Property<int>("UnidadOrganizativaId");

                    b.HasKey("Id");

                    b.HasIndex("CargoId");

                    b.HasIndex("UnidadOrganizativaId");

                    b.ToTable("Plantilla");
                });

            modelBuilder.Entity("RhWebApi.Models.Provincia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre");

                    b.HasKey("Id");

                    b.ToTable("provincias");
                });

            modelBuilder.Entity("RhWebApi.Models.PuestoDeTrabajo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CargoId");

                    b.Property<string>("Descripcion");

                    b.Property<int?>("JefeId");

                    b.Property<int>("PlantillaOcupada");

                    b.Property<int>("UnidadOrganizativaId");

                    b.HasKey("Id");

                    b.HasIndex("CargoId");

                    b.HasIndex("JefeId");

                    b.HasIndex("UnidadOrganizativaId");

                    b.ToTable("puestos_de_trabajos");
                });

            modelBuilder.Entity("RhWebApi.Models.Requisitos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CargoId");

                    b.Property<string>("Descripcion");

                    b.HasKey("Id");

                    b.HasIndex("CargoId");

                    b.ToTable("Requisitos");
                });

            modelBuilder.Entity("RhWebApi.Models.TipoUnidadOrganizativa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.Property<int>("Prioridad");

                    b.HasKey("Id");

                    b.ToTable("tipos_de_unidad_organizativa");
                });

            modelBuilder.Entity("RhWebApi.Models.Trabajador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Apellidos");

                    b.Property<string>("CI");

                    b.Property<string>("Codigo");

                    b.Property<string>("Correo");

                    b.Property<string>("Direccion");

                    b.Property<int>("EstadoTrabajador");

                    b.Property<int?>("MunicipioId");

                    b.Property<int>("NivelDeEscolaridad");

                    b.Property<string>("Nombre");

                    b.Property<string>("Perfil_Ocupacional");

                    b.Property<int?>("PuestoDeTrabajoId");

                    b.Property<int?>("Sexo");

                    b.Property<string>("TelefonoFijo");

                    b.Property<string>("TelefonoMovil");

                    b.HasKey("Id");

                    b.HasIndex("MunicipioId");

                    b.HasIndex("PuestoDeTrabajoId");

                    b.ToTable("trabajadores");
                });

            modelBuilder.Entity("RhWebApi.Models.Traslado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CargoDestinoId");

                    b.Property<int?>("CargoOrigenId");

                    b.Property<DateTime>("Fecha");

                    b.Property<int>("TrabajadorId");

                    b.Property<int>("UnidadOrganizativaId");

                    b.HasKey("Id");

                    b.HasIndex("CargoDestinoId");

                    b.HasIndex("CargoOrigenId");

                    b.HasIndex("TrabajadorId");

                    b.HasIndex("UnidadOrganizativaId");

                    b.ToTable("Traslados");
                });

            modelBuilder.Entity("RhWebApi.Models.UnidadOrganizativa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Activa");

                    b.Property<string>("Codigo")
                        .IsRequired();

                    b.Property<string>("Nombre")
                        .IsRequired();

                    b.Property<int?>("PerteneceAId");

                    b.Property<int?>("TipoUnidadOrganizativaId");

                    b.HasKey("Id");

                    b.HasIndex("PerteneceAId");

                    b.HasIndex("TipoUnidadOrganizativaId");

                    b.ToTable("unidades_organizativas");
                });

            modelBuilder.Entity("RhWebApi.Models.ActividadContratoTrab", b =>
                {
                    b.HasOne("RhWebApi.Models.ActividadLaboral", "ActividadLaboral")
                        .WithMany()
                        .HasForeignKey("ActividadLaboralId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RhWebApi.Models.ContratoTrab", "ContratoTrab")
                        .WithMany("ActividadContratoTrabs")
                        .HasForeignKey("ContratoTrabId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RhWebApi.Models.Baja", b =>
                {
                    b.HasOne("RhWebApi.Models.Trabajador", "Trabajador")
                        .WithMany()
                        .HasForeignKey("TrabajadorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RhWebApi.Models.Bolsa", b =>
                {
                    b.HasOne("RhWebApi.Models.Trabajador", "Trabajador")
                        .WithMany()
                        .HasForeignKey("TrabajadorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RhWebApi.Models.CaracteristicasTrab", b =>
                {
                    b.HasOne("RhWebApi.Models.Trabajador", "Trabajador")
                        .WithOne("CaracteristicasTrab")
                        .HasForeignKey("RhWebApi.Models.CaracteristicasTrab", "TrabajadorId");
                });

            modelBuilder.Entity("RhWebApi.Models.Cargo", b =>
                {
                    b.HasOne("RhWebApi.Models.GrupoEscala", "GrupoEscala")
                        .WithMany("Cargos")
                        .HasForeignKey("GrupoEscalaId");

                    b.HasOne("RhWebApi.Models.Cargo", "Jefe")
                        .WithMany()
                        .HasForeignKey("JefeId");
                });

            modelBuilder.Entity("RhWebApi.Models.Entrada", b =>
                {
                    b.HasOne("RhWebApi.Models.Cargo", "Cargo")
                        .WithMany()
                        .HasForeignKey("CargoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RhWebApi.Models.Trabajador", "Trabajador")
                        .WithMany()
                        .HasForeignKey("TrabajadorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RhWebApi.Models.UnidadOrganizativa", "UnidadOrganizativa")
                        .WithMany()
                        .HasForeignKey("UnidadOrganizativaId");
                });

            modelBuilder.Entity("RhWebApi.Models.Funciones", b =>
                {
                    b.HasOne("RhWebApi.Models.Cargo", "Cargo")
                        .WithMany()
                        .HasForeignKey("CargoId");
                });

            modelBuilder.Entity("RhWebApi.Models.GrupoEscala", b =>
                {
                    b.HasOne("RhWebApi.Models.CategoriaOcupacional", "CategoriaOcupacional")
                        .WithMany("GruposEscala")
                        .HasForeignKey("CategoriaOcupacionalId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RhWebApi.Models.HistoricoPuestoDeTrabajo", b =>
                {
                    b.HasOne("RhWebApi.Models.PuestoDeTrabajo")
                        .WithMany("Historicos")
                        .HasForeignKey("PuestoDeTrabajoId");

                    b.HasOne("RhWebApi.Models.Trabajador", "Trabajador")
                        .WithMany()
                        .HasForeignKey("TrabajadorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RhWebApi.Models.Municipio", b =>
                {
                    b.HasOne("RhWebApi.Models.Provincia", "Provincia")
                        .WithMany("Municipios")
                        .HasForeignKey("ProvinciaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RhWebApi.Models.OtroMovimiento", b =>
                {
                    b.HasOne("RhWebApi.Models.Trabajador", "Trabajador")
                        .WithMany()
                        .HasForeignKey("TrabajadorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RhWebApi.Models.Plantilla", b =>
                {
                    b.HasOne("RhWebApi.Models.Cargo", "Cargo")
                        .WithMany()
                        .HasForeignKey("CargoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RhWebApi.Models.UnidadOrganizativa", "UnidadOrganizativa")
                        .WithMany()
                        .HasForeignKey("UnidadOrganizativaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RhWebApi.Models.PuestoDeTrabajo", b =>
                {
                    b.HasOne("RhWebApi.Models.Cargo", "Cargo")
                        .WithMany()
                        .HasForeignKey("CargoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RhWebApi.Models.PuestoDeTrabajo", "Jefe")
                        .WithMany("Subordinados")
                        .HasForeignKey("JefeId");

                    b.HasOne("RhWebApi.Models.UnidadOrganizativa", "UnidadOrganizativa")
                        .WithMany("PuestoDeTrabajo")
                        .HasForeignKey("UnidadOrganizativaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RhWebApi.Models.Requisitos", b =>
                {
                    b.HasOne("RhWebApi.Models.Cargo", "Cargo")
                        .WithMany()
                        .HasForeignKey("CargoId");
                });

            modelBuilder.Entity("RhWebApi.Models.Trabajador", b =>
                {
                    b.HasOne("RhWebApi.Models.Municipio", "Municipio")
                        .WithMany()
                        .HasForeignKey("MunicipioId");

                    b.HasOne("RhWebApi.Models.PuestoDeTrabajo", "PuestoDeTrabajo")
                        .WithMany("Trabajadores")
                        .HasForeignKey("PuestoDeTrabajoId");
                });

            modelBuilder.Entity("RhWebApi.Models.Traslado", b =>
                {
                    b.HasOne("RhWebApi.Models.Cargo", "CargoDestino")
                        .WithMany()
                        .HasForeignKey("CargoDestinoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RhWebApi.Models.Cargo", "CargoOrigen")
                        .WithMany()
                        .HasForeignKey("CargoOrigenId");

                    b.HasOne("RhWebApi.Models.Trabajador", "Trabajador")
                        .WithMany()
                        .HasForeignKey("TrabajadorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RhWebApi.Models.UnidadOrganizativa", "UnidadOrganizativa")
                        .WithMany()
                        .HasForeignKey("UnidadOrganizativaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RhWebApi.Models.UnidadOrganizativa", b =>
                {
                    b.HasOne("RhWebApi.Models.UnidadOrganizativa", "PerteneceA")
                        .WithMany("UnidadesSubordinadas")
                        .HasForeignKey("PerteneceAId");

                    b.HasOne("RhWebApi.Models.TipoUnidadOrganizativa", "TipoUnidadOrganizativa")
                        .WithMany("UnidadesOrganizativas")
                        .HasForeignKey("TipoUnidadOrganizativaId");
                });
#pragma warning restore 612, 618
        }
    }
}
