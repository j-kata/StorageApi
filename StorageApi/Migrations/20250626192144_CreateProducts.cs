using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StorageApi.Migrations
{
    /// <inheritdoc />
    public partial class CreateProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Price = table.Column<int>(type: "INTEGER", nullable: false),
                    Category = table.Column<string>(type: "TEXT", nullable: false),
                    Shelf = table.Column<string>(type: "TEXT", nullable: true),
                    Count = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Count", "Description", "Name", "Price", "Shelf" },
                values: new object[,]
                {
                    { 1, "Electronics", 15, "Ergonomic mouse with 2.4 GHz receiver.", "Wireless Mouse", 299, "E1" },
                    { 2, "Electronics", 30, "20W fast charger, compatible with most phones.", "USB-C Charger", 199, "E2" },
                    { 3, "Sports", 12, "Non-slip exercise mat made of durable material.", "Yoga Mat", 349, "S3" },
                    { 4, "Office", 50, "80 pages, lined with soft cover.", "Notebook A5", 79, "O1" },
                    { 5, "Kitchen", 40, "Ceramic cup 300 ml, dishwasher safe.", "Coffee Mug", 99, "K2" },
                    { 6, "Electronics", 20, "HDMI 2.1 supporting 4K and 8K.", "HDMI Cable 2m", 149, "E3" },
                    { 7, "Sports", 18, "Safety vest for running or cycling.", "Reflective Vest", 129, "S4" },
                    { 8, "Office", 25, "Multi-purpose scissors with ergonomic grip.", "Scissors", 59, "O2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
