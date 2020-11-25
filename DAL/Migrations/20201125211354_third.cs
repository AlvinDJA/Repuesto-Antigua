using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2020, 11, 25, 17, 13, 53, 491, DateTimeKind.Local).AddTicks(634));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                column: "Fecha",
                value: new DateTime(2020, 11, 25, 16, 43, 17, 315, DateTimeKind.Local).AddTicks(3294));
        }
    }
}
