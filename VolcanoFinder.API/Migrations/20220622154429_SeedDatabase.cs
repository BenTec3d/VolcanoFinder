using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VolcanoFinder.API.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Volcanoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Picture = table.Column<string>(type: "TEXT", nullable: false),
                    CountryAlpha2 = table.Column<string>(type: "TEXT", maxLength: 2, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    LastEruption = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Active = table.Column<bool>(type: "INTEGER", nullable: true),
                    RegionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volcanoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Volcanoes_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Africa" });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Asia" });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Australia" });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Europe" });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Name" },
                values: new object[] { 5, "North America" });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Name" },
                values: new object[] { 6, "South America" });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Name" },
                values: new object[] { 7, "Antarctica" });

            migrationBuilder.InsertData(
                table: "Volcanoes",
                columns: new[] { "Id", "Active", "CountryAlpha2", "Description", "LastEruption", "Name", "Picture", "RegionId" },
                values: new object[] { 1, true, "IT", "Mount Etna is an active stratovolcano on the east coast of Sicily, Italy, in the Metropolitan City of Catania, between the cities of Messina and Catania.", new DateTime(2022, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mount Etna", "https://upload.wikimedia.org/wikipedia/commons/8/8d/Mt_Etna_and_Catania.JPG", 4 });

            migrationBuilder.InsertData(
                table: "Volcanoes",
                columns: new[] { "Id", "Active", "CountryAlpha2", "Description", "LastEruption", "Name", "Picture", "RegionId" },
                values: new object[] { 2, true, "JP", "Mount Fuji is located about 100 km (62 mi) southwest of Tokyo and is visible from there on clear days. Mount Fuji's exceptionally symmetrical cone, which is covered in snow for about five months of the year, is commonly used as a cultural icon of Japan and it is frequently depicted in art and photography, as well as visited by sightseers and climbers.", new DateTime(1708, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mount Fuji", "https://upload.wikimedia.org/wikipedia/commons/1/1b/080103_hakkai_fuji.jpg", 2 });

            migrationBuilder.InsertData(
                table: "Volcanoes",
                columns: new[] { "Id", "Active", "CountryAlpha2", "Description", "LastEruption", "Name", "Picture", "RegionId" },
                values: new object[] { 3, false, "TZ", "Mount Kilimanjaro is a dormant volcano in United Republic of Tanzania. It has three volcanic cones: Kibo, Mawenzi, and Shira. It is the highest mountain in Africa and the highest single free-standing mountain above sea level in the world: 5,895 metres (19,341 ft) above sea level and about 4,900 metres (16,100 ft) above its plateau base.", null, "Mount Kilimanjaro", "https://upload.wikimedia.org/wikipedia/commons/6/6b/Mt._Kilimanjaro_12.2006.JPG", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Volcanoes_RegionId",
                table: "Volcanoes",
                column: "RegionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Volcanoes");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
