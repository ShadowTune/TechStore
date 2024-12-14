using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedDataToCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "Branch", "Name", "State", "Support" },
                values: new object[,]
                {
                    { 1, "BANGKOK", "LENOVO", "THAILAND", "1-855-253-6686" },
                    { 2, "KUALALAMPUR", "ASUS", "MALAYSIS", "1-855-263-6686" },
                    { 3, "NYC", "ACER", "US", "1-855-783-6686" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: 3);
        }
    }
}
