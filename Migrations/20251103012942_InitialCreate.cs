using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcBookshelf.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Pages = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.ID);
                });

            // STORED PROCEDURE IN MIGRATION NOT NEEDED. Created separately in database.
            //// create stored procedure inside the migration
            //// create or alter so not making so many https://www.reddit.com/r/dotnet/comments/18ya86b/entity_framework_core_managing_stored_procedures/
            //migrationBuilder.Sql(
            //    @"CREATE OR ALTER PROCEDURE dbo.GetMostPages2
            //      AS
            //      BEGIN
            //          SELECT MAX(Pages) AS MostPages FROM Book
            //      END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");
        }
    }
}
