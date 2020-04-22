using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class tablesupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminResponse");

            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "parties",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "constituencies",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "candidates",
                newName: "Modified");

            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "candidates",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "candidates",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "admins",
                newName: "Modified");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "admins",
                newName: "Created");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "parties",
                newName: "Deleted");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "constituencies",
                newName: "Deleted");

            migrationBuilder.RenameColumn(
                name: "Modified",
                table: "candidates",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "candidates",
                newName: "Deleted");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "candidates",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "Modified",
                table: "admins",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "admins",
                newName: "CreatedDate");

            migrationBuilder.CreateTable(
                name: "AdminResponse",
                columns: table => new
                {
                    AdminId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdmContactNumber = table.Column<long>(nullable: false),
                    AdmEmail = table.Column<string>(nullable: true),
                    AdmFirstName = table.Column<string>(nullable: true),
                    AdmLastName = table.Column<string>(nullable: true),
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
    }
}
