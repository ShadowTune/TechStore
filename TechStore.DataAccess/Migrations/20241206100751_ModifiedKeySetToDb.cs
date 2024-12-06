using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedKeySetToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Series = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Series = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegularPrice = table.Column<double>(type: "float", nullable: false),
                    DiscountPrice = table.Column<double>(type: "float", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    Specification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageLink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "Name", "Series" },
                values: new object[,]
                {
                    { 1, 12, "LENOVO", "LOQ" },
                    { 2, 5, "ASUS", "TUF" },
                    { 3, 3, "ACER", "NITRO" },
                    { 4, 4, "DELL", "ALIENWARE" },
                    { 5, 6, "MSI", "TITAN" },
                    { 6, 2, "HP", "VICTUS" },
                    { 7, 6, "ASUS", "ROG" },
                    { 8, 15, "LENOVO", "LEGION" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Brand", "DiscountPrice", "DisplayOrder", "ImageLink", "Model", "RegularPrice", "Series", "Specification" },
                values: new object[,]
                {
                    { "09b4ab76-9b49-4278-8a31-e30b2bb244e5", "LENOVO", 1059.99, 30, "", "15IAX9", 1099.99, "LOQ", "abcd" },
                    { "418f00cb-3c1b-4cca-b071-43b7458fe4c4", "LENOVO", 2059.9899999999998, 20, "", "PRO 5i", 2099.9899999999998, "LEGION", "abcd" },
                    { "4aae79c9-e38e-43dc-83a5-79fe0bac94e5", "ASUS", 1259.99, 25, "", "A15", 1399.99, "TUF", "abcd" },
                    { "6b5786c6-3eed-4fb6-9183-6f5eaae7d897", "ASUS", 2399.9899999999998, 10, "", "STRIX G15", 2599.9899999999998, "ROG", "abcd" },
                    { "75d8624d-f365-4807-ae91-78483858f8dc", "MSI", 4959.9899999999998, 10, "", "GT77", 5099.9899999999998, "TITAN", "abcd" },
                    { "a55a96e0-660e-4407-ae45-bcb3b6f8a53e", "HP", 1159.99, 20, "", "15", 1199.99, "VICTUS", "abcd" },
                    { "b694c472-76e9-4e71-9ac0-ccd28944335c", "ACER", 1150.99, 20, "", "AN515", 1159.99, "NITRO", "abcd" },
                    { "e612e509-77f0-4cd6-a784-0c75172db894", "DELL", 4100.9899999999998, 5, "", "R15", 4159.9899999999998, "ALIENWARE", "abcd" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
