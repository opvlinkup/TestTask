using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TestTask.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Status" },
                values: new object[,]
                {
                    { 1, "user1@gmail.com", 0 },
                    { 2, "user2@gmail.com", 0 },
                    { 3, "user3@gmail.com", 0 },
                    { 4, "user4@gmail.com", 0 },
                    { 5, "user5@gmail.com", 1 },
                    { 6, "user6@gmail.com", 1 },
                    { 7, "user7@gmail.com", 0 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Price", "ProductName", "Quantity", "UserId" },
                values: new object[,]
                {
                    { 1, 10, "Apple", 5, 1 },
                    { 2, 30, "Lemon", 2, 1 },
                    { 3, 5, "Cucumber", 10, 1 },
                    { 4, 7, "Cabbage", 2, 2 },
                    { 5, 8, "Onion", 6, 2 },
                    { 6, 9, "Carrot", 5, 2 },
                    { 7, 40, "Mango", 2, 3 },
                    { 8, 45, "Orange", 5, 4 },
                    { 9, 100, "Watermelon", 1, 4 },
                    { 10, 8, "Garlic", 12, 4 },
                    { 11, 3, "Potato", 100, 7 },
                    { 12, 9, "Carrot", 15, 7 },
                    { 13, 8, "Onion", 15, 7 },
                    { 14, 50, "Pumpkin", 1, 7 },
                    { 15, 100, "Watermelon", 12, 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
