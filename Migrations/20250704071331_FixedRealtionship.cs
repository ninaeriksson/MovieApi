using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieApi.Migrations
{
    /// <inheritdoc />
    public partial class FixedRealtionship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieDetails_Movies_MovieId1",
                table: "MovieDetails");

            migrationBuilder.DropTable(
                name: "MovieActor");

            migrationBuilder.DropIndex(
                name: "IX_Movies_MovieDetailsId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_MovieDetails_MovieId1",
                table: "MovieDetails");

            migrationBuilder.DropColumn(
                name: "MovieDetailsId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "MovieId1",
                table: "MovieDetails");

            migrationBuilder.CreateTable(
                name: "ActorMovie",
                columns: table => new
                {
                    ActorsId = table.Column<int>(type: "int", nullable: false),
                    MoviesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorMovie", x => new { x.ActorsId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_ActorMovie_Actors_ActorsId",
                        column: x => x.ActorsId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorMovie_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieDetails_MovieId",
                table: "MovieDetails",
                column: "MovieId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActorMovie_MoviesId",
                table: "ActorMovie",
                column: "MoviesId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieDetails_Movies_MovieId",
                table: "MovieDetails",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieDetails_Movies_MovieId",
                table: "MovieDetails");

            migrationBuilder.DropTable(
                name: "ActorMovie");

            migrationBuilder.DropIndex(
                name: "IX_MovieDetails_MovieId",
                table: "MovieDetails");

            migrationBuilder.AddColumn<int>(
                name: "MovieDetailsId",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MovieId1",
                table: "MovieDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MovieActor",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    ActorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieActor", x => new { x.MovieId, x.ActorId });
                    table.ForeignKey(
                        name: "FK_MovieActor_Actors_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieActor_Movies_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_MovieDetailsId",
                table: "Movies",
                column: "MovieDetailsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovieDetails_MovieId1",
                table: "MovieDetails",
                column: "MovieId1");

            migrationBuilder.CreateIndex(
                name: "IX_MovieActor_ActorId",
                table: "MovieActor",
                column: "ActorId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieDetails_Movies_MovieId1",
                table: "MovieDetails",
                column: "MovieId1",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
