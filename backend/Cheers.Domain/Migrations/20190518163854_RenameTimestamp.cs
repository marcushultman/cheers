using Microsoft.EntityFrameworkCore.Migrations;

namespace Cheers.Domain.Migrations
{
    public partial class RenameTimestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "Ratings",
                newName: "Created");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Ratings",
                newName: "Timestamp");
        }
    }
}
