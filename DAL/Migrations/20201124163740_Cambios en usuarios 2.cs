using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Cambiosenusuarios2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                columns: new[] { "Fecha", "Nombres", "Usuario" },
                values: new object[] { new DateTime(2020, 11, 24, 12, 37, 40, 175, DateTimeKind.Local).AddTicks(2881), null, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                columns: new[] { "Fecha", "Nombres", "Usuario" },
                values: new object[] { new DateTime(2020, 11, 24, 12, 35, 34, 871, DateTimeKind.Local).AddTicks(2622), "admin", null });
        }
    }
}
