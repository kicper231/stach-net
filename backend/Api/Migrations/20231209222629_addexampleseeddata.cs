using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class addexampleseeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryRequests_Users_UserId",
                table: "DeliveryRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_DeliveryRequests_DeliveryRequestId",
                table: "Offers");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Auth0Id", "CreatedAt", "Email", "FirstName", "Id", "LastName" },
                values: new object[,]
                {
                    { 1, "auth0-id-1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jan.kowalski@example.com", "Jan", 0, "Kowalski" },
                    { 2, "auth0-id-2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "anna.nowak@example.com", "Anna", 0, "Nowak" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryRequests_Users_UserId",
                table: "DeliveryRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_DeliveryRequests_DeliveryRequestId",
                table: "Offers",
                column: "DeliveryRequestId",
                principalTable: "DeliveryRequests",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryRequests_Users_UserId",
                table: "DeliveryRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_DeliveryRequests_DeliveryRequestId",
                table: "Offers");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryRequests_Users_UserId",
                table: "DeliveryRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_DeliveryRequests_DeliveryRequestId",
                table: "Offers",
                column: "DeliveryRequestId",
                principalTable: "DeliveryRequests",
                principalColumn: "RequestId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
