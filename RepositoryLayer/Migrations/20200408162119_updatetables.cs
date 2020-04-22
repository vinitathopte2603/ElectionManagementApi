using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class updatetables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminResponse");

            migrationBuilder.AlterColumn<string>(
                name: "VoterLastName",
                table: "voters",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VoterFirstName",
                table: "voters",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VoterLastName",
                table: "voters",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "VoterFirstName",
                table: "voters",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "AdminResponse",
                columns: table => new
                {
                    AdminId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdminContactNumber = table.Column<long>(nullable: false),
                    AdminEmail = table.Column<string>(nullable: true),
                    AdminFirstName = table.Column<string>(nullable: true),
                    AdminLastName = table.Column<string>(nullable: true),
                    CandidateId = table.Column<int>(nullable: true),
                    ConstituencyId = table.Column<int>(nullable: true),
                    PartyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminResponse", x => x.AdminId);
                    table.ForeignKey(
                        name: "FK_AdminResponse_candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "candidates",
                        principalColumn: "CandidateId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdminResponse_constituencies_ConstituencyId",
                        column: x => x.ConstituencyId,
                        principalTable: "constituencies",
                        principalColumn: "ConstituencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdminResponse_parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "parties",
                        principalColumn: "PartyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminResponse_CandidateId",
                table: "AdminResponse",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminResponse_ConstituencyId",
                table: "AdminResponse",
                column: "ConstituencyId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminResponse_PartyId",
                table: "AdminResponse",
                column: "PartyId");
        }
    }
}
