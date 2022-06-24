using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VolcanoFinder.API.Migrations
{
    public partial class SeedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "BLOB", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "PasswordHash", "PasswordSalt" },
                values: new object[] { 1, "username", new byte[] { 55, 202, 159, 105, 255, 18, 100, 192, 83, 184, 63, 60, 247, 64, 226, 23, 128, 176, 64, 247, 152, 5, 195, 15, 126, 173, 90, 181, 87, 246, 146, 189, 118, 6, 194, 39, 18, 187, 163, 127, 166, 76, 42, 193, 77, 154, 130, 63, 51, 171, 95, 99, 45, 208, 34, 99, 253, 172, 33, 176, 214, 197, 1, 87 }, new byte[] { 77, 235, 10, 43, 75, 212, 229, 121, 171, 41, 122, 192, 212, 39, 195, 170, 85, 110, 64, 123, 236, 186, 188, 254, 137, 71, 245, 131, 94, 62, 51, 51, 183, 144, 252, 230, 240, 248, 48, 41, 220, 134, 222, 8, 152, 188, 134, 168, 177, 147, 85, 216, 46, 174, 36, 200, 67, 73, 152, 176, 37, 252, 77, 126, 153, 152, 5, 248, 61, 125, 161, 158, 24, 161, 132, 181, 137, 216, 172, 53, 207, 55, 219, 171, 142, 125, 63, 12, 17, 246, 128, 25, 51, 13, 48, 129, 79, 181, 166, 56, 142, 237, 157, 212, 149, 248, 251, 67, 92, 81, 252, 60, 85, 127, 47, 103, 217, 117, 17, 248, 42, 33, 125, 209, 244, 124, 253, 17 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

        }
    }
}
