using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class zmianastrukturylekka : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryRequests_Users_UserId",
                table: "DeliveryRequests");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryRequests_UserId",
                table: "DeliveryRequests");

            migrationBuilder.DropColumn(
                name: "UserAuth0",
                table: "DeliveryRequests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DeliveryRequests");

            migrationBuilder.UpdateData(
                table: "CourierCompanies",
                keyColumn: "CourierCompanyId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 18, 3, 55, 37, 375, DateTimeKind.Local).AddTicks(4963));

            migrationBuilder.UpdateData(
                table: "CourierCompanies",
                keyColumn: "CourierCompanyId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 18, 3, 55, 37, 375, DateTimeKind.Local).AddTicks(5003));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserAuth0",
                table: "DeliveryRequests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "DeliveryRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CourierCompanies",
                keyColumn: "CourierCompanyId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 18, 0, 26, 18, 519, DateTimeKind.Local).AddTicks(5175));

            migrationBuilder.UpdateData(
                table: "CourierCompanies",
                keyColumn: "CourierCompanyId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 18, 0, 26, 18, 519, DateTimeKind.Local).AddTicks(5220));

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryRequests_UserId",
                table: "DeliveryRequests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryRequests_Users_UserId",
                table: "DeliveryRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
