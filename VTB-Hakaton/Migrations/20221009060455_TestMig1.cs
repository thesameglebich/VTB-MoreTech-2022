using Microsoft.EntityFrameworkCore.Migrations;

namespace VTB_Hakaton.Migrations
{
    public partial class TestMig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Groups_LeadId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_ActivitySolutions_AuthorId",
                table: "ActivitySolutions");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_LeadId",
                table: "Groups",
                column: "LeadId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivitySolutions_AuthorId",
                table: "ActivitySolutions",
                column: "AuthorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Groups_LeadId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_ActivitySolutions_AuthorId",
                table: "ActivitySolutions");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_LeadId",
                table: "Groups",
                column: "LeadId",
                unique: true,
                filter: "[LeadId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ActivitySolutions_AuthorId",
                table: "ActivitySolutions",
                column: "AuthorId",
                unique: true,
                filter: "[AuthorId] IS NOT NULL");
        }
    }
}
