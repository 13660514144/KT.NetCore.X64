CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

CREATE TABLE "CARD_DEVICE" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_CARD_DEVICE" PRIMARY KEY,
    "Creator" TEXT NULL,
    "Editor" TEXT NULL,
    "CreatedTime" INTEGER NOT NULL,
    "EditedTime" INTEGER NOT NULL,
    "BrandModel" TEXT NULL,
    "DeviceType" TEXT NULL,
    "PortName" TEXT NULL,
    "SerialConfigId" TEXT NULL,
    "Baudrate" INTEGER NOT NULL,
    "Databits" INTEGER NOT NULL,
    "Stopbits" INTEGER NOT NULL,
    "Parity" INTEGER NOT NULL,
    "ReadTimeout" INTEGER NOT NULL,
    "Encoding" TEXT NULL,
    "HandleDeviceId" TEXT NULL
);

CREATE TABLE "HANDLE_ELEVATOR_DEVICE" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_HANDLE_ELEVATOR_DEVICE" PRIMARY KEY,
    "Creator" TEXT NULL,
    "Editor" TEXT NULL,
    "CreatedTime" INTEGER NOT NULL,
    "EditedTime" INTEGER NOT NULL,
    "ElevatorGroupId" TEXT NULL
);

CREATE TABLE "PASS_RECORD" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_PASS_RECORD" PRIMARY KEY,
    "Creator" TEXT NULL,
    "Editor" TEXT NULL,
    "CreatedTime" INTEGER NOT NULL,
    "EditedTime" INTEGER NOT NULL,
    "DeviceId" TEXT NULL,
    "FaceImage" BLOB NULL,
    "FaceImageSize" INTEGER NOT NULL,
    "DeviceType" TEXT NULL,
    "AccessType" TEXT NULL,
    "CardNumber" TEXT NULL,
    "PassLocalTime" TEXT NULL,
    "Extra" TEXT NULL,
    "WayType" TEXT NULL,
    "Remark" TEXT NULL,
    "PassTime" INTEGER NOT NULL,
    "PassRightId" TEXT NULL,
    "UnitDeviceId" TEXT NULL,
    "UnitDeviceType" TEXT NULL
);

CREATE TABLE "PASS_RIGHT" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_PASS_RIGHT" PRIMARY KEY,
    "Creator" TEXT NULL,
    "Editor" TEXT NULL,
    "CreatedTime" INTEGER NOT NULL,
    "EditedTime" INTEGER NOT NULL,
    "Sign" TEXT NULL,
    "AccessType" TEXT NULL,
    "Feature" BLOB NULL,
    "FeatureSize" INTEGER NOT NULL,
    "TimeStart" INTEGER NOT NULL,
    "TimeEnd" INTEGER NOT NULL
);

CREATE TABLE "SYSTEM_CONFIG" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_SYSTEM_CONFIG" PRIMARY KEY,
    "Creator" TEXT NULL,
    "Editor" TEXT NULL,
    "CreatedTime" INTEGER NOT NULL,
    "EditedTime" INTEGER NOT NULL,
    "Key" TEXT NULL,
    "Value" TEXT NULL
);

CREATE TABLE "UNIT_FLOOR" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_UNIT_FLOOR" PRIMARY KEY,
    "Creator" TEXT NULL,
    "Editor" TEXT NULL,
    "CreatedTime" INTEGER NOT NULL,
    "EditedTime" INTEGER NOT NULL,
    "Name" TEXT NULL,
    "RealFloorId" TEXT NULL,
    "IsPublic" INTEGER NOT NULL,
    "EdificeId" TEXT NULL,
    "EdificeName" TEXT NULL,
    "ElevatorGroupId" TEXT NULL,
    "HandleElevatorDeviceId" TEXT NULL
);

CREATE TABLE "PASS_RIGHT_DETAIL" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_PASS_RIGHT_DETAIL" PRIMARY KEY,
    "Creator" TEXT NULL,
    "Editor" TEXT NULL,
    "CreatedTime" INTEGER NOT NULL,
    "EditedTime" INTEGER NOT NULL,
    "PassRightId" TEXT NULL,
    "FloorId" TEXT NULL,
    "FloorName" TEXT NULL,
    "RealFloorId" TEXT NULL,
    "IsPublic" INTEGER NOT NULL,
    CONSTRAINT "FK_PASS_RIGHT_DETAIL_PASS_RIGHT_PassRightId" FOREIGN KEY ("PassRightId") REFERENCES "PASS_RIGHT" ("Id") ON DELETE CASCADE
);

CREATE UNIQUE INDEX "IX_CARD_DEVICE_PortName" ON "CARD_DEVICE" ("PortName");

CREATE UNIQUE INDEX "IX_PASS_RIGHT_Sign_AccessType" ON "PASS_RIGHT" ("Sign", "AccessType");

CREATE UNIQUE INDEX "IX_PASS_RIGHT_DETAIL_PassRightId_FloorId" ON "PASS_RIGHT_DETAIL" ("PassRightId", "FloorId");

CREATE UNIQUE INDEX "IX_SYSTEM_CONFIG_Key" ON "SYSTEM_CONFIG" ("Key");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20210322105652_v1.5.368', '3.1.7');

