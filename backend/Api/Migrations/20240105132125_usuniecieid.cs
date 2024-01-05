using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class usuniecieid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryRequests_Addresses_DestinationAddressAddressId",
                table: "DeliveryRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryRequests_Addresses_SourceAddressAddressId",
                table: "DeliveryRequests");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryRequests_DestinationAddressAddressId",
                table: "DeliveryRequests");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "DestinationAddressAddressId",
                table: "DeliveryRequests");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CourierCompanies");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "SourceAddressAddressId",
                table: "DeliveryRequests",
                newName: "SourceAddressId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DeliveryRequests",
                newName: "DestinationAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_DeliveryRequests_SourceAddressAddressId",
                table: "DeliveryRequests",
                newName: "IX_DeliveryRequests_SourceAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryRequests_DestinationAddressId",
                table: "DeliveryRequests",
                column: "DestinationAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryRequests_Addresses_DestinationAddressId",
                table: "DeliveryRequests",
                column: "DestinationAddressId",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryRequests_Addresses_SourceAddressId",
                table: "DeliveryRequests",
                column: "SourceAddressId",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryRequests_Addresses_DestinationAddressId",
                table: "DeliveryRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryRequests_Addresses_SourceAddressId",
                table: "DeliveryRequests");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryRequests_DestinationAddressId",
                table: "DeliveryRequests");

            migrationBuilder.RenameColumn(
                name: "SourceAddressId",
                table: "DeliveryRequests",
                newName: "SourceAddressAddressId");

            migrationBuilder.RenameColumn(
                name: "DestinationAddressId",
                table: "DeliveryRequests",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_DeliveryRequests_SourceAddressId",
                table: "DeliveryRequests",
                newName: "IX_DeliveryRequests_SourceAddressAddressId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Packages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Offers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DestinationAddressAddressId",
                table: "DeliveryRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Deliveries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CourierCompanies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Auth0Id", "CreatedAt", "Email", "FirstName", "Id", "LastName" },
                values: new object[,]
                {
                    { 1, "auth0-id-1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jan.kowalski@example.com", "Jan", 0, "Kowalski" },
                    { 2, "auth0-id-2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "anna.nowak@example.com", "Anna", 0, "Nowak" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryRequests_DestinationAddressAddressId",
                table: "DeliveryRequests",
                column: "DestinationAddressAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryRequests_Addresses_DestinationAddressAddressId",
                table: "DeliveryRequests",
                column: "DestinationAddressAddressId",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryRequests_Addresses_SourceAddressAddressId",
                table: "DeliveryRequests",
                column: "SourceAddressAddressId",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
