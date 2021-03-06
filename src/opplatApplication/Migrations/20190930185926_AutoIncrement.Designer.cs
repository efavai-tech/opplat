﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using opplatApplication.Data;

namespace opplatApplication.Migrations
{
    [DbContext(typeof(OpplatAppDbContext))]
    [Migration("20190930185926_AutoIncrement")]
    partial class AutoIncrement
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("opplatApplication.Models.Licencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Aplicacion");

                    b.Property<byte[]>("Hash");

                    b.Property<string>("Subscriptor");

                    b.Property<DateTime>("Vencimiento");

                    b.HasKey("Id");

                    b.ToTable("Licencias");
                });
#pragma warning restore 612, 618
        }
    }
}
