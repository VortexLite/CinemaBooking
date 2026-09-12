using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movie.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIndexFromMovieGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MovieGenres_MovieId_GenreId",
                schema: "movie",
                table: "MovieGenres");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_MovieId_GenreId",
                schema: "movie",
                table: "MovieGenres",
                columns: new[] { "MovieId", "GenreId" },
                unique: true);
        }
    }
}
