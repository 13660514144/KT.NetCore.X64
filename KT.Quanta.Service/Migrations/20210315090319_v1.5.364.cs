using Microsoft.EntityFrameworkCore.Migrations;

namespace KT.Quanta.Service.Migrations
{
    public partial class v15364 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "HandleElevatorDeviceId",
                table: "DOP_SPECIFIC_DEFAULT_ACCESS_MASK",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_DOP_SPECIFIC_DEFAULT_ACCESS_MASK_ElevatorGroupId",
                table: "DOP_SPECIFIC_DEFAULT_ACCESS_MASK",
                column: "ElevatorGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_DOP_SPECIFIC_DEFAULT_ACCESS_MASK_HandleElevatorDeviceId",
                table: "DOP_SPECIFIC_DEFAULT_ACCESS_MASK",
                column: "HandleElevatorDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DOP_GLOBAL_DEFAULT_ACCESS_MASK_ElevatorGroupId",
                table: "DOP_GLOBAL_DEFAULT_ACCESS_MASK",
                column: "ElevatorGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_DOP_GLOBAL_DEFAULT_ACCESS_MASK_ELEVATOR_GROUP_ElevatorGroupId",
                table: "DOP_GLOBAL_DEFAULT_ACCESS_MASK",
                column: "ElevatorGroupId",
                principalTable: "ELEVATOR_GROUP",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DOP_SPECIFIC_DEFAULT_ACCESS_MASK_ELEVATOR_GROUP_ElevatorGrou~",
                table: "DOP_SPECIFIC_DEFAULT_ACCESS_MASK",
                column: "ElevatorGroupId",
                principalTable: "ELEVATOR_GROUP",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DOP_SPECIFIC_DEFAULT_ACCESS_MASK_HANDLE_ELEVATOR_DEVICE_Hand~",
                table: "DOP_SPECIFIC_DEFAULT_ACCESS_MASK",
                column: "HandleElevatorDeviceId",
                principalTable: "HANDLE_ELEVATOR_DEVICE",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DOP_GLOBAL_DEFAULT_ACCESS_MASK_ELEVATOR_GROUP_ElevatorGroupId",
                table: "DOP_GLOBAL_DEFAULT_ACCESS_MASK");

            migrationBuilder.DropForeignKey(
                name: "FK_DOP_SPECIFIC_DEFAULT_ACCESS_MASK_ELEVATOR_GROUP_ElevatorGrou~",
                table: "DOP_SPECIFIC_DEFAULT_ACCESS_MASK");

            migrationBuilder.DropForeignKey(
                name: "FK_DOP_SPECIFIC_DEFAULT_ACCESS_MASK_HANDLE_ELEVATOR_DEVICE_Hand~",
                table: "DOP_SPECIFIC_DEFAULT_ACCESS_MASK");

            migrationBuilder.DropIndex(
                name: "IX_DOP_SPECIFIC_DEFAULT_ACCESS_MASK_ElevatorGroupId",
                table: "DOP_SPECIFIC_DEFAULT_ACCESS_MASK");

            migrationBuilder.DropIndex(
                name: "IX_DOP_SPECIFIC_DEFAULT_ACCESS_MASK_HandleElevatorDeviceId",
                table: "DOP_SPECIFIC_DEFAULT_ACCESS_MASK");

            migrationBuilder.DropIndex(
                name: "IX_DOP_GLOBAL_DEFAULT_ACCESS_MASK_ElevatorGroupId",
                table: "DOP_GLOBAL_DEFAULT_ACCESS_MASK");

            migrationBuilder.AlterColumn<int>(
                name: "HandleElevatorDeviceId",
                table: "DOP_SPECIFIC_DEFAULT_ACCESS_MASK",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
