using Microsoft.EntityFrameworkCore.Migrations;

namespace KT.Quanta.Service.Migrations
{
    public partial class v15363 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DopGlobalDefaultAccessFloorMasks_DopGlobalDefaultAccessMasks~",
                table: "DopGlobalDefaultAccessFloorMasks");

            migrationBuilder.DropForeignKey(
                name: "FK_DopSpecificDefaultAccessFloorMasks_DopSpecificDefaultAccessM~",
                table: "DopSpecificDefaultAccessFloorMasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DopSpecificDefaultAccessMasks",
                table: "DopSpecificDefaultAccessMasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DopSpecificDefaultAccessFloorMasks",
                table: "DopSpecificDefaultAccessFloorMasks");

            migrationBuilder.DropIndex(
                name: "IX_DopSpecificDefaultAccessFloorMasks_AccessMaskId_Floor",
                table: "DopSpecificDefaultAccessFloorMasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DopGlobalDefaultAccessMasks",
                table: "DopGlobalDefaultAccessMasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DopGlobalDefaultAccessFloorMasks",
                table: "DopGlobalDefaultAccessFloorMasks");

            migrationBuilder.DropColumn(
                name: "Floor",
                table: "DopSpecificDefaultAccessFloorMasks");

            migrationBuilder.DropColumn(
                name: "Floor",
                table: "DopGlobalDefaultAccessFloorMasks");

            migrationBuilder.RenameTable(
                name: "DopSpecificDefaultAccessMasks",
                newName: "DOP_SPECIFIC_DEFAULT_ACCESS_MASK");

            migrationBuilder.RenameTable(
                name: "DopSpecificDefaultAccessFloorMasks",
                newName: "DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK");

            migrationBuilder.RenameTable(
                name: "DopGlobalDefaultAccessMasks",
                newName: "DOP_GLOBAL_DEFAULT_ACCESS_MASK");

            migrationBuilder.RenameTable(
                name: "DopGlobalDefaultAccessFloorMasks",
                newName: "DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK");

            migrationBuilder.RenameIndex(
                name: "IX_DopSpecificDefaultAccessMasks_ConnectedState_ElevatorGroupId~",
                table: "DOP_SPECIFIC_DEFAULT_ACCESS_MASK",
                newName: "IX_DOP_SPECIFIC_DEFAULT_ACCESS_MASK_ConnectedState_ElevatorGrou~");

            migrationBuilder.RenameIndex(
                name: "IX_DopGlobalDefaultAccessMasks_ConnectedState_ElevatorGroupId",
                table: "DOP_GLOBAL_DEFAULT_ACCESS_MASK",
                newName: "IX_DOP_GLOBAL_DEFAULT_ACCESS_MASK_ConnectedState_ElevatorGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_DopGlobalDefaultAccessFloorMasks_AccessMaskId",
                table: "DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK",
                newName: "IX_DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK_AccessMaskId");

            migrationBuilder.AddColumn<string>(
                name: "FloorId",
                table: "DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FloorId",
                table: "DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DOP_SPECIFIC_DEFAULT_ACCESS_MASK",
                table: "DOP_SPECIFIC_DEFAULT_ACCESS_MASK",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK",
                table: "DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DOP_GLOBAL_DEFAULT_ACCESS_MASK",
                table: "DOP_GLOBAL_DEFAULT_ACCESS_MASK",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK",
                table: "DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK_FloorId",
                table: "DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK_AccessMaskId_FloorId",
                table: "DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK",
                columns: new[] { "AccessMaskId", "FloorId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK_FloorId",
                table: "DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK",
                column: "FloorId");

            migrationBuilder.AddForeignKey(
                name: "FK_DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK_DOP_GLOBAL_DEFAULT_ACCE~",
                table: "DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK",
                column: "AccessMaskId",
                principalTable: "DOP_GLOBAL_DEFAULT_ACCESS_MASK",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK_FLOOR_FloorId",
                table: "DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK",
                column: "FloorId",
                principalTable: "FLOOR",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK_DOP_SPECIFIC_DEFAULT_~",
                table: "DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK",
                column: "AccessMaskId",
                principalTable: "DOP_SPECIFIC_DEFAULT_ACCESS_MASK",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK_FLOOR_FloorId",
                table: "DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK",
                column: "FloorId",
                principalTable: "FLOOR",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK_DOP_GLOBAL_DEFAULT_ACCE~",
                table: "DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK");

            migrationBuilder.DropForeignKey(
                name: "FK_DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK_FLOOR_FloorId",
                table: "DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK");

            migrationBuilder.DropForeignKey(
                name: "FK_DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK_DOP_SPECIFIC_DEFAULT_~",
                table: "DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK");

            migrationBuilder.DropForeignKey(
                name: "FK_DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK_FLOOR_FloorId",
                table: "DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DOP_SPECIFIC_DEFAULT_ACCESS_MASK",
                table: "DOP_SPECIFIC_DEFAULT_ACCESS_MASK");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK",
                table: "DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK");

            migrationBuilder.DropIndex(
                name: "IX_DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK_FloorId",
                table: "DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK");

            migrationBuilder.DropIndex(
                name: "IX_DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK_AccessMaskId_FloorId",
                table: "DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DOP_GLOBAL_DEFAULT_ACCESS_MASK",
                table: "DOP_GLOBAL_DEFAULT_ACCESS_MASK");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK",
                table: "DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK");

            migrationBuilder.DropIndex(
                name: "IX_DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK_FloorId",
                table: "DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK");

            migrationBuilder.DropColumn(
                name: "FloorId",
                table: "DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK");

            migrationBuilder.DropColumn(
                name: "FloorId",
                table: "DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK");

            migrationBuilder.RenameTable(
                name: "DOP_SPECIFIC_DEFAULT_ACCESS_MASK",
                newName: "DopSpecificDefaultAccessMasks");

            migrationBuilder.RenameTable(
                name: "DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK",
                newName: "DopSpecificDefaultAccessFloorMasks");

            migrationBuilder.RenameTable(
                name: "DOP_GLOBAL_DEFAULT_ACCESS_MASK",
                newName: "DopGlobalDefaultAccessMasks");

            migrationBuilder.RenameTable(
                name: "DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK",
                newName: "DopGlobalDefaultAccessFloorMasks");

            migrationBuilder.RenameIndex(
                name: "IX_DOP_SPECIFIC_DEFAULT_ACCESS_MASK_ConnectedState_ElevatorGrou~",
                table: "DopSpecificDefaultAccessMasks",
                newName: "IX_DopSpecificDefaultAccessMasks_ConnectedState_ElevatorGroupId~");

            migrationBuilder.RenameIndex(
                name: "IX_DOP_GLOBAL_DEFAULT_ACCESS_MASK_ConnectedState_ElevatorGroupId",
                table: "DopGlobalDefaultAccessMasks",
                newName: "IX_DopGlobalDefaultAccessMasks_ConnectedState_ElevatorGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK_AccessMaskId",
                table: "DopGlobalDefaultAccessFloorMasks",
                newName: "IX_DopGlobalDefaultAccessFloorMasks_AccessMaskId");

            migrationBuilder.AddColumn<int>(
                name: "Floor",
                table: "DopSpecificDefaultAccessFloorMasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Floor",
                table: "DopGlobalDefaultAccessFloorMasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DopSpecificDefaultAccessMasks",
                table: "DopSpecificDefaultAccessMasks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DopSpecificDefaultAccessFloorMasks",
                table: "DopSpecificDefaultAccessFloorMasks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DopGlobalDefaultAccessMasks",
                table: "DopGlobalDefaultAccessMasks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DopGlobalDefaultAccessFloorMasks",
                table: "DopGlobalDefaultAccessFloorMasks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DopSpecificDefaultAccessFloorMasks_AccessMaskId_Floor",
                table: "DopSpecificDefaultAccessFloorMasks",
                columns: new[] { "AccessMaskId", "Floor" },
                unique: true);

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
    }
}
