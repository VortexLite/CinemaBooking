using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movie.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOutboxDeadLetters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OutboxDeadLetters",
                schema: "movie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OriginalMessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    OccurredOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FailedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RetryCount = table.Column<int>(type: "integer", nullable: false),
                    Error = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxDeadLetters", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OutboxDeadLetters_FailedOnUtc",
                schema: "movie",
                table: "OutboxDeadLetters",
                column: "FailedOnUtc");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxDeadLetters_OriginalMessageId",
                schema: "movie",
                table: "OutboxDeadLetters",
                column: "OriginalMessageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OutboxDeadLetters_Type",
                schema: "movie",
                table: "OutboxDeadLetters",
                column: "Type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutboxDeadLetters",
                schema: "movie");
        }
    }
}
