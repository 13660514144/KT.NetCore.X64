using Microsoft.EntityFrameworkCore.Migrations;

namespace KT.Elevator.Manage.Service.Migrations
{
    public partial class V156 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "PROCESSOR");

            migrationBuilder.DropColumn(
                name: "ProductType",
                table: "ELEVATOR_GROUP");

            migrationBuilder.DropColumn(
                name: "ProductType",
                table: "CARD_DEVICE");

            migrationBuilder.AddColumn<string>(
                name: "BrandModel",
                table: "PROCESSOR",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrandModel",
                table: "ELEVATOR_GROUP",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrandModel",
                table: "CARD_DEVICE",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrandModel",
                table: "PROCESSOR");

            migrationBuilder.DropColumn(
                name: "BrandModel",
                table: "ELEVATOR_GROUP");

            migrationBuilder.DropColumn(
                name: "BrandModel",
                table: "CARD_DEVICE");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "PROCESSOR",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductType",
                table: "ELEVATOR_GROUP",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductType",
                table: "CARD_DEVICE",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
