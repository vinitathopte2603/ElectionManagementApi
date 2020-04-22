using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class tablesedited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AdmEmail",
                table: "admins",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_candidates_CandidatePhoneNumber",
                table: "candidates",
                column: "CandidatePhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_admins_AdmEmail",
                table: "admins",
                column: "AdmEmail",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_candidates_CandidatePhoneNumber",
                table: "candidates");

            migrationBuilder.DropIndex(
                name: "IX_admins_AdmEmail",
                table: "admins");

            migrationBuilder.AlterColumn<string>(
                name: "AdmEmail",
                table: "admins",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
