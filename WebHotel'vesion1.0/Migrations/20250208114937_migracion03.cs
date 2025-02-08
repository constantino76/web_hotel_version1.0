using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebHotel_vesion1._0.Migrations
{
    /// <inheritdoc />
    public partial class migracion03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "tb_Usuarios",
                keyColumn: "IdUsuario",
                keyValue: "01-1234-1234",
                column: "FechaRegistro",
                value: new DateTime(2025, 2, 8, 11, 49, 34, 187, DateTimeKind.Utc).AddTicks(8011));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "tb_Usuarios",
                keyColumn: "IdUsuario",
                keyValue: "01-1234-1234",
                column: "FechaRegistro",
                value: new DateTime(2025, 2, 8, 11, 37, 42, 619, DateTimeKind.Utc).AddTicks(7567));
        }
    }
}
