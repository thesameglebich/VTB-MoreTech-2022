using Microsoft.EntityFrameworkCore.Migrations;

namespace VTB_Hakaton.Migrations
{
    public partial class AddNftId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NftId",
                table: "ShopItems",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NftId",
                table: "ShopItems");
        }
    }
}
