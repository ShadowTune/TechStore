using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechStore.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddProductDbTable : Migration
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
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
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
                    Brand = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Series = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegularPrice = table.Column<double>(type: "float", nullable: false),
                    DiscountPrice = table.Column<double>(type: "float", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { 1, 12, "LENOVO" },
                    { 2, 5, "ASUS" },
                    { 3, 3, "ACER" },
                    { 4, 4, "DELL" },
                    { 5, 6, "MSI" },
                    { 6, 2, "HP" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Brand", "DiscountPrice", "DisplayOrder", "Model", "RegularPrice", "Series" },
                values: new object[,]
                {
                    { "A72N6HJ2", "Asus", 1259.99, 25, "A15", 1399.99, "TUF" },
                    { "GT2N6HJ1", "MSI", 4959.9899999999998, 10, "GT77", 5099.9899999999998, "Titan" },
                    { "HP2N6HJ1", "HP", 1159.99, 20, "15", 1199.99, "Victus" },
                    { "M62N6HJ1", "Lenovo", 2059.9899999999998, 20, "Pro 5i", 2099.9899999999998, "Legion" },
                    { "MP2N6HJ1", "Lenovo", 1059.99, 30, "15IAX9", 1099.99, "LOQ" },
                    { "RP2N6HJ1", "Asus", 2399.9899999999998, 10, "Strix G15", 2599.9899999999998, "ROG" }
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
