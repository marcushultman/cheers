using Microsoft.EntityFrameworkCore.Migrations;

namespace Cheers.Domain.Migrations
{
    public partial class AddUniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from public.\"Venues\"");

            migrationBuilder.CreateIndex(
                name: "IX_Venues_Name",
                table: "Venues",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Venues_Name",
                table: "Venues");
        }
    }
}
