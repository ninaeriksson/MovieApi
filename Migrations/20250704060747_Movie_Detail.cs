using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApi.Migrations
{
    /// <inheritdoc />
    public partial class Movie_Detail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieId1",
                table: "MovieDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MovieDetails_MovieId1",
                table: "MovieDetails",
                column: "MovieId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieDetails_Movies_MovieId1",
                table: "MovieDetails",
                column: "MovieId1",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieDetails_Movies_MovieId1",
                table: "MovieDetails");

            migrationBuilder.DropIndex(
                name: "IX_MovieDetails_MovieId1",
                table: "MovieDetails");

            migrationBuilder.DropColumn(
                name: "MovieId1",
                table: "MovieDetails");
        }
    }
}
