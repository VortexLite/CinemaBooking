using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Movie.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenewOutbox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OutboxMessages_LockedUntilUtc",
                schema: "movie",
                table: "OutboxMessages");

            migrationBuilder.DropIndex(
                name: "IX_OutboxMessages_NextRetryUtc",
                schema: "movie",
                table: "OutboxMessages");

            migrationBuilder.DropIndex(
                name: "IX_OutboxMessages_OccurredOnUtc",
                schema: "movie",
                table: "OutboxMessages");

            migrationBuilder.DropIndex(
                name: "IX_OutboxMessages_ProcessedOnUtc",
                schema: "movie",
                table: "OutboxMessages");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessages_ProcessedOnUtc_NextRetryUtc_LockedUntilUtc",
                schema: "movie",
                table: "OutboxMessages",
                columns: new[] { "ProcessedOnUtc", "NextRetryUtc", "LockedUntilUtc" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OutboxMessages_ProcessedOnUtc_NextRetryUtc_LockedUntilUtc",
                schema: "movie",
                table: "OutboxMessages");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessages_LockedUntilUtc",
                schema: "movie",
                table: "OutboxMessages",
                column: "LockedUntilUtc");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessages_NextRetryUtc",
                schema: "movie",
                table: "OutboxMessages",
                column: "NextRetryUtc");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessages_OccurredOnUtc",
                schema: "movie",
                table: "OutboxMessages",
                column: "OccurredOnUtc");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessages_ProcessedOnUtc",
                schema: "movie",
                table: "OutboxMessages",
                column: "ProcessedOnUtc");
        }
    }
}
