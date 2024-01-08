using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class changerequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "WeekendDelivery",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "WeightUnit",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "IsCompany",
                table: "DeliveryRequests");

            migrationBuilder.RenameColumn(
                name: "VipPackage",
                table: "DeliveryRequests",
                newName: "WeekendDelivery");

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "DeliveryRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "DeliveryRequests");

            migrationBuilder.RenameColumn(
                name: "WeekendDelivery",
                table: "DeliveryRequests",
                newName: "VipPackage");

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Packages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "WeekendDelivery",
                table: "Packages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "WeightUnit",
                table: "Packages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompany",
                table: "DeliveryRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
