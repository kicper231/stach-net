using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class guidinquiry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeliveryRequestPublicId",
                table: "DeliveryRequests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "CourierCompanies",
                keyColumn: "CourierCompanyId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 18, 15, 44, 35, 690, DateTimeKind.Local).AddTicks(5558));

            migrationBuilder.UpdateData(
                table: "CourierCompanies",
                keyColumn: "CourierCompanyId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 18, 15, 44, 35, 690, DateTimeKind.Local).AddTicks(5644));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryRequestPublicId",
                table: "DeliveryRequests");

            migrationBuilder.UpdateData(
                table: "CourierCompanies",
                keyColumn: "CourierCompanyId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 18, 15, 0, 20, 670, DateTimeKind.Local).AddTicks(2605));

            migrationBuilder.UpdateData(
                table: "CourierCompanies",
                keyColumn: "CourierCompanyId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 18, 15, 0, 20, 670, DateTimeKind.Local).AddTicks(2678));
        }
    }
}
