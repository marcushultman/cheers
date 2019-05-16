using Microsoft.EntityFrameworkCore.Migrations;

namespace Cheers.Domain.Migrations
{
    public partial class AddLongitude : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "Venues",
                newName: "Created");

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "Venues",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "Venues",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Venues");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Venues");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Venues",
                newName: "Timestamp");
        }
    }
}
