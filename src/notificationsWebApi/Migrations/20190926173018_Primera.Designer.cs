﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using notificationsWebApi.Data;

namespace notificationsWebApi.Migrations
{
    [DbContext(typeof(notificationsDbContext))]
    [Migration("20190926173018_Primera")]
    partial class Primera
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("notificationsWebApi.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Link");

                    b.Property<string>("Modulo");

                    b.Property<string>("Texto");

                    b.HasKey("Id");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("notificationsWebApi.Models.UserNotification", b =>
                {
                    b.Property<int>("NotificationId");

                    b.Property<string>("UsuarioId");

                    b.Property<bool>("IsRead");

                    b.HasKey("NotificationId", "UsuarioId");

                    b.ToTable("UserNotifications");
                });

            modelBuilder.Entity("notificationsWebApi.Models.UserNotification", b =>
                {
                    b.HasOne("notificationsWebApi.Models.Notification", "Notification")
                        .WithMany("UserNotification")
                        .HasForeignKey("NotificationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}