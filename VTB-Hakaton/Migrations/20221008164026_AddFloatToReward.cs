using Microsoft.EntityFrameworkCore.Migrations;

namespace VTB_Hakaton.Migrations
{
    public partial class AddFloatToReward : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "RewardMoney",
                table: "Activities",
                type: "real",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RewardMoney",
                table: "Activities",
                type: "int",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);
        }
    }
}
