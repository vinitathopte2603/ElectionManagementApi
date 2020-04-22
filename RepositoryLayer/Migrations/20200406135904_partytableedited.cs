using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class partytableedited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartyHead",
                table: "parties");

            migrationBuilder.DropColumn(
                name: "State",
                table: "parties");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PartyHead",
                table: "parties",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "parties",
                nullable: true);
        }
    }
}
