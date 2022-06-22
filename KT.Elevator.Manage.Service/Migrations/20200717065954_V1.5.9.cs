using Microsoft.EntityFrameworkCore.Migrations;

namespace KT.Elevator.Manage.Service.Migrations
{
    public partial class V159 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CardNumber",
                table: "Persons",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RealId",
                table: "ELEVATOR_INFO",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ElevatorGroupId",
                table: "ELEVATOR_INFO",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ELEVATOR_INFO_ElevatorGroupId",
                table: "ELEVATOR_INFO",
                column: "ElevatorGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ELEVATOR_INFO_RealId_ElevatorGroupId",
                table: "ELEVATOR_INFO",
                columns: new[] { "RealId", "ElevatorGroupId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ELEVATOR_INFO_ELEVATOR_GROUP_ElevatorGroupId",
                table: "ELEVATOR_INFO",
                column: "ElevatorGroupId",
                principalTable: "ELEVATOR_GROUP",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ELEVATOR_INFO_ELEVATOR_GROUP_ElevatorGroupId",
                table: "ELEVATOR_INFO");

            migrationBuilder.DropIndex(
                name: "IX_ELEVATOR_INFO_ElevatorGroupId",
                table: "ELEVATOR_INFO");

            migrationBuilder.DropIndex(
                name: "IX_ELEVATOR_INFO_RealId_ElevatorGroupId",
                table: "ELEVATOR_INFO");

            migrationBuilder.DropColumn(
                name: "CardNumber",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "ElevatorGroupId",
                table: "ELEVATOR_INFO");

            migrationBuilder.AlterColumn<string>(
                name: "RealId",
                table: "ELEVATOR_INFO",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
