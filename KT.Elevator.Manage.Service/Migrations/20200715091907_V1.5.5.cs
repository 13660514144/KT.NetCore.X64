using Microsoft.EntityFrameworkCore.Migrations;

namespace KT.Elevator.Manage.Service.Migrations
{
    public partial class V155 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CARD_DEVICE_PROCESSOR_ProcessorId",
                table: "CARD_DEVICE");

            migrationBuilder.AddColumn<string>(
                name: "DeviceKey",
                table: "CARD_DEVICE",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CARD_DEVICE_PROCESSOR_ProcessorId",
                table: "CARD_DEVICE",
                column: "ProcessorId",
                principalTable: "PROCESSOR",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CARD_DEVICE_PROCESSOR_ProcessorId",
                table: "CARD_DEVICE");

            migrationBuilder.DropColumn(
                name: "DeviceKey",
                table: "CARD_DEVICE");

            migrationBuilder.AddForeignKey(
                name: "FK_CARD_DEVICE_PROCESSOR_ProcessorId",
                table: "CARD_DEVICE",
                column: "ProcessorId",
                principalTable: "PROCESSOR",
                principalColumn: "Id");
        }
    }
}
