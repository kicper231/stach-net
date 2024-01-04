using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class fluentapi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryRequests_Addresses_DestinationAddressId",
                table: "DeliveryRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryRequests_Addresses_SourceAddressId",
                table: "DeliveryRequests");

            migrationBuilder.RenameColumn(
                name: "SourceAddressId",
                table: "DeliveryRequests",
                newName: "SourceAddressAddressId");

            migrationBuilder.RenameColumn(
                name: "DestinationAddressId",
                table: "DeliveryRequests",
                newName: "DestinationAddressAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_DeliveryRequests_SourceAddressId",
                table: "DeliveryRequests",
                newName: "IX_DeliveryRequests_SourceAddressAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_DeliveryRequests_DestinationAddressId",
                table: "DeliveryRequests",
                newName: "IX_DeliveryRequests_DestinationAddressAddressId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryRequests_Addresses_DestinationAddressAddressId",
                table: "DeliveryRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryRequests_Addresses_SourceAddressAddressId",
                table: "DeliveryRequests");

            migrationBuilder.RenameColumn(
                name: "SourceAddressAddressId",
                table: "DeliveryRequests",
                newName: "SourceAddressId");

            migrationBuilder.RenameColumn(
                name: "DestinationAddressAddressId",
                table: "DeliveryRequests",
                newName: "DestinationAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_DeliveryRequests_SourceAddressAddressId",
                table: "DeliveryRequests",
                newName: "IX_DeliveryRequests_SourceAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_DeliveryRequests_DestinationAddressAddressId",
                table: "DeliveryRequests",
                newName: "IX_DeliveryRequests_DestinationAddressId");

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
    }
}
