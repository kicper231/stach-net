using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class guidadding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "InquiryId",
                table: "Offers",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "ApiId",
                table: "Deliveries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
                value: new DateTime(2024, 1, 18, 15, 0, 20, 670, DateTimeKind.Local).AddTicks(2605));

            migrationBuilder.UpdateData(
                table: "CourierCompanies",
                keyColumn: "CourierCompanyId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 18, 15, 0, 20, 670, DateTimeKind.Local).AddTicks(2678));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApiId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "PublicID",
                table: "Deliveries");

            migrationBuilder.AlterColumn<string>(
                name: "InquiryId",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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
