using Microsoft.EntityFrameworkCore.Migrations;

namespace KT.Quanta.Service.Migrations
{
    public partial class v15370 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "HandleElevatorDeviceId",
                table: "HANDLE_ELEVATOR_DEVICE_AUXILIARY",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HANDLE_ELEVATOR_DEVICE_AUXILIARY_HandleElevatorDeviceId",
                table: "HANDLE_ELEVATOR_DEVICE_AUXILIARY",
                column: "HandleElevatorDeviceId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HANDLE_ELEVATOR_DEVICE_AUXILIARY_HandleElevatorDeviceId",
                table: "HANDLE_ELEVATOR_DEVICE_AUXILIARY");

            migrationBuilder.AlterColumn<string>(
                name: "HandleElevatorDeviceId",
                table: "HANDLE_ELEVATOR_DEVICE_AUXILIARY",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
