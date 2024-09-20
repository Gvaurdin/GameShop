using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameShopModel.Migrations
{
    /// <inheritdoc />
    public partial class RecommendedGameProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecommendedGameProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpertName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpertSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecommendedGameProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecommendedGameProducts_GameProducts_GameProductId",
                        column: x => x.GameProductId,
                        principalTable: "GameProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecommendedGameProducts_GameProductId",
                table: "RecommendedGameProducts",
                column: "GameProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecommendedGameProducts");
        }
    }
}
