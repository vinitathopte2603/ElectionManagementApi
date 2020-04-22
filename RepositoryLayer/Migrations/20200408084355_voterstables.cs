using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class voterstables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminResponse",
                columns: table => new
                {
                    AdminId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdminFirstName = table.Column<string>(nullable: true),
                    AdminLastName = table.Column<string>(nullable: true),
                    AdminContactNumber = table.Column<long>(nullable: false),
                    AdminEmail = table.Column<string>(nullable: true),
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

            migrationBuilder.CreateTable(
                name: "adminVoters",
                columns: table => new
                {
                    AdminVoterId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdminId = table.Column<int>(nullable: false),
                    VoterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adminVoters", x => x.AdminVoterId);
                });

            migrationBuilder.CreateTable(
                name: "voters",
                columns: table => new
                {
                    VoterId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UniqueVoterId = table.Column<long>(nullable: false),
                    VoterFirstName = table.Column<string>(nullable: true),
                    VoterLastName = table.Column<string>(nullable: true),
                    VoterContactNUmber = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    CandidateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voters", x => x.VoterId);
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminResponse");

            migrationBuilder.DropTable(
                name: "adminVoters");

            migrationBuilder.DropTable(
                name: "voters");
        }
    }
}
