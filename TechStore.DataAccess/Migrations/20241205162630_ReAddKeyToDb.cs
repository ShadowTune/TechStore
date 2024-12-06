using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ReAddKeyToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "BrandId",
                keyColumnType: "int",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "BrandId",
                keyColumnType: "int",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "BrandId",
                keyColumnType: "int",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "BrandId",
                keyColumnType: "int",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "BrandId",
                keyColumnType: "int",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "BrandId",
                keyColumnType: "int",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "BrandId",
                keyColumnType: "int",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "BrandId",
                keyColumnType: "int",
                keyValue: 8);

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                table: "Products",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "ProductId");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Brand", "DiscountPrice", "DisplayOrder", "ImageLink", "Model", "RegularPrice", "Series", "Specification" },
                values: new object[,]
                {
                    { "A72N6HJ2", "ASUS", 1259.99, 25, "", "A15", 1399.99, "TUF", "abcd" },
                    { "AC2N6HJ1", "ACER", 1150.99, 20, "", "AN15", 1159.99, "NITRO", "abcd" },
                    { "DL2N6HJ1", "DELL", 4100.9899999999998, 5, "", "RM15", 4159.9899999999998, "ALIENWARE", "abcd" },
                    { "GT2N6HJ1", "MSI", 4959.9899999999998, 10, "", "GT77", 5099.9899999999998, "TITAN", "abcd" },
                    { "HP2N6HJ1", "HP", 1159.99, 20, "", "15", 1199.99, "VICTUS", "abcd" },
                    { "M62N6HJ1", "LENOVO", 2059.9899999999998, 20, "", "PRO 5i", 2099.9899999999998, "LEGION", "abcd" },
                    { "MP2N6HJ1", "LENOVO", 1059.99, 30, "", "15IAX9", 1099.99, "LOQ", "abcd" },
                    { "RP2N6HJ1", "ASUS", 2399.9899999999998, 10, "", "STRIX G15", 2599.9899999999998, "ROG", "abcd" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "A72N6HJ2");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "AC2N6HJ1");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "DL2N6HJ1");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "GT2N6HJ1");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "HP2N6HJ1");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "M62N6HJ1");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "MP2N6HJ1");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: "RP2N6HJ1");

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "ProductId",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "BrandId");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "BrandId", "Brand", "DiscountPrice", "DisplayOrder", "ImageLink", "Model", "ProductId", "RegularPrice", "Series", "Specification" },
                values: new object[,]
                {
                    { 1, "LENOVO", 1059.99, 30, "", "15IAX9", "MP2N6HJ1", 1099.99, "LOQ", "abcd" },
                    { 2, "LENOVO", 2059.9899999999998, 20, "", "PRO 5i", "M62N6HJ1", 2099.9899999999998, "LEGION", "abcd" },
                    { 3, "ASUS", 1259.99, 25, "", "A15", "A72N6HJ2", 1399.99, "TUF", "abcd" },
                    { 4, "ASUS", 2399.9899999999998, 10, "", "Strix G15", "RP2N6HJ1", 2599.9899999999998, "ROG", "abcd" },
                    { 5, "MSI", 4959.9899999999998, 10, "", "GT77", "GT2N6HJ1", 5099.9899999999998, "TITAN", "abcd" },
                    { 6, "HP", 1159.99, 20, "", "15", "HP2N6HJ1", 1199.99, "VICTUS", "abcd" },
                    { 7, "ACER", 1150.99, 20, "", "AN15", "AC2N6HJ1", 1159.99, "NITRO", "abcd" },
                    { 8, "DELL", 4100.9899999999998, 5, "", "RM15", "DL2N6HJ1", 4159.9899999999998, "ALIENWARE", "abcd" }
                });
        }
    }
}
