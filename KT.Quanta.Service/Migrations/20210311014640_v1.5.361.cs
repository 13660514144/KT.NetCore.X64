using Microsoft.EntityFrameworkCore.Migrations;

namespace KT.Quanta.Service.Migrations
{
    public partial class v15361 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DopGlobalDefaultAccessMasks",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    AddressKey = table.Column<string>(nullable: true),
                    ConnectedState = table.Column<int>(nullable: false),
                    ElevatorGroupId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DopGlobalDefaultAccessMasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DopSpecificDefaultAccessMasks",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    ConnectedState = table.Column<int>(nullable: false),
                    ElevatorGroupId = table.Column<string>(nullable: true),
                    HandleElevatorDeviceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DopSpecificDefaultAccessMasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DopGlobalDefaultAccessFloorMasks",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    Floor = table.Column<int>(nullable: false),
                    IsDestinationFront = table.Column<bool>(nullable: false),
                    IsDestinationRear = table.Column<bool>(nullable: false),
                    IsSourceFront = table.Column<bool>(nullable: false),
                    IsSourceRear = table.Column<bool>(nullable: false),
                    DopGlobalDefaultAccessMaskEntityId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DopGlobalDefaultAccessFloorMasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DopGlobalDefaultAccessFloorMasks_DopGlobalDefaultAccessMasks~",
                        column: x => x.DopGlobalDefaultAccessMaskEntityId,
                        principalTable: "DopGlobalDefaultAccessMasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DopSpecificDefaultAccessFloorMasks",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: true),
                    Editor = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<long>(nullable: false),
                    EditedTime = table.Column<long>(nullable: false),
                    Floor = table.Column<int>(nullable: false),
                    IsDestinationFront = table.Column<bool>(nullable: false),
                    IsDestinationRear = table.Column<bool>(nullable: false),
                    DopSpecificDefaultAccessMaskEntityId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DopSpecificDefaultAccessFloorMasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DopSpecificDefaultAccessFloorMasks_DopSpecificDefaultAccessM~",
                        column: x => x.DopSpecificDefaultAccessMaskEntityId,
                        principalTable: "DopSpecificDefaultAccessMasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DopGlobalDefaultAccessFloorMasks_DopGlobalDefaultAccessMaskE~",
                table: "DopGlobalDefaultAccessFloorMasks",
                column: "DopGlobalDefaultAccessMaskEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_DopSpecificDefaultAccessFloorMasks_DopSpecificDefaultAccessM~",
                table: "DopSpecificDefaultAccessFloorMasks",
                column: "DopSpecificDefaultAccessMaskEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DopGlobalDefaultAccessFloorMasks");

            migrationBuilder.DropTable(
                name: "DopSpecificDefaultAccessFloorMasks");

            migrationBuilder.DropTable(
                name: "DopGlobalDefaultAccessMasks");

            migrationBuilder.DropTable(
                name: "DopSpecificDefaultAccessMasks");
        }
    }
}
