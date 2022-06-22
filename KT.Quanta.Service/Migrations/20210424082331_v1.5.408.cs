using Microsoft.EntityFrameworkCore.Migrations;

namespace KT.Quanta.Service.Migrations
{
    public partial class v15408 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ELI_OPEN_ACCESS_FOR_DOP_MESSAGE_TYPE",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    PassRightSign = table.Column<string>(nullable: true),
                    HandleElevatorDeviceId = table.Column<string>(nullable: true),
                    MessageType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ELI_OPEN_ACCESS_FOR_DOP_MESSAGE_TYPE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ELI_PASS_RIGHT_HANDLE_ELEVATOR_DEVICE_CALL_TYPE",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    PassRightSign = table.Column<string>(nullable: true),
                    HandleElevatorDeviceId = table.Column<string>(nullable: true),
                    CallType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ELI_PASS_RIGHT_HANDLE_ELEVATOR_DEVICE_CALL_TYPE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RCGIF_PASS_RIGHT_HANDLE_ELEVATOR_DEVICE_CALL_TYPE",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    PassRightSign = table.Column<string>(nullable: true),
                    HandleElevatorDeviceId = table.Column<string>(nullable: true),
                    CallType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RCGIF_PASS_RIGHT_HANDLE_ELEVATOR_DEVICE_CALL_TYPE", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ELI_OPEN_ACCESS_FOR_DOP_MESSAGE_TYPE");

            migrationBuilder.DropTable(
                name: "ELI_PASS_RIGHT_HANDLE_ELEVATOR_DEVICE_CALL_TYPE");

            migrationBuilder.DropTable(
                name: "RCGIF_PASS_RIGHT_HANDLE_ELEVATOR_DEVICE_CALL_TYPE");
        }
    }
}
