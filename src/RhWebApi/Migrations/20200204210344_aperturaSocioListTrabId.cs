﻿using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RhWebApi.Migrations
{
    public partial class aperturaSocioListTrabId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_trabajadores_AperturaSocio_AperturaSocioId",
                table: "trabajadores");

            migrationBuilder.DropIndex(
                name: "IX_trabajadores_AperturaSocioId",
                table: "trabajadores");

            migrationBuilder.DropColumn(
                name: "AperturaSocioId",
                table: "trabajadores");

            migrationBuilder.AddColumn<List<int>>(
                name: "ListaTrabajadoresId",
                table: "AperturaSocio",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListaTrabajadoresId",
                table: "AperturaSocio");

            migrationBuilder.AddColumn<int>(
                name: "AperturaSocioId",
                table: "trabajadores",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_trabajadores_AperturaSocioId",
                table: "trabajadores",
                column: "AperturaSocioId");

            migrationBuilder.AddForeignKey(
                name: "FK_trabajadores_AperturaSocio_AperturaSocioId",
                table: "trabajadores",
                column: "AperturaSocioId",
                principalTable: "AperturaSocio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}