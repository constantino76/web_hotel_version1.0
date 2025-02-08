using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebHotel_vesion1._0.Migrations
{
    /// <inheritdoc />
    public partial class CargaDatosInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UsuarioRol",
                columns: new[] { "IdRol", "IdUsuario" },
                values: new object[] { 1, "01-1234-01234" });

            migrationBuilder.InsertData(
                table: "tb_Usuarios",
                columns: new[] { "IdUsuario", "Clave", "Correo", "FechaRegistro", "NombreCompleto" },
                values: new object[] { "01-1234-1234", "123", "admind23@gmail.com", new DateTime(2025, 2, 8, 11, 37, 42, 619, DateTimeKind.Utc).AddTicks(7567), "Juan Camacho" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UsuarioRol",
                keyColumns: new[] { "IdRol", "IdUsuario" },
                keyValues: new object[] { 1, "01-1234-01234" });

            migrationBuilder.DeleteData(
                table: "tb_Usuarios",
                keyColumn: "IdUsuario",
                keyValue: "01-1234-1234");
        }
    }
}
