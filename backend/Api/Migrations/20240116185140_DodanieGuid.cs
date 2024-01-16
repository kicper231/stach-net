using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class DodanieGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Users_CourierId",
                table: "Deliveries");

            migrationBuilder.AddColumn<Guid>(
                name: "OfferGuid",
                table: "Offers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DeliveryRequestGuid",
                table: "DeliveryRequests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<int>(
                name: "CourierId",
                table: "Deliveries",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "DeliveryGuid",
                table: "Deliveries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "CourierCompanies",
                keyColumn: "CourierCompanyId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 16, 19, 51, 40, 36, DateTimeKind.Local).AddTicks(9115));

            migrationBuilder.UpdateData(
                table: "CourierCompanies",
                keyColumn: "CourierCompanyId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 16, 19, 51, 40, 36, DateTimeKind.Local).AddTicks(9190));

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Users_CourierId",
                table: "Deliveries",
                column: "CourierId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_Users_CourierId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "OfferGuid",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "DeliveryRequestGuid",
                table: "DeliveryRequests");

            migrationBuilder.DropColumn(
                name: "DeliveryGuid",
                table: "Deliveries");

            migrationBuilder.AlterColumn<int>(
                name: "CourierId",
                table: "Deliveries",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "CourierCompanies",
                keyColumn: "CourierCompanyId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "CourierCompanies",
                keyColumn: "CourierCompanyId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_Users_CourierId",
                table: "Deliveries",
                column: "CourierId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
