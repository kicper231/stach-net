using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class guiditd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PublicID",
                table: "Deliveries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "CourierCompanies",
                keyColumn: "CourierCompanyId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 16, 19, 55, 55, 364, DateTimeKind.Local).AddTicks(3642));

            migrationBuilder.UpdateData(
                table: "CourierCompanies",
                keyColumn: "CourierCompanyId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 16, 19, 55, 55, 364, DateTimeKind.Local).AddTicks(3689));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicID",
                table: "Deliveries");

            migrationBuilder.UpdateData(
                table: "CourierCompanies",
                keyColumn: "CourierCompanyId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 16, 17, 52, 39, 588, DateTimeKind.Local).AddTicks(90));

            migrationBuilder.UpdateData(
                table: "CourierCompanies",
                keyColumn: "CourierCompanyId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 16, 17, 52, 39, 588, DateTimeKind.Local).AddTicks(182));
        }
    }
}
