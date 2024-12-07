using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class NewDataBaseAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "75d8624d-f365-4807-ae91-78483858f8dc");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "09b4ab76-9b49-4278-8a31-e30b2bb244e5",
                column: "BrandId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "418f00cb-3c1b-4cca-b071-43b7458fe4c4",
                column: "BrandId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "4aae79c9-e38e-43dc-83a5-79fe0bac94e5",
                column: "BrandId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "6b5786c6-3eed-4fb6-9183-6f5eaae7d897",
                columns: new[] { "BrandId", "DiscountPrice", "Model", "RegularPrice", "Series" },
                values: new object[] { 7, 4959.9899999999998, "GT77", 5099.9899999999998, "TITAN" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "a55a96e0-660e-4407-ae45-bcb3b6f8a53e",
                column: "BrandId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "b694c472-76e9-4e71-9ac0-ccd28944335c",
                column: "BrandId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "e612e509-77f0-4cd6-a784-0c75172db894",
                column: "BrandId",
                value: 4);

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_BrandId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_BrandId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "09b4ab76-9b49-4278-8a31-e30b2bb244e5",
                column: "Brand",
                value: "LENOVO");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "418f00cb-3c1b-4cca-b071-43b7458fe4c4",
                column: "Brand",
                value: "LENOVO");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "4aae79c9-e38e-43dc-83a5-79fe0bac94e5",
                column: "Brand",
                value: "ASUS");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "6b5786c6-3eed-4fb6-9183-6f5eaae7d897",
                columns: new[] { "Brand", "DiscountPrice", "Model", "RegularPrice", "Series" },
                values: new object[] { "ASUS", 2399.9899999999998, "STRIX G15", 2599.9899999999998, "ROG" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "a55a96e0-660e-4407-ae45-bcb3b6f8a53e",
                column: "Brand",
                value: "HP");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "b694c472-76e9-4e71-9ac0-ccd28944335c",
                column: "Brand",
                value: "ACER");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "e612e509-77f0-4cd6-a784-0c75172db894",
                column: "Brand",
                value: "DELL");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Brand", "DiscountPrice", "DisplayOrder", "ImageLink", "Model", "RegularPrice", "Series", "Specification" },
                values: new object[] { "75d8624d-f365-4807-ae91-78483858f8dc", "MSI", 4959.9899999999998, 10, "", "GT77", 5099.9899999999998, "TITAN", "abcd" });
        }
    }
}
