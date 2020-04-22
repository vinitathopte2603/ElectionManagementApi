using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class partytableupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartyHead",
                table: "parties");

            migrationBuilder.DropColumn(
                name: "State",
                table: "parties");
        }
    }
}
