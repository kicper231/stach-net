using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.Migrations
{
    /// <inheritdoc/>
    public partial class offers : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Offers",
                newName: "totalPrice");

            migrationBuilder.InsertData(
                table: "CourierCompanies",
                columns: new[] { "CourierCompanyId", "ContactInfo", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { 1, "https://courierapistachnet.azurewebsites.net/api", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "StachnetCompany" },
                    { 2, "https://mini.currier.api.snet.com.pl", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SzymonCompany" }
                });
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourierCompanies",
                keyColumn: "CourierCompanyId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CourierCompanies",
                keyColumn: "CourierCompanyId",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "totalPrice",
                table: "Offers",
                newName: "Price");
        }
    }
}