using Microsoft.EntityFrameworkCore.Migrations;

namespace KT.Quanta.Service.Migrations
{
    public partial class v15368 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HandleElevatorDeviceId",
                table: "PASS_RIGHT_AUXILIARY");

            migrationBuilder.AddColumn<string>(
                name: "ElevatorGroupId",
                table: "PASS_RIGHT_AUXILIARY",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ElevatorGroupId",
                table: "PASS_RIGHT_AUXILIARY");

            migrationBuilder.AddColumn<string>(
                name: "HandleElevatorDeviceId",
                table: "PASS_RIGHT_AUXILIARY",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
