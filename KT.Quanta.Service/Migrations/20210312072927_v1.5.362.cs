using Microsoft.EntityFrameworkCore.Migrations;

namespace KT.Quanta.Service.Migrations
{
    public partial class v15362 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DopGlobalDefaultAccessFloorMasks_DopGlobalDefaultAccessMasks~",
                table: "DopGlobalDefaultAccessFloorMasks");

            migrationBuilder.DropForeignKey(
                name: "FK_DopSpecificDefaultAccessFloorMasks_DopSpecificDefaultAccessM~",
                table: "DopSpecificDefaultAccessFloorMasks");

            migrationBuilder.DropIndex(
                name: "IX_DopSpecificDefaultAccessFloorMasks_DopSpecificDefaultAccessM~",
                table: "DopSpecificDefaultAccessFloorMasks");

            migrationBuilder.DropIndex(
                name: "IX_DopGlobalDefaultAccessFloorMasks_DopGlobalDefaultAccessMaskE~",
                table: "DopGlobalDefaultAccessFloorMasks");

            migrationBuilder.DropColumn(
                name: "DopSpecificDefaultAccessMaskEntityId",
                table: "DopSpecificDefaultAccessFloorMasks");

            migrationBuilder.DropColumn(
                name: "AddressKey",
                table: "DopGlobalDefaultAccessMasks");

            migrationBuilder.DropColumn(
                name: "DopGlobalDefaultAccessMaskEntityId",
                table: "DopGlobalDefaultAccessFloorMasks");

            migrationBuilder.AlterColumn<string>(
                name: "ElevatorGroupId",
                table: "DopSpecificDefaultAccessMasks",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccessMaskId",
                table: "DopSpecificDefaultAccessFloorMasks",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ElevatorGroupId",
                table: "DopGlobalDefaultAccessMasks",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccessMaskId",
                table: "DopGlobalDefaultAccessFloorMasks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DopSpecificDefaultAccessMasks_ConnectedState_ElevatorGroupId~",
                table: "DopSpecificDefaultAccessMasks",
                columns: new[] { "ConnectedState", "ElevatorGroupId", "HandleElevatorDeviceId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DopSpecificDefaultAccessFloorMasks_AccessMaskId_Floor",
                table: "DopSpecificDefaultAccessFloorMasks",
                columns: new[] { "AccessMaskId", "Floor" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DopGlobalDefaultAccessMasks_ConnectedState_ElevatorGroupId",
                table: "DopGlobalDefaultAccessMasks",
                columns: new[] { "ConnectedState", "ElevatorGroupId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DopGlobalDefaultAccessFloorMasks_AccessMaskId",
                table: "DopGlobalDefaultAccessFloorMasks",
                column: "AccessMaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_DopGlobalDefaultAccessFloorMasks_DopGlobalDefaultAccessMasks~",
                table: "DopGlobalDefaultAccessFloorMasks",
                column: "AccessMaskId",
                principalTable: "DopGlobalDefaultAccessMasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DopSpecificDefaultAccessFloorMasks_DopSpecificDefaultAccessM~",
                table: "DopSpecificDefaultAccessFloorMasks",
                column: "AccessMaskId",
                principalTable: "DopSpecificDefaultAccessMasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DopGlobalDefaultAccessFloorMasks_DopGlobalDefaultAccessMasks~",
                table: "DopGlobalDefaultAccessFloorMasks");

            migrationBuilder.DropForeignKey(
                name: "FK_DopSpecificDefaultAccessFloorMasks_DopSpecificDefaultAccessM~",
                table: "DopSpecificDefaultAccessFloorMasks");

            migrationBuilder.DropIndex(
                name: "IX_DopSpecificDefaultAccessMasks_ConnectedState_ElevatorGroupId~",
                table: "DopSpecificDefaultAccessMasks");

            migrationBuilder.DropIndex(
                name: "IX_DopSpecificDefaultAccessFloorMasks_AccessMaskId_Floor",
                table: "DopSpecificDefaultAccessFloorMasks");

            migrationBuilder.DropIndex(
                name: "IX_DopGlobalDefaultAccessMasks_ConnectedState_ElevatorGroupId",
                table: "DopGlobalDefaultAccessMasks");

            migrationBuilder.DropIndex(
                name: "IX_DopGlobalDefaultAccessFloorMasks_AccessMaskId",
                table: "DopGlobalDefaultAccessFloorMasks");

            migrationBuilder.DropColumn(
                name: "AccessMaskId",
                table: "DopSpecificDefaultAccessFloorMasks");

            migrationBuilder.DropColumn(
                name: "AccessMaskId",
                table: "DopGlobalDefaultAccessFloorMasks");

            migrationBuilder.AlterColumn<string>(
                name: "ElevatorGroupId",
                table: "DopSpecificDefaultAccessMasks",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DopSpecificDefaultAccessMaskEntityId",
                table: "DopSpecificDefaultAccessFloorMasks",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ElevatorGroupId",
                table: "DopGlobalDefaultAccessMasks",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressKey",
                table: "DopGlobalDefaultAccessMasks",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DopGlobalDefaultAccessMaskEntityId",
                table: "DopGlobalDefaultAccessFloorMasks",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DopSpecificDefaultAccessFloorMasks_DopSpecificDefaultAccessM~",
                table: "DopSpecificDefaultAccessFloorMasks",
                column: "DopSpecificDefaultAccessMaskEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_DopGlobalDefaultAccessFloorMasks_DopGlobalDefaultAccessMaskE~",
                table: "DopGlobalDefaultAccessFloorMasks",
                column: "DopGlobalDefaultAccessMaskEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_DopGlobalDefaultAccessFloorMasks_DopGlobalDefaultAccessMasks~",
                table: "DopGlobalDefaultAccessFloorMasks",
                column: "DopGlobalDefaultAccessMaskEntityId",
                principalTable: "DopGlobalDefaultAccessMasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DopSpecificDefaultAccessFloorMasks_DopSpecificDefaultAccessM~",
                table: "DopSpecificDefaultAccessFloorMasks",
                column: "DopSpecificDefaultAccessMaskEntityId",
                principalTable: "DopSpecificDefaultAccessMasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
