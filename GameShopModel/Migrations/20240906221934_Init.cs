using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameShopModel.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MinimumSystemRequirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Processor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RandomAccessMemory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoCard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DirectX = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiskSpace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoundCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Network = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Additionally = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinimumSystemRequirements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecommendedSystemRequirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Processor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RandomAccessMemory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoCard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DirectX = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiskSpace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoundCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Network = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Additionally = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecommendedSystemRequirements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PresentationImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MinimumSystemRequirementId = table.Column<int>(type: "int", nullable: true),
                    RecommendedSystemRequirementId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameProducts_MinimumSystemRequirements_MinimumSystemRequirementId",
                        column: x => x.MinimumSystemRequirementId,
                        principalTable: "MinimumSystemRequirements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GameProducts_RecommendedSystemRequirements_RecommendedSystemRequirementId",
                        column: x => x.RecommendedSystemRequirementId,
                        principalTable: "RecommendedSystemRequirements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GameProductGenre",
                columns: table => new
                {
                    GameProductsId = table.Column<int>(type: "int", nullable: false),
                    GenresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameProductGenre", x => new { x.GameProductsId, x.GenresId });
                    table.ForeignKey(
                        name: "FK_GameProductGenre_GameProducts_GameProductsId",
                        column: x => x.GameProductsId,
                        principalTable: "GameProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameProductGenre_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_GameProducts_GameProductId",
                        column: x => x.GameProductId,
                        principalTable: "GameProducts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameProductGenre_GenresId",
                table: "GameProductGenre",
                column: "GenresId");

            migrationBuilder.CreateIndex(
                name: "IX_GameProducts_MinimumSystemRequirementId",
                table: "GameProducts",
                column: "MinimumSystemRequirementId");

            migrationBuilder.CreateIndex(
                name: "IX_GameProducts_RecommendedSystemRequirementId",
                table: "GameProducts",
                column: "RecommendedSystemRequirementId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_GameProductId",
                table: "Images",
                column: "GameProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameProductGenre");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "GameProducts");

            migrationBuilder.DropTable(
                name: "MinimumSystemRequirements");

            migrationBuilder.DropTable(
                name: "RecommendedSystemRequirements");
        }
    }
}
