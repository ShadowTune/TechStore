using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedDataToCompany3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: 2,
                columns: new[] { "Branch", "Name", "State", "Support" },
                values: new object[] { "KUALALAMPUR", "ASUS", "MALAYSIS", "1-855-263-6686" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: 3,
                columns: new[] { "Branch", "Name", "State", "Support" },
                values: new object[] { "NYC", "ACER", "US", "1-855-783-6686" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "Branch", "Name", "State", "Support" },
                values: new object[] { 1, "BANGKOK", "LENOVO", "THAILAND", "1-855-253-6686" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: 2,
                columns: new[] { "Branch", "Name", "State", "Support" },
                values: new object[] { "BANGKOK", "LENOVO", "THAILAND", "1-855-253-6686" });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "CompanyId",
                keyValue: 3,
                columns: new[] { "Branch", "Name", "State", "Support" },
                values: new object[] { "KUALALAMPUR", "ASUS", "MALAYSIS", "1-855-263-6686" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "Branch", "Name", "State", "Support" },
                values: new object[] { 4, "NYC", "ACER", "US", "1-855-783-6686" });
        }
    }
}
