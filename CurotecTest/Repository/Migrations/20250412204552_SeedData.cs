using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Author",
                columns: ["CreatedDate", "CreatedUserId", "FirstName", "LastName", "UpdatedDate", "UpdatedUserId"],
                values: new object[,]
                {
                    {new DateTime(2025, 4, 12, 20, 45, 51, 447, DateTimeKind.Utc).AddTicks(1734), 1, "Jane", "Austen", new DateTime(2025, 4, 12, 20, 45, 51, 447, DateTimeKind.Utc).AddTicks(1734), 1 },
                    {new DateTime(2025, 4, 12, 20, 45, 51, 447, DateTimeKind.Utc).AddTicks(1734), 1, "Mark", "Twain", new DateTime(2025, 4, 12, 20, 45, 51, 447, DateTimeKind.Utc).AddTicks(1734), 1 }
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: ["AuthorId", "CreatedDate", "CreatedUserId", "Title", "UpdatedDate", "UpdatedUserId", "YearPublished"],
                values: new object[,]
                {
                    {1, new DateTime(2025, 4, 12, 20, 45, 51, 447, DateTimeKind.Utc).AddTicks(1734), 1, "Pride and Prejudice", new DateTime(2025, 4, 12, 20, 45, 51, 447, DateTimeKind.Utc).AddTicks(1734), 1, 1813 },
                    {1, new DateTime(2025, 4, 12, 20, 45, 51, 447, DateTimeKind.Utc).AddTicks(1734), 1, "Emma", new DateTime(2025, 4, 12, 20, 45, 51, 447, DateTimeKind.Utc).AddTicks(1734), 1, 1815 },
                    {2, new DateTime(2025, 4, 12, 20, 45, 51, 447, DateTimeKind.Utc).AddTicks(1734), 1, "Adventures of Huckleberry Finn", new DateTime(2025, 4, 12, 20, 45, 51, 447, DateTimeKind.Utc).AddTicks(1734), 1, 1884 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
