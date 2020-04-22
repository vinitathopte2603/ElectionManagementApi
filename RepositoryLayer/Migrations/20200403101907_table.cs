using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "adminConstituencies",
                columns: table => new
                {
                    AdminConstId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdminId = table.Column<int>(nullable: false),
                    ConstituencyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adminConstituencies", x => x.AdminConstId);
                });

            migrationBuilder.CreateTable(
                name: "adminParties",
                columns: table => new
                {
                    AdminPartyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdminId = table.Column<int>(nullable: false),
                    PartyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adminParties", x => x.AdminPartyId);
                });

            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdmFirstName = table.Column<string>(nullable: false),
                    AdmLastName = table.Column<string>(nullable: false),
                    AdmContactNumber = table.Column<long>(nullable: false),
                    AdmEmail = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admins", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "candidates",
                columns: table => new
                {
                    CandidateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CandidateFirstName = table.Column<string>(nullable: false),
                    CandidateLastName = table.Column<string>(nullable: false),
                    CandidatePhoneNumber = table.Column<long>(nullable: false),
                    PartyId = table.Column<int>(nullable: false),
                    ConstituencyId = table.Column<int>(nullable: false),
                    AdminId = table.Column<int>(nullable: false),
                    Votes = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_candidates", x => x.CandidateId);
                });

            migrationBuilder.CreateTable(
                name: "constituencies",
                columns: table => new
                {
                    ConstituencyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ConstituencyName = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    AdminId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_constituencies", x => x.ConstituencyId);
                });

            migrationBuilder.CreateTable(
                name: "parties",
                columns: table => new
                {
                    PartyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PartyName = table.Column<string>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false),
                    AdminId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parties", x => x.PartyId);
                });

            migrationBuilder.CreateTable(
                name: "AdminResponse",
                columns: table => new
                {
                    AdminId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdmFirstName = table.Column<string>(nullable: true),
                    AdmLastName = table.Column<string>(nullable: true),
                    AdmContactNumber = table.Column<long>(nullable: false),
                    AdmEmail = table.Column<string>(nullable: true),
                    ConstituencyId = table.Column<int>(nullable: true),
                    PartyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminResponse", x => x.AdminId);
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
                name: "adminConstituencies");

            migrationBuilder.DropTable(
                name: "adminParties");

            migrationBuilder.DropTable(
                name: "AdminResponse");

            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.DropTable(
                name: "candidates");

            migrationBuilder.DropTable(
                name: "constituencies");

            migrationBuilder.DropTable(
                name: "parties");
        }
    }
}
