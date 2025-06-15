using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebHotel_vesion1._0.Migrations
{
    /// <inheritdoc />
    public partial class checkincheckout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FechaReserva",
                table: "tb_Reservas",
                newName: "FechadIngreso");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaSalida",
                table: "tb_Reservas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaSalida",
                table: "tb_Reservas");

            migrationBuilder.RenameColumn(
                name: "FechadIngreso",
                table: "tb_Reservas",
                newName: "FechaReserva");
        }
    }
}
