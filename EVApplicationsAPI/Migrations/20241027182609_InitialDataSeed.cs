using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EVApplicationsAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Applications",
                columns: new[] { "ApplicationId", "AddressLine1", "AddressLine2", "City", "County", "EmailAddress", "Name", "Postcode", "Vrn" },
                values: new object[] { 1, "", "Brixton", "", "Greater London", "test@tester.com", "Ben", "", "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "ApplicationId",
                keyValue: 1);
        }
    }
}
