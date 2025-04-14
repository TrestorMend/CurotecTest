using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
        table: "User",
        columns:
        [
            "Name",
            "UserName",
            "NormalizedUserName",
            "Email",
            "NormalizedEmail",
            "EmailConfirmed",
            "PasswordHash",
            "SecurityStamp",
            "ConcurrencyStamp",
            "PhoneNumber",
            "PhoneNumberConfirmed",
            "TwoFactorEnabled",
            "LockoutEnd",
            "LockoutEnabled",
            "AccessFailedCount"
        ],
        values:
        [
            "Admin",
            "admin",
            "ADMIN",
            "curotec@admin.com",
            "CUROTEC@ADMIN.COM",
            false,
            "AQAAAAIAAYagAAAAEIKWvFSGXxo8o5Uoyvc0TZpfyYf1UimH9nkgQ0lHRd4Z+55yh1Gr5XXG1zLyrZQBqA==",
            "45J6LMIO2TKYLS66KKF6UKUPYHS5F2GW",
            "732cc7ed-1f21-437b-98da-ba46f202a78a",
            null,
            false,
            false,
            null,
            true,
            0
        ]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
