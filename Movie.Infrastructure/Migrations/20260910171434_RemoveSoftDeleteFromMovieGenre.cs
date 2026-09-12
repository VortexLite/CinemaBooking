using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movie.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSoftDeleteFromMovieGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenres_Genres_GenreId",
                schema: "movie",
                table: "MovieGenres");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "movie",
                table: "MovieGenres");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "movie",
                table: "MovieGenres");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "movie",
                table: "MovieGenres");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "movie",
                table: "MovieGenres");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "movie",
                table: "MovieGenres");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenres_Genres_GenreId",
                schema: "movie",
                table: "MovieGenres",
                column: "GenreId",
                principalSchema: "movie",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenres_Genres_GenreId",
                schema: "movie",
                table: "MovieGenres");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "movie",
                table: "MovieGenres",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "movie",
                table: "MovieGenres",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "movie",
                table: "MovieGenres",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "movie",
                table: "MovieGenres",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "movie",
                table: "MovieGenres",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenres_Genres_GenreId",
                schema: "movie",
                table: "MovieGenres",
                column: "GenreId",
                principalSchema: "movie",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
