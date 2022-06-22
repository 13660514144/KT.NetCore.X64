using Microsoft.EntityFrameworkCore.Migrations;

namespace KT.Quanta.Service.Migrations
{
    public partial class v15372 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Direction",
                table: "PASS_RIGHT_RELATION_FLOOR");

            migrationBuilder.DropColumn(
                name: "Direction",
                table: "PASS_RIGHT_AUXILIARY_DETAIL");

            migrationBuilder.DropColumn(
                name: "Direction",
                table: "PASS_RIGHT");

            migrationBuilder.DropColumn(
                name: "Direction",
                table: "HANDLE_ELEVATOR_DEVICE_AUXILIARY");

            migrationBuilder.DropColumn(
                name: "Direction",
                table: "FLOOR");

            migrationBuilder.AddColumn<bool>(
                name: "IsFront",
                table: "PASS_RIGHT_RELATION_FLOOR",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRear",
                table: "PASS_RIGHT_RELATION_FLOOR",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFront",
                table: "PASS_RIGHT_AUXILIARY_DETAIL",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRear",
                table: "PASS_RIGHT_AUXILIARY_DETAIL",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFront",
                table: "PASS_RIGHT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRear",
                table: "PASS_RIGHT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFront",
                table: "HANDLE_ELEVATOR_DEVICE_AUXILIARY",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRear",
                table: "HANDLE_ELEVATOR_DEVICE_AUXILIARY",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFront",
                table: "FLOOR",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRear",
                table: "FLOOR",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFront",
                table: "PASS_RIGHT_RELATION_FLOOR");

            migrationBuilder.DropColumn(
                name: "IsRear",
                table: "PASS_RIGHT_RELATION_FLOOR");

            migrationBuilder.DropColumn(
                name: "IsFront",
                table: "PASS_RIGHT_AUXILIARY_DETAIL");

            migrationBuilder.DropColumn(
                name: "IsRear",
                table: "PASS_RIGHT_AUXILIARY_DETAIL");

            migrationBuilder.DropColumn(
                name: "IsFront",
                table: "PASS_RIGHT");

            migrationBuilder.DropColumn(
                name: "IsRear",
                table: "PASS_RIGHT");

            migrationBuilder.DropColumn(
                name: "IsFront",
                table: "HANDLE_ELEVATOR_DEVICE_AUXILIARY");

            migrationBuilder.DropColumn(
                name: "IsRear",
                table: "HANDLE_ELEVATOR_DEVICE_AUXILIARY");

            migrationBuilder.DropColumn(
                name: "IsFront",
                table: "FLOOR");

            migrationBuilder.DropColumn(
                name: "IsRear",
                table: "FLOOR");

            migrationBuilder.AddColumn<string>(
                name: "Direction",
                table: "PASS_RIGHT_RELATION_FLOOR",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Direction",
                table: "PASS_RIGHT_AUXILIARY_DETAIL",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Direction",
                table: "PASS_RIGHT",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Direction",
                table: "HANDLE_ELEVATOR_DEVICE_AUXILIARY",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Direction",
                table: "FLOOR",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
