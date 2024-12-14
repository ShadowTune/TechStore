using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemovedLegion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "418f00cb-3c1b-4cca-b071-43b7458fe4c4");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "Name", "Series" },
                values: new object[] { 8, 15, "LENOVO", "LEGION" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "BrandId", "DiscountPrice", "DisplayOrder", "ImageLink", "Model", "RegularPrice", "Series", "Specification" },
                values: new object[] { "418f00cb-3c1b-4cca-b071-43b7458fe4c4", 8, 2059.9899999999998, 20, "", "PRO 5i", 2099.9899999999998, "LEGION", "abcd" });
        }
    }
}
