using Microsoft.EntityFrameworkCore.Migrations;

namespace Cheers.Domain.Migrations
{
    public partial class AddStuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Venues",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "Venues",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "Ratings",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "Ratings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Ratings");

            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                table: "Venues",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "Venues",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }
    }
}
