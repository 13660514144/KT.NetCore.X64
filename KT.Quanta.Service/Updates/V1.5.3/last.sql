CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);

CREATE TABLE `CARD_DEVICE_RIGHT_GROUP` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_CARD_DEVICE_RIGHT_GROUP` PRIMARY KEY (`Id`)
);

CREATE TABLE `CommunicateInfos` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `IpAddress` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Port` int NOT NULL,
    `Account` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Password` longtext CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_CommunicateInfos` PRIMARY KEY (`Id`)
);

CREATE TABLE `DISTRIBUTE_ERROR` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `DeviceId` longtext CHARACTER SET utf8mb4 NULL,
    `PartUrl` longtext CHARACTER SET utf8mb4 NULL,
    `DataModelName` longtext CHARACTER SET utf8mb4 NULL,
    `DataId` longtext CHARACTER SET utf8mb4 NULL,
    `ErrorMessage` longtext CHARACTER SET utf8mb4 NULL,
    `ErrorTimes` int NOT NULL,
    `DataContent` longtext CHARACTER SET utf8mb4 NULL,
    `Type` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_DISTRIBUTE_ERROR` PRIMARY KEY (`Id`)
);

CREATE TABLE `EDIFICE` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_EDIFICE` PRIMARY KEY (`Id`)
);

CREATE TABLE `FACE_INFO` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Type` longtext CHARACTER SET utf8mb4 NULL,
    `Extension` longtext CHARACTER SET utf8mb4 NULL,
    `SourceUrl` longtext CHARACTER SET utf8mb4 NULL,
    `FaceUrl` longtext CHARACTER SET utf8mb4 NULL,
    `Feature` longblob NULL,
    `FeatureSize` int NOT NULL,
    CONSTRAINT `PK_FACE_INFO` PRIMARY KEY (`Id`)
);

CREATE TABLE `LOGIN_USER` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `DBAddr` longtext CHARACTER SET utf8mb4 NULL,
    `DBName` longtext CHARACTER SET utf8mb4 NULL,
    `DBUser` longtext CHARACTER SET utf8mb4 NULL,
    `DBPassword` longtext CHARACTER SET utf8mb4 NULL,
    `PCAddr` longtext CHARACTER SET utf8mb4 NULL,
    `PCUser` longtext CHARACTER SET utf8mb4 NULL,
    `PCPassword` longtext CHARACTER SET utf8mb4 NULL,
    `ServerAddress` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_LOGIN_USER` PRIMARY KEY (`Id`)
);

CREATE TABLE `PASS_RECORD` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `DeviceId` longtext CHARACTER SET utf8mb4 NULL,
    `DeviceType` longtext CHARACTER SET utf8mb4 NULL,
    `AccessType` longtext CHARACTER SET utf8mb4 NULL,
    `PassRightSign` longtext CHARACTER SET utf8mb4 NULL,
    `PassLocalTime` longtext CHARACTER SET utf8mb4 NULL,
    `PassTime` bigint NOT NULL,
    `PassRightId` longtext CHARACTER SET utf8mb4 NULL,
    `FaceImage` longblob NULL,
    `FaceImageSize` bigint NOT NULL,
    `Extra` longtext CHARACTER SET utf8mb4 NULL,
    `WayType` longtext CHARACTER SET utf8mb4 NULL,
    `Remark` longtext CHARACTER SET utf8mb4 NULL,
    `Temperature` decimal(65,30) NULL,
    `IsMask` tinyint(1) NULL,
    CONSTRAINT `PK_PASS_RECORD` PRIMARY KEY (`Id`)
);

CREATE TABLE `PASS_RIGHT_FLOOR_DIRECTION` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `Sign` longtext CHARACTER SET utf8mb4 NULL,
    `FloorId` longtext CHARACTER SET utf8mb4 NULL,
    `Direction` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_PASS_RIGHT_FLOOR_DIRECTION` PRIMARY KEY (`Id`)
);

CREATE TABLE `PERSON` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_PERSON` PRIMARY KEY (`Id`)
);

CREATE TABLE `RELAY_DEVICE` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Type` longtext CHARACTER SET utf8mb4 NULL,
    `CommunicateType` longtext CHARACTER SET utf8mb4 NULL,
    `IpAddress` varchar(255) CHARACTER SET utf8mb4 NULL,
    `Port` int NOT NULL,
    `Remark` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_RELAY_DEVICE` PRIMARY KEY (`Id`)
);

CREATE TABLE `SERIAL_CONFIG` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Baudrate` int NOT NULL,
    `Databits` int NOT NULL,
    `Stopbits` int NOT NULL,
    `Parity` int NOT NULL,
    `ReadTimeout` int NOT NULL,
    `Encoding` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_SERIAL_CONFIG` PRIMARY KEY (`Id`)
);

CREATE TABLE `SYSTEM_CONFIG` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `Key` varchar(255) CHARACTER SET utf8mb4 NULL,
    `Value` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_SYSTEM_CONFIG` PRIMARY KEY (`Id`)
);

CREATE TABLE `ELEVATOR_GROUP` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `BrandModel` longtext CHARACTER SET utf8mb4 NULL,
    `Version` longtext CHARACTER SET utf8mb4 NULL,
    `EdificeId` varchar(255) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_ELEVATOR_GROUP` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_ELEVATOR_GROUP_EDIFICE_EdificeId` FOREIGN KEY (`EdificeId`) REFERENCES `EDIFICE` (`Id`)
);

CREATE TABLE `FLOOR` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Direction` longtext CHARACTER SET utf8mb4 NULL,
    `RealFloorId` longtext CHARACTER SET utf8mb4 NULL,
    `IsPublic` tinyint(1) NOT NULL,
    `EdificeId` varchar(255) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_FLOOR` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_FLOOR_EDIFICE_EdificeId` FOREIGN KEY (`EdificeId`) REFERENCES `EDIFICE` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `ELEVATOR_INFO` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `RealId` varchar(255) CHARACTER SET utf8mb4 NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `ElevatorGroupId` varchar(255) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_ELEVATOR_INFO` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_ELEVATOR_INFO_ELEVATOR_GROUP_ElevatorGroupId` FOREIGN KEY (`ElevatorGroupId`) REFERENCES `ELEVATOR_GROUP` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `ELEVATOR_SERVER` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `IpAddress` longtext CHARACTER SET utf8mb4 NULL,
    `Port` int NOT NULL,
    `PCAccount` longtext CHARACTER SET utf8mb4 NULL,
    `PCPassword` longtext CHARACTER SET utf8mb4 NULL,
    `DBAccount` longtext CHARACTER SET utf8mb4 NULL,
    `DBPassword` longtext CHARACTER SET utf8mb4 NULL,
    `IsMain` tinyint(1) NOT NULL,
    `ElevatorGroupId` varchar(255) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_ELEVATOR_SERVER` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_ELEVATOR_SERVER_ELEVATOR_GROUP_ElevatorGroupId` FOREIGN KEY (`ElevatorGroupId`) REFERENCES `ELEVATOR_GROUP` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `ELEVATOR_GROUP_RELATION_FLOOR` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `ElevatorGroupId` varchar(255) CHARACTER SET utf8mb4 NULL,
    `FloorId` varchar(255) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_ELEVATOR_GROUP_RELATION_FLOOR` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_ELEVATOR_GROUP_RELATION_FLOOR_ELEVATOR_GROUP_ElevatorGroupId` FOREIGN KEY (`ElevatorGroupId`) REFERENCES `ELEVATOR_GROUP` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_ELEVATOR_GROUP_RELATION_FLOOR_FLOOR_FloorId` FOREIGN KEY (`FloorId`) REFERENCES `FLOOR` (`Id`)
);

CREATE TABLE `PASS_RIGHT` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `Sign` varchar(255) CHARACTER SET utf8mb4 NULL,
    `AccessType` varchar(255) CHARACTER SET utf8mb4 NULL,
    `RightType` varchar(255) CHARACTER SET utf8mb4 NULL,
    `TimeNow` bigint NOT NULL,
    `TimeOut` bigint NOT NULL,
    `FloorId` varchar(255) CHARACTER SET utf8mb4 NULL,
    `Direction` longtext CHARACTER SET utf8mb4 NULL,
    `PersonId` varchar(255) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_PASS_RIGHT` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_PASS_RIGHT_FLOOR_FloorId` FOREIGN KEY (`FloorId`) REFERENCES `FLOOR` (`Id`),
    CONSTRAINT `FK_PASS_RIGHT_PERSON_PersonId` FOREIGN KEY (`PersonId`) REFERENCES `PERSON` (`Id`)
);

CREATE TABLE `PROCESSOR` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `IpAddress` varchar(255) CHARACTER SET utf8mb4 NULL,
    `Port` int NOT NULL,
    `DeviceType` varchar(255) CHARACTER SET utf8mb4 NULL,
    `BrandModel` longtext CHARACTER SET utf8mb4 NULL,
    `FloorId` varchar(255) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_PROCESSOR` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_PROCESSOR_FLOOR_FloorId` FOREIGN KEY (`FloorId`) REFERENCES `FLOOR` (`Id`)
);

CREATE TABLE `PASS_RIGHT_RELATION_CARD_DEVICE_RIGHT_GROUP` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `PassRightId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `CardDeviceRightGroupId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_PASS_RIGHT_RELATION_CARD_DEVICE_RIGHT_GROUP` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_PASS_RIGHT_RELATION_CARD_DEVICE_RIGHT_GROUP_CARD_DEVICE_RIGH~` FOREIGN KEY (`CardDeviceRightGroupId`) REFERENCES `CARD_DEVICE_RIGHT_GROUP` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_PASS_RIGHT_RELATION_CARD_DEVICE_RIGHT_GROUP_PASS_RIGHT_PassR~` FOREIGN KEY (`PassRightId`) REFERENCES `PASS_RIGHT` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `PASS_RIGHT_RELATION_FLOOR` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `PassRightId` varchar(255) CHARACTER SET utf8mb4 NULL,
    `FloorId` varchar(255) CHARACTER SET utf8mb4 NULL,
    `Direction` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_PASS_RIGHT_RELATION_FLOOR` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_PASS_RIGHT_RELATION_FLOOR_FLOOR_FloorId` FOREIGN KEY (`FloorId`) REFERENCES `FLOOR` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_PASS_RIGHT_RELATION_FLOOR_PASS_RIGHT_PassRightId` FOREIGN KEY (`PassRightId`) REFERENCES `PASS_RIGHT` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `HANDLE_ELEVATOR_DEVICE` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `DeviceId` longtext CHARACTER SET utf8mb4 NULL,
    `DeviceType` longtext CHARACTER SET utf8mb4 NULL,
    `BrandModel` longtext CHARACTER SET utf8mb4 NULL,
    `CommunicateType` longtext CHARACTER SET utf8mb4 NULL,
    `IpAddress` longtext CHARACTER SET utf8mb4 NULL,
    `Port` int NOT NULL,
    `FaceAppId` longtext CHARACTER SET utf8mb4 NULL,
    `FaceSdkKey` longtext CHARACTER SET utf8mb4 NULL,
    `FaceActivateCode` longtext CHARACTER SET utf8mb4 NULL,
    `FloorId` varchar(255) CHARACTER SET utf8mb4 NULL,
    `ElevatorGroupId` varchar(255) CHARACTER SET utf8mb4 NULL,
    `ProcessorId` varchar(255) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_HANDLE_ELEVATOR_DEVICE` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_HANDLE_ELEVATOR_DEVICE_ELEVATOR_GROUP_ElevatorGroupId` FOREIGN KEY (`ElevatorGroupId`) REFERENCES `ELEVATOR_GROUP` (`Id`),
    CONSTRAINT `FK_HANDLE_ELEVATOR_DEVICE_FLOOR_FloorId` FOREIGN KEY (`FloorId`) REFERENCES `FLOOR` (`Id`),
    CONSTRAINT `FK_HANDLE_ELEVATOR_DEVICE_PROCESSOR_ProcessorId` FOREIGN KEY (`ProcessorId`) REFERENCES `PROCESSOR` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `PROCESSOR_FLOOR` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `SortId` int NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `ProcessorId` varchar(255) CHARACTER SET utf8mb4 NULL,
    `FloorId` varchar(255) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_PROCESSOR_FLOOR` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_PROCESSOR_FLOOR_FLOOR_FloorId` FOREIGN KEY (`FloorId`) REFERENCES `FLOOR` (`Id`),
    CONSTRAINT `FK_PROCESSOR_FLOOR_PROCESSOR_ProcessorId` FOREIGN KEY (`ProcessorId`) REFERENCES `PROCESSOR` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `CARD_DEVICE` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `BrandModel` longtext CHARACTER SET utf8mb4 NULL,
    `CardDeviceType` longtext CHARACTER SET utf8mb4 NULL,
    `DeviceType` longtext CHARACTER SET utf8mb4 NULL,
    `PortName` varchar(255) CHARACTER SET utf8mb4 NULL,
    `ProcessorId` varchar(255) CHARACTER SET utf8mb4 NULL,
    `HandElevatorDeviceId` varchar(255) CHARACTER SET utf8mb4 NULL,
    `SerialConfigId` varchar(255) CHARACTER SET utf8mb4 NULL,
    `RelayDeviceId` varchar(255) CHARACTER SET utf8mb4 NULL,
    `RelayDeviceOut` int NOT NULL,
    `HandleElevatorDeviceId` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_CARD_DEVICE` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_CARD_DEVICE_HANDLE_ELEVATOR_DEVICE_HandElevatorDeviceId` FOREIGN KEY (`HandElevatorDeviceId`) REFERENCES `HANDLE_ELEVATOR_DEVICE` (`Id`),
    CONSTRAINT `FK_CARD_DEVICE_PROCESSOR_ProcessorId` FOREIGN KEY (`ProcessorId`) REFERENCES `PROCESSOR` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_CARD_DEVICE_RELAY_DEVICE_RelayDeviceId` FOREIGN KEY (`RelayDeviceId`) REFERENCES `RELAY_DEVICE` (`Id`),
    CONSTRAINT `FK_CARD_DEVICE_SERIAL_CONFIG_SerialConfigId` FOREIGN KEY (`SerialConfigId`) REFERENCES `SERIAL_CONFIG` (`Id`)
);

CREATE TABLE `HANDLE_ELEVATOR_INPUT_DEVICE` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `AccessType` longtext CHARACTER SET utf8mb4 NULL,
    `DeviceType` longtext CHARACTER SET utf8mb4 NULL,
    `PortName` longtext CHARACTER SET utf8mb4 NULL,
    `HandElevatorDeviceId` varchar(255) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_HANDLE_ELEVATOR_INPUT_DEVICE` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_HANDLE_ELEVATOR_INPUT_DEVICE_HANDLE_ELEVATOR_DEVICE_HandElev~` FOREIGN KEY (`HandElevatorDeviceId`) REFERENCES `HANDLE_ELEVATOR_DEVICE` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `CARD_DEVICE_RIGHT_GROUP_RELATION_CARD_DEVICE` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `CardDeviceRightGroupId` varchar(255) CHARACTER SET utf8mb4 NULL,
    `CardDeviceId` varchar(255) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_CARD_DEVICE_RIGHT_GROUP_RELATION_CARD_DEVICE` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_CARD_DEVICE_RIGHT_GROUP_RELATION_CARD_DEVICE_CARD_DEVICE_Car~` FOREIGN KEY (`CardDeviceId`) REFERENCES `CARD_DEVICE` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_CARD_DEVICE_RIGHT_GROUP_RELATION_CARD_DEVICE_CARD_DEVICE_RIG~` FOREIGN KEY (`CardDeviceRightGroupId`) REFERENCES `CARD_DEVICE_RIGHT_GROUP` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_CARD_DEVICE_HandElevatorDeviceId` ON `CARD_DEVICE` (`HandElevatorDeviceId`);

CREATE INDEX `IX_CARD_DEVICE_RelayDeviceId` ON `CARD_DEVICE` (`RelayDeviceId`);

CREATE INDEX `IX_CARD_DEVICE_SerialConfigId` ON `CARD_DEVICE` (`SerialConfigId`);

CREATE UNIQUE INDEX `IX_CARD_DEVICE_ProcessorId_PortName` ON `CARD_DEVICE` (`ProcessorId`, `PortName`);

CREATE INDEX `IX_CARD_DEVICE_RIGHT_GROUP_RELATION_CARD_DEVICE_CardDeviceRight~` ON `CARD_DEVICE_RIGHT_GROUP_RELATION_CARD_DEVICE` (`CardDeviceRightGroupId`);

CREATE UNIQUE INDEX `IX_CARD_DEVICE_RIGHT_GROUP_RELATION_CARD_DEVICE_CardDeviceId_Ca~` ON `CARD_DEVICE_RIGHT_GROUP_RELATION_CARD_DEVICE` (`CardDeviceId`, `CardDeviceRightGroupId`);

CREATE INDEX `IX_ELEVATOR_GROUP_EdificeId` ON `ELEVATOR_GROUP` (`EdificeId`);

CREATE INDEX `IX_ELEVATOR_GROUP_RELATION_FLOOR_FloorId` ON `ELEVATOR_GROUP_RELATION_FLOOR` (`FloorId`);

CREATE UNIQUE INDEX `IX_ELEVATOR_GROUP_RELATION_FLOOR_ElevatorGroupId_FloorId` ON `ELEVATOR_GROUP_RELATION_FLOOR` (`ElevatorGroupId`, `FloorId`);

CREATE INDEX `IX_ELEVATOR_INFO_ElevatorGroupId` ON `ELEVATOR_INFO` (`ElevatorGroupId`);

CREATE UNIQUE INDEX `IX_ELEVATOR_INFO_RealId_ElevatorGroupId` ON `ELEVATOR_INFO` (`RealId`, `ElevatorGroupId`);

CREATE INDEX `IX_ELEVATOR_SERVER_ElevatorGroupId` ON `ELEVATOR_SERVER` (`ElevatorGroupId`);

CREATE INDEX `IX_FLOOR_EdificeId` ON `FLOOR` (`EdificeId`);

CREATE INDEX `IX_HANDLE_ELEVATOR_DEVICE_ElevatorGroupId` ON `HANDLE_ELEVATOR_DEVICE` (`ElevatorGroupId`);

CREATE INDEX `IX_HANDLE_ELEVATOR_DEVICE_FloorId` ON `HANDLE_ELEVATOR_DEVICE` (`FloorId`);

CREATE INDEX `IX_HANDLE_ELEVATOR_DEVICE_ProcessorId` ON `HANDLE_ELEVATOR_DEVICE` (`ProcessorId`);

CREATE INDEX `IX_HANDLE_ELEVATOR_INPUT_DEVICE_HandElevatorDeviceId` ON `HANDLE_ELEVATOR_INPUT_DEVICE` (`HandElevatorDeviceId`);

CREATE INDEX `IX_PASS_RIGHT_FloorId` ON `PASS_RIGHT` (`FloorId`);

CREATE INDEX `IX_PASS_RIGHT_PersonId` ON `PASS_RIGHT` (`PersonId`);

CREATE UNIQUE INDEX `IX_PASS_RIGHT_Sign_AccessType_RightType` ON `PASS_RIGHT` (`Sign`, `AccessType`, `RightType`);

CREATE INDEX `IX_PASS_RIGHT_RELATION_CARD_DEVICE_RIGHT_GROUP_CardDeviceRightG~` ON `PASS_RIGHT_RELATION_CARD_DEVICE_RIGHT_GROUP` (`CardDeviceRightGroupId`);

CREATE UNIQUE INDEX `IX_PASS_RIGHT_RELATION_CARD_DEVICE_RIGHT_GROUP_PassRightId_Card~` ON `PASS_RIGHT_RELATION_CARD_DEVICE_RIGHT_GROUP` (`PassRightId`, `CardDeviceRightGroupId`);

CREATE INDEX `IX_PASS_RIGHT_RELATION_FLOOR_FloorId` ON `PASS_RIGHT_RELATION_FLOOR` (`FloorId`);

CREATE INDEX `IX_PASS_RIGHT_RELATION_FLOOR_PassRightId` ON `PASS_RIGHT_RELATION_FLOOR` (`PassRightId`);

CREATE INDEX `IX_PROCESSOR_FloorId` ON `PROCESSOR` (`FloorId`);

CREATE UNIQUE INDEX `IX_PROCESSOR_IpAddress_Port_DeviceType` ON `PROCESSOR` (`IpAddress`, `Port`, `DeviceType`);

CREATE INDEX `IX_PROCESSOR_FLOOR_FloorId` ON `PROCESSOR_FLOOR` (`FloorId`);

CREATE UNIQUE INDEX `IX_PROCESSOR_FLOOR_ProcessorId_FloorId` ON `PROCESSOR_FLOOR` (`ProcessorId`, `FloorId`);

CREATE UNIQUE INDEX `IX_PROCESSOR_FLOOR_ProcessorId_SortId` ON `PROCESSOR_FLOOR` (`ProcessorId`, `SortId`);

CREATE UNIQUE INDEX `IX_RELAY_DEVICE_IpAddress_Port` ON `RELAY_DEVICE` (`IpAddress`, `Port`);

CREATE UNIQUE INDEX `IX_SYSTEM_CONFIG_Key` ON `SYSTEM_CONFIG` (`Key`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210310055806_v1.5.360', '3.1.7');

CREATE TABLE `DopGlobalDefaultAccessMasks` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `AddressKey` longtext CHARACTER SET utf8mb4 NULL,
    `ConnectedState` int NOT NULL,
    `ElevatorGroupId` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_DopGlobalDefaultAccessMasks` PRIMARY KEY (`Id`)
);

CREATE TABLE `DopSpecificDefaultAccessMasks` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `ConnectedState` int NOT NULL,
    `ElevatorGroupId` longtext CHARACTER SET utf8mb4 NULL,
    `HandleElevatorDeviceId` int NOT NULL,
    CONSTRAINT `PK_DopSpecificDefaultAccessMasks` PRIMARY KEY (`Id`)
);

CREATE TABLE `DopGlobalDefaultAccessFloorMasks` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `Floor` int NOT NULL,
    `IsDestinationFront` tinyint(1) NOT NULL,
    `IsDestinationRear` tinyint(1) NOT NULL,
    `IsSourceFront` tinyint(1) NOT NULL,
    `IsSourceRear` tinyint(1) NOT NULL,
    `DopGlobalDefaultAccessMaskEntityId` varchar(255) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_DopGlobalDefaultAccessFloorMasks` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_DopGlobalDefaultAccessFloorMasks_DopGlobalDefaultAccessMasks~` FOREIGN KEY (`DopGlobalDefaultAccessMaskEntityId`) REFERENCES `DopGlobalDefaultAccessMasks` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `DopSpecificDefaultAccessFloorMasks` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `Floor` int NOT NULL,
    `IsDestinationFront` tinyint(1) NOT NULL,
    `IsDestinationRear` tinyint(1) NOT NULL,
    `DopSpecificDefaultAccessMaskEntityId` varchar(255) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_DopSpecificDefaultAccessFloorMasks` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_DopSpecificDefaultAccessFloorMasks_DopSpecificDefaultAccessM~` FOREIGN KEY (`DopSpecificDefaultAccessMaskEntityId`) REFERENCES `DopSpecificDefaultAccessMasks` (`Id`) ON DELETE RESTRICT
);

CREATE INDEX `IX_DopGlobalDefaultAccessFloorMasks_DopGlobalDefaultAccessMaskE~` ON `DopGlobalDefaultAccessFloorMasks` (`DopGlobalDefaultAccessMaskEntityId`);

CREATE INDEX `IX_DopSpecificDefaultAccessFloorMasks_DopSpecificDefaultAccessM~` ON `DopSpecificDefaultAccessFloorMasks` (`DopSpecificDefaultAccessMaskEntityId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210311014640_v1.5.361', '3.1.7');

ALTER TABLE `DopGlobalDefaultAccessFloorMasks` DROP FOREIGN KEY `FK_DopGlobalDefaultAccessFloorMasks_DopGlobalDefaultAccessMasks~`;

ALTER TABLE `DopSpecificDefaultAccessFloorMasks` DROP FOREIGN KEY `FK_DopSpecificDefaultAccessFloorMasks_DopSpecificDefaultAccessM~`;

ALTER TABLE `DopSpecificDefaultAccessFloorMasks` DROP INDEX `IX_DopSpecificDefaultAccessFloorMasks_DopSpecificDefaultAccessM~`;

ALTER TABLE `DopGlobalDefaultAccessFloorMasks` DROP INDEX `IX_DopGlobalDefaultAccessFloorMasks_DopGlobalDefaultAccessMaskE~`;

ALTER TABLE `DopSpecificDefaultAccessFloorMasks` DROP COLUMN `DopSpecificDefaultAccessMaskEntityId`;

ALTER TABLE `DopGlobalDefaultAccessMasks` DROP COLUMN `AddressKey`;

ALTER TABLE `DopGlobalDefaultAccessFloorMasks` DROP COLUMN `DopGlobalDefaultAccessMaskEntityId`;

ALTER TABLE `DopSpecificDefaultAccessMasks` MODIFY COLUMN `ElevatorGroupId` varchar(255) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `DopSpecificDefaultAccessFloorMasks` ADD `AccessMaskId` varchar(255) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `DopGlobalDefaultAccessMasks` MODIFY COLUMN `ElevatorGroupId` varchar(255) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `DopGlobalDefaultAccessFloorMasks` ADD `AccessMaskId` varchar(255) CHARACTER SET utf8mb4 NULL;

CREATE UNIQUE INDEX `IX_DopSpecificDefaultAccessMasks_ConnectedState_ElevatorGroupId~` ON `DopSpecificDefaultAccessMasks` (`ConnectedState`, `ElevatorGroupId`, `HandleElevatorDeviceId`);

CREATE UNIQUE INDEX `IX_DopSpecificDefaultAccessFloorMasks_AccessMaskId_Floor` ON `DopSpecificDefaultAccessFloorMasks` (`AccessMaskId`, `Floor`);

CREATE UNIQUE INDEX `IX_DopGlobalDefaultAccessMasks_ConnectedState_ElevatorGroupId` ON `DopGlobalDefaultAccessMasks` (`ConnectedState`, `ElevatorGroupId`);

CREATE INDEX `IX_DopGlobalDefaultAccessFloorMasks_AccessMaskId` ON `DopGlobalDefaultAccessFloorMasks` (`AccessMaskId`);

ALTER TABLE `DopGlobalDefaultAccessFloorMasks` ADD CONSTRAINT `FK_DopGlobalDefaultAccessFloorMasks_DopGlobalDefaultAccessMasks~` FOREIGN KEY (`AccessMaskId`) REFERENCES `DopGlobalDefaultAccessMasks` (`Id`) ON DELETE CASCADE;

ALTER TABLE `DopSpecificDefaultAccessFloorMasks` ADD CONSTRAINT `FK_DopSpecificDefaultAccessFloorMasks_DopSpecificDefaultAccessM~` FOREIGN KEY (`AccessMaskId`) REFERENCES `DopSpecificDefaultAccessMasks` (`Id`) ON DELETE CASCADE;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210312072927_v1.5.362', '3.1.7');

DROP PROCEDURE IF EXISTS `POMELO_BEFORE_DROP_PRIMARY_KEY`;
DELIMITER //
CREATE PROCEDURE `POMELO_BEFORE_DROP_PRIMARY_KEY`(IN `SCHEMA_NAME_ARGUMENT` VARCHAR(255), IN `TABLE_NAME_ARGUMENT` VARCHAR(255))
BEGIN
	DECLARE HAS_AUTO_INCREMENT_ID TINYINT(1);
	DECLARE PRIMARY_KEY_COLUMN_NAME VARCHAR(255);
	DECLARE PRIMARY_KEY_TYPE VARCHAR(255);
	DECLARE SQL_EXP VARCHAR(1000);
	SELECT COUNT(*)
		INTO HAS_AUTO_INCREMENT_ID
		FROM `information_schema`.`COLUMNS`
		WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
			AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
			AND `Extra` = 'auto_increment'
			AND `COLUMN_KEY` = 'PRI'
			LIMIT 1;
	IF HAS_AUTO_INCREMENT_ID THEN
		SELECT `COLUMN_TYPE`
			INTO PRIMARY_KEY_TYPE
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_KEY` = 'PRI'
			LIMIT 1;
		SELECT `COLUMN_NAME`
			INTO PRIMARY_KEY_COLUMN_NAME
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_KEY` = 'PRI'
			LIMIT 1;
		SET SQL_EXP = CONCAT('ALTER TABLE `', (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA())), '`.`', TABLE_NAME_ARGUMENT, '` MODIFY COLUMN `', PRIMARY_KEY_COLUMN_NAME, '` ', PRIMARY_KEY_TYPE, ' NOT NULL;');
		SET @SQL_EXP = SQL_EXP;
		PREPARE SQL_EXP_EXECUTE FROM @SQL_EXP;
		EXECUTE SQL_EXP_EXECUTE;
		DEALLOCATE PREPARE SQL_EXP_EXECUTE;
	END IF;
END //
DELIMITER ;

DROP PROCEDURE IF EXISTS `POMELO_AFTER_ADD_PRIMARY_KEY`;
DELIMITER //
CREATE PROCEDURE `POMELO_AFTER_ADD_PRIMARY_KEY`(IN `SCHEMA_NAME_ARGUMENT` VARCHAR(255), IN `TABLE_NAME_ARGUMENT` VARCHAR(255), IN `COLUMN_NAME_ARGUMENT` VARCHAR(255))
BEGIN
	DECLARE HAS_AUTO_INCREMENT_ID INT(11);
	DECLARE PRIMARY_KEY_COLUMN_NAME VARCHAR(255);
	DECLARE PRIMARY_KEY_TYPE VARCHAR(255);
	DECLARE SQL_EXP VARCHAR(1000);
	SELECT COUNT(*)
		INTO HAS_AUTO_INCREMENT_ID
		FROM `information_schema`.`COLUMNS`
		WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
			AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
			AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
			AND `COLUMN_TYPE` LIKE '%int%'
			AND `COLUMN_KEY` = 'PRI';
	IF HAS_AUTO_INCREMENT_ID THEN
		SELECT `COLUMN_TYPE`
			INTO PRIMARY_KEY_TYPE
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
				AND `COLUMN_TYPE` LIKE '%int%'
				AND `COLUMN_KEY` = 'PRI';
		SELECT `COLUMN_NAME`
			INTO PRIMARY_KEY_COLUMN_NAME
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
				AND `COLUMN_TYPE` LIKE '%int%'
				AND `COLUMN_KEY` = 'PRI';
		SET SQL_EXP = CONCAT('ALTER TABLE `', (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA())), '`.`', TABLE_NAME_ARGUMENT, '` MODIFY COLUMN `', PRIMARY_KEY_COLUMN_NAME, '` ', PRIMARY_KEY_TYPE, ' NOT NULL AUTO_INCREMENT;');
		SET @SQL_EXP = SQL_EXP;
		PREPARE SQL_EXP_EXECUTE FROM @SQL_EXP;
		EXECUTE SQL_EXP_EXECUTE;
		DEALLOCATE PREPARE SQL_EXP_EXECUTE;
	END IF;
END //
DELIMITER ;

ALTER TABLE `DopGlobalDefaultAccessFloorMasks` DROP FOREIGN KEY `FK_DopGlobalDefaultAccessFloorMasks_DopGlobalDefaultAccessMasks~`;

ALTER TABLE `DopSpecificDefaultAccessFloorMasks` DROP FOREIGN KEY `FK_DopSpecificDefaultAccessFloorMasks_DopSpecificDefaultAccessM~`;

CALL POMELO_BEFORE_DROP_PRIMARY_KEY(NULL, 'DopSpecificDefaultAccessMasks');
ALTER TABLE `DopSpecificDefaultAccessMasks` DROP PRIMARY KEY;

CALL POMELO_BEFORE_DROP_PRIMARY_KEY(NULL, 'DopSpecificDefaultAccessFloorMasks');
ALTER TABLE `DopSpecificDefaultAccessFloorMasks` DROP PRIMARY KEY;

ALTER TABLE `DopSpecificDefaultAccessFloorMasks` DROP INDEX `IX_DopSpecificDefaultAccessFloorMasks_AccessMaskId_Floor`;

CALL POMELO_BEFORE_DROP_PRIMARY_KEY(NULL, 'DopGlobalDefaultAccessMasks');
ALTER TABLE `DopGlobalDefaultAccessMasks` DROP PRIMARY KEY;

CALL POMELO_BEFORE_DROP_PRIMARY_KEY(NULL, 'DopGlobalDefaultAccessFloorMasks');
ALTER TABLE `DopGlobalDefaultAccessFloorMasks` DROP PRIMARY KEY;

ALTER TABLE `DopSpecificDefaultAccessFloorMasks` DROP COLUMN `Floor`;

ALTER TABLE `DopGlobalDefaultAccessFloorMasks` DROP COLUMN `Floor`;

ALTER TABLE `DopSpecificDefaultAccessMasks` RENAME `DOP_SPECIFIC_DEFAULT_ACCESS_MASK`;

ALTER TABLE `DopSpecificDefaultAccessFloorMasks` RENAME `DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK`;

ALTER TABLE `DopGlobalDefaultAccessMasks` RENAME `DOP_GLOBAL_DEFAULT_ACCESS_MASK`;

ALTER TABLE `DopGlobalDefaultAccessFloorMasks` RENAME `DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK`;

ALTER TABLE `DOP_SPECIFIC_DEFAULT_ACCESS_MASK` RENAME INDEX `IX_DopSpecificDefaultAccessMasks_ConnectedState_ElevatorGroupId~` TO `IX_DOP_SPECIFIC_DEFAULT_ACCESS_MASK_ConnectedState_ElevatorGrou~`;

ALTER TABLE `DOP_GLOBAL_DEFAULT_ACCESS_MASK` RENAME INDEX `IX_DopGlobalDefaultAccessMasks_ConnectedState_ElevatorGroupId` TO `IX_DOP_GLOBAL_DEFAULT_ACCESS_MASK_ConnectedState_ElevatorGroupId`;

ALTER TABLE `DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK` RENAME INDEX `IX_DopGlobalDefaultAccessFloorMasks_AccessMaskId` TO `IX_DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK_AccessMaskId`;

ALTER TABLE `DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK` ADD `FloorId` varchar(255) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK` ADD `FloorId` varchar(255) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `DOP_SPECIFIC_DEFAULT_ACCESS_MASK` ADD CONSTRAINT `PK_DOP_SPECIFIC_DEFAULT_ACCESS_MASK` PRIMARY KEY (`Id`);
CALL POMELO_AFTER_ADD_PRIMARY_KEY(NULL, 'DOP_SPECIFIC_DEFAULT_ACCESS_MASK', 'Id');

ALTER TABLE `DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK` ADD CONSTRAINT `PK_DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK` PRIMARY KEY (`Id`);
CALL POMELO_AFTER_ADD_PRIMARY_KEY(NULL, 'DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK', 'Id');

ALTER TABLE `DOP_GLOBAL_DEFAULT_ACCESS_MASK` ADD CONSTRAINT `PK_DOP_GLOBAL_DEFAULT_ACCESS_MASK` PRIMARY KEY (`Id`);
CALL POMELO_AFTER_ADD_PRIMARY_KEY(NULL, 'DOP_GLOBAL_DEFAULT_ACCESS_MASK', 'Id');

ALTER TABLE `DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK` ADD CONSTRAINT `PK_DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK` PRIMARY KEY (`Id`);
CALL POMELO_AFTER_ADD_PRIMARY_KEY(NULL, 'DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK', 'Id');

CREATE INDEX `IX_DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK_FloorId` ON `DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK` (`FloorId`);

CREATE UNIQUE INDEX `IX_DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK_AccessMaskId_FloorId` ON `DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK` (`AccessMaskId`, `FloorId`);

CREATE INDEX `IX_DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK_FloorId` ON `DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK` (`FloorId`);

ALTER TABLE `DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK` ADD CONSTRAINT `FK_DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK_DOP_GLOBAL_DEFAULT_ACCE~` FOREIGN KEY (`AccessMaskId`) REFERENCES `DOP_GLOBAL_DEFAULT_ACCESS_MASK` (`Id`) ON DELETE CASCADE;

ALTER TABLE `DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK` ADD CONSTRAINT `FK_DOP_GLOBAL_DEFAULT_ACCESS_FLOOR_MASK_FLOOR_FloorId` FOREIGN KEY (`FloorId`) REFERENCES `FLOOR` (`Id`) ON DELETE RESTRICT;

ALTER TABLE `DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK` ADD CONSTRAINT `FK_DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK_DOP_SPECIFIC_DEFAULT_~` FOREIGN KEY (`AccessMaskId`) REFERENCES `DOP_SPECIFIC_DEFAULT_ACCESS_MASK` (`Id`) ON DELETE CASCADE;

ALTER TABLE `DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK` ADD CONSTRAINT `FK_DOP_SPECIFIC_DEFAULT_ACCESS_FLOOR_MASK_FLOOR_FloorId` FOREIGN KEY (`FloorId`) REFERENCES `FLOOR` (`Id`) ON DELETE CASCADE;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210315062335_v1.5.363', '3.1.7');

DROP PROCEDURE `POMELO_BEFORE_DROP_PRIMARY_KEY`;

DROP PROCEDURE `POMELO_AFTER_ADD_PRIMARY_KEY`;

ALTER TABLE `DOP_SPECIFIC_DEFAULT_ACCESS_MASK` MODIFY COLUMN `HandleElevatorDeviceId` varchar(255) CHARACTER SET utf8mb4 NULL;

CREATE INDEX `IX_DOP_SPECIFIC_DEFAULT_ACCESS_MASK_ElevatorGroupId` ON `DOP_SPECIFIC_DEFAULT_ACCESS_MASK` (`ElevatorGroupId`);

CREATE INDEX `IX_DOP_SPECIFIC_DEFAULT_ACCESS_MASK_HandleElevatorDeviceId` ON `DOP_SPECIFIC_DEFAULT_ACCESS_MASK` (`HandleElevatorDeviceId`);

CREATE INDEX `IX_DOP_GLOBAL_DEFAULT_ACCESS_MASK_ElevatorGroupId` ON `DOP_GLOBAL_DEFAULT_ACCESS_MASK` (`ElevatorGroupId`);

ALTER TABLE `DOP_GLOBAL_DEFAULT_ACCESS_MASK` ADD CONSTRAINT `FK_DOP_GLOBAL_DEFAULT_ACCESS_MASK_ELEVATOR_GROUP_ElevatorGroupId` FOREIGN KEY (`ElevatorGroupId`) REFERENCES `ELEVATOR_GROUP` (`Id`) ON DELETE CASCADE;

ALTER TABLE `DOP_SPECIFIC_DEFAULT_ACCESS_MASK` ADD CONSTRAINT `FK_DOP_SPECIFIC_DEFAULT_ACCESS_MASK_ELEVATOR_GROUP_ElevatorGrou~` FOREIGN KEY (`ElevatorGroupId`) REFERENCES `ELEVATOR_GROUP` (`Id`) ON DELETE CASCADE;

ALTER TABLE `DOP_SPECIFIC_DEFAULT_ACCESS_MASK` ADD CONSTRAINT `FK_DOP_SPECIFIC_DEFAULT_ACCESS_MASK_HANDLE_ELEVATOR_DEVICE_Hand~` FOREIGN KEY (`HandleElevatorDeviceId`) REFERENCES `HANDLE_ELEVATOR_DEVICE` (`Id`) ON DELETE CASCADE;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210315090319_v1.5.364', '3.1.7');

DROP TABLE `PASS_RIGHT_FLOOR_DIRECTION`;

CREATE TABLE `PASS_RIGHT_AUXILIARY` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `HandleDeviceId` longtext CHARACTER SET utf8mb4 NULL,
    `Sign` longtext CHARACTER SET utf8mb4 NULL,
    `FloorId` longtext CHARACTER SET utf8mb4 NULL,
    `IsDestinationFloor` tinyint(1) NOT NULL,
    `Direction` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_PASS_RIGHT_AUXILIARY` PRIMARY KEY (`Id`)
);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210318123833_v1.5.366', '3.1.7');

ALTER TABLE `PASS_RIGHT_AUXILIARY` DROP COLUMN `Direction`;

ALTER TABLE `PASS_RIGHT_AUXILIARY` DROP COLUMN `FloorId`;

ALTER TABLE `PASS_RIGHT_AUXILIARY` DROP COLUMN `HandleDeviceId`;

ALTER TABLE `PASS_RIGHT_AUXILIARY` DROP COLUMN `IsDestinationFloor`;

ALTER TABLE `PASS_RIGHT_AUXILIARY` ADD `HandleElevatorDeviceId` longtext CHARACTER SET utf8mb4 NULL;

CREATE TABLE `HANDLE_ELEVATOR_DEVICE_AUXILIARY` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `HandleElevatorDeviceId` longtext CHARACTER SET utf8mb4 NULL,
    `Direction` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_HANDLE_ELEVATOR_DEVICE_AUXILIARY` PRIMARY KEY (`Id`)
);

CREATE TABLE `PASS_RIGHT_AUXILIARY_DETAIL` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `FloorId` varchar(255) CHARACTER SET utf8mb4 NULL,
    `IsDestinationFloor` tinyint(1) NOT NULL,
    `Direction` longtext CHARACTER SET utf8mb4 NULL,
    `PassRightAuxiliaryId` varchar(255) CHARACTER SET utf8mb4 NULL,
    `PassRightAuxiliaryEntityId` varchar(255) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_PASS_RIGHT_AUXILIARY_DETAIL` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_PASS_RIGHT_AUXILIARY_DETAIL_FLOOR_FloorId` FOREIGN KEY (`FloorId`) REFERENCES `FLOOR` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_PASS_RIGHT_AUXILIARY_DETAIL_PASS_RIGHT_AUXILIARY_PassRightAu~` FOREIGN KEY (`PassRightAuxiliaryEntityId`) REFERENCES `PASS_RIGHT_AUXILIARY` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_PASS_RIGHT_AUXILIARY_DETAIL_PASS_RIGHT_AUXILIARY_PassRightA~1` FOREIGN KEY (`PassRightAuxiliaryId`) REFERENCES `PASS_RIGHT_AUXILIARY` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_PASS_RIGHT_AUXILIARY_DETAIL_FloorId` ON `PASS_RIGHT_AUXILIARY_DETAIL` (`FloorId`);

CREATE INDEX `IX_PASS_RIGHT_AUXILIARY_DETAIL_PassRightAuxiliaryEntityId` ON `PASS_RIGHT_AUXILIARY_DETAIL` (`PassRightAuxiliaryEntityId`);

CREATE INDEX `IX_PASS_RIGHT_AUXILIARY_DETAIL_PassRightAuxiliaryId` ON `PASS_RIGHT_AUXILIARY_DETAIL` (`PassRightAuxiliaryId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210318162856_v1.5.367', '3.1.7');

ALTER TABLE `PASS_RIGHT_AUXILIARY` DROP COLUMN `HandleElevatorDeviceId`;

ALTER TABLE `PASS_RIGHT_AUXILIARY` ADD `ElevatorGroupId` longtext CHARACTER SET utf8mb4 NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210319033756_v1.5.368', '3.1.7');

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210321135812_v1.5.369', '3.1.7');

ALTER TABLE `HANDLE_ELEVATOR_DEVICE_AUXILIARY` MODIFY COLUMN `HandleElevatorDeviceId` varchar(255) CHARACTER SET utf8mb4 NULL;

CREATE UNIQUE INDEX `IX_HANDLE_ELEVATOR_DEVICE_AUXILIARY_HandleElevatorDeviceId` ON `HANDLE_ELEVATOR_DEVICE_AUXILIARY` (`HandleElevatorDeviceId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210321140233_v1.5.370', '3.1.7');

ALTER TABLE `PASS_RIGHT_RELATION_FLOOR` DROP COLUMN `Direction`;

ALTER TABLE `PASS_RIGHT_AUXILIARY_DETAIL` DROP COLUMN `Direction`;

ALTER TABLE `PASS_RIGHT` DROP COLUMN `Direction`;

ALTER TABLE `HANDLE_ELEVATOR_DEVICE_AUXILIARY` DROP COLUMN `Direction`;

ALTER TABLE `FLOOR` DROP COLUMN `Direction`;

ALTER TABLE `PASS_RIGHT_RELATION_FLOOR` ADD `IsFront` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `PASS_RIGHT_RELATION_FLOOR` ADD `IsRear` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `PASS_RIGHT_AUXILIARY_DETAIL` ADD `IsFront` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `PASS_RIGHT_AUXILIARY_DETAIL` ADD `IsRear` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `PASS_RIGHT` ADD `IsFront` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `PASS_RIGHT` ADD `IsRear` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `HANDLE_ELEVATOR_DEVICE_AUXILIARY` ADD `IsFront` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `HANDLE_ELEVATOR_DEVICE_AUXILIARY` ADD `IsRear` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `FLOOR` ADD `IsFront` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `FLOOR` ADD `IsRear` tinyint(1) NOT NULL DEFAULT FALSE;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210323081130_v1.5.372', '3.1.7');

ALTER TABLE `PASS_RIGHT_AUXILIARY_DETAIL` DROP FOREIGN KEY `FK_PASS_RIGHT_AUXILIARY_DETAIL_PASS_RIGHT_AUXILIARY_PassRightAu~`;

ALTER TABLE `PASS_RIGHT_AUXILIARY_DETAIL` DROP FOREIGN KEY `FK_PASS_RIGHT_AUXILIARY_DETAIL_PASS_RIGHT_AUXILIARY_PassRightA~1`;

ALTER TABLE `PASS_RIGHT_AUXILIARY_DETAIL` DROP INDEX `IX_PASS_RIGHT_AUXILIARY_DETAIL_PassRightAuxiliaryEntityId`;

ALTER TABLE `PASS_RIGHT_AUXILIARY_DETAIL` DROP COLUMN `PassRightAuxiliaryEntityId`;

ALTER TABLE `PASS_RIGHT_AUXILIARY_DETAIL` ADD CONSTRAINT `FK_PASS_RIGHT_AUXILIARY_DETAIL_PASS_RIGHT_AUXILIARY_PassRightAu~` FOREIGN KEY (`PassRightAuxiliaryId`) REFERENCES `PASS_RIGHT_AUXILIARY` (`Id`) ON DELETE CASCADE;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210323095943_v1.5.373', '3.1.7');

DROP TABLE `PASS_RIGHT_AUXILIARY_DETAIL`;

DROP TABLE `PASS_RIGHT_AUXILIARY`;

CREATE TABLE `PASS_RIGHT_ACCESSIBLE_FLOOR` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `Sign` longtext CHARACTER SET utf8mb4 NULL,
    `ElevatorGroupId` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_PASS_RIGHT_ACCESSIBLE_FLOOR` PRIMARY KEY (`Id`)
);

CREATE TABLE `PASS_RIGHT_DESTINATION_FLOOR_FLOOR` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `Sign` longtext CHARACTER SET utf8mb4 NULL,
    `ElevatorGroupId` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_PASS_RIGHT_DESTINATION_FLOOR_FLOOR` PRIMARY KEY (`Id`)
);

CREATE TABLE `PASS_RIGHT_ACCESSIBLE_FLOOR_DETAIL` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `FloorId` varchar(255) CHARACTER SET utf8mb4 NULL,
    `IsFront` tinyint(1) NOT NULL,
    `IsRear` tinyint(1) NOT NULL,
    `PassRightAccessibleFloorId` varchar(255) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_PASS_RIGHT_ACCESSIBLE_FLOOR_DETAIL` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_PASS_RIGHT_ACCESSIBLE_FLOOR_DETAIL_FLOOR_FloorId` FOREIGN KEY (`FloorId`) REFERENCES `FLOOR` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_PASS_RIGHT_ACCESSIBLE_FLOOR_DETAIL_PASS_RIGHT_ACCESSIBLE_FLO~` FOREIGN KEY (`PassRightAccessibleFloorId`) REFERENCES `PASS_RIGHT_ACCESSIBLE_FLOOR` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `PASS_RIGHT_DESTINATION_FLOOR_DETAIL` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `FloorId` varchar(255) CHARACTER SET utf8mb4 NULL,
    `IsFront` tinyint(1) NOT NULL,
    `IsRear` tinyint(1) NOT NULL,
    `PassRightDestinationFloorId` varchar(255) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_PASS_RIGHT_DESTINATION_FLOOR_DETAIL` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_PASS_RIGHT_DESTINATION_FLOOR_DETAIL_FLOOR_FloorId` FOREIGN KEY (`FloorId`) REFERENCES `FLOOR` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_PASS_RIGHT_DESTINATION_FLOOR_DETAIL_PASS_RIGHT_DESTINATION_F~` FOREIGN KEY (`PassRightDestinationFloorId`) REFERENCES `PASS_RIGHT_DESTINATION_FLOOR_FLOOR` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_PASS_RIGHT_ACCESSIBLE_FLOOR_DETAIL_FloorId` ON `PASS_RIGHT_ACCESSIBLE_FLOOR_DETAIL` (`FloorId`);

CREATE INDEX `IX_PASS_RIGHT_ACCESSIBLE_FLOOR_DETAIL_PassRightAccessibleFloorId` ON `PASS_RIGHT_ACCESSIBLE_FLOOR_DETAIL` (`PassRightAccessibleFloorId`);

CREATE INDEX `IX_PASS_RIGHT_DESTINATION_FLOOR_DETAIL_FloorId` ON `PASS_RIGHT_DESTINATION_FLOOR_DETAIL` (`FloorId`);

CREATE INDEX `IX_PASS_RIGHT_DESTINATION_FLOOR_DETAIL_PassRightDestinationFloo~` ON `PASS_RIGHT_DESTINATION_FLOOR_DETAIL` (`PassRightDestinationFloorId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210330073521_v1.5.374', '3.1.7');

DROP TABLE `PASS_RIGHT_DESTINATION_FLOOR_DETAIL`;

ALTER TABLE `PASS_RIGHT_DESTINATION_FLOOR_FLOOR` ADD `FloorId` varchar(255) CHARACTER SET utf8mb4 NULL;

ALTER TABLE `PASS_RIGHT_DESTINATION_FLOOR_FLOOR` ADD `IsFront` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `PASS_RIGHT_DESTINATION_FLOOR_FLOOR` ADD `IsRear` tinyint(1) NOT NULL DEFAULT FALSE;

CREATE INDEX `IX_PASS_RIGHT_DESTINATION_FLOOR_FLOOR_FloorId` ON `PASS_RIGHT_DESTINATION_FLOOR_FLOOR` (`FloorId`);

ALTER TABLE `PASS_RIGHT_DESTINATION_FLOOR_FLOOR` ADD CONSTRAINT `FK_PASS_RIGHT_DESTINATION_FLOOR_FLOOR_FLOOR_FloorId` FOREIGN KEY (`FloorId`) REFERENCES `FLOOR` (`Id`) ON DELETE CASCADE;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210330084227_v1.5.375', '3.1.7');

CREATE TABLE `DopMaskRecords` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `ElevatorServer` longtext CHARACTER SET utf8mb4 NULL,
    `Type` longtext CHARACTER SET utf8mb4 NULL,
    `Operate` longtext CHARACTER SET utf8mb4 NULL,
    `IsSucess` tinyint(1) NOT NULL,
    `Status` int NOT NULL,
    `SendData` longtext CHARACTER SET utf8mb4 NULL,
    `ReceiveData` longtext CHARACTER SET utf8mb4 NULL,
    `Message` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_DopMaskRecords` PRIMARY KEY (`Id`)
);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210401053926_v1.5.376', '3.1.7');

DROP PROCEDURE IF EXISTS `POMELO_BEFORE_DROP_PRIMARY_KEY`;
DELIMITER //
CREATE PROCEDURE `POMELO_BEFORE_DROP_PRIMARY_KEY`(IN `SCHEMA_NAME_ARGUMENT` VARCHAR(255), IN `TABLE_NAME_ARGUMENT` VARCHAR(255))
BEGIN
	DECLARE HAS_AUTO_INCREMENT_ID TINYINT(1);
	DECLARE PRIMARY_KEY_COLUMN_NAME VARCHAR(255);
	DECLARE PRIMARY_KEY_TYPE VARCHAR(255);
	DECLARE SQL_EXP VARCHAR(1000);
	SELECT COUNT(*)
		INTO HAS_AUTO_INCREMENT_ID
		FROM `information_schema`.`COLUMNS`
		WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
			AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
			AND `Extra` = 'auto_increment'
			AND `COLUMN_KEY` = 'PRI'
			LIMIT 1;
	IF HAS_AUTO_INCREMENT_ID THEN
		SELECT `COLUMN_TYPE`
			INTO PRIMARY_KEY_TYPE
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_KEY` = 'PRI'
			LIMIT 1;
		SELECT `COLUMN_NAME`
			INTO PRIMARY_KEY_COLUMN_NAME
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_KEY` = 'PRI'
			LIMIT 1;
		SET SQL_EXP = CONCAT('ALTER TABLE `', (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA())), '`.`', TABLE_NAME_ARGUMENT, '` MODIFY COLUMN `', PRIMARY_KEY_COLUMN_NAME, '` ', PRIMARY_KEY_TYPE, ' NOT NULL;');
		SET @SQL_EXP = SQL_EXP;
		PREPARE SQL_EXP_EXECUTE FROM @SQL_EXP;
		EXECUTE SQL_EXP_EXECUTE;
		DEALLOCATE PREPARE SQL_EXP_EXECUTE;
	END IF;
END //
DELIMITER ;

DROP PROCEDURE IF EXISTS `POMELO_AFTER_ADD_PRIMARY_KEY`;
DELIMITER //
CREATE PROCEDURE `POMELO_AFTER_ADD_PRIMARY_KEY`(IN `SCHEMA_NAME_ARGUMENT` VARCHAR(255), IN `TABLE_NAME_ARGUMENT` VARCHAR(255), IN `COLUMN_NAME_ARGUMENT` VARCHAR(255))
BEGIN
	DECLARE HAS_AUTO_INCREMENT_ID INT(11);
	DECLARE PRIMARY_KEY_COLUMN_NAME VARCHAR(255);
	DECLARE PRIMARY_KEY_TYPE VARCHAR(255);
	DECLARE SQL_EXP VARCHAR(1000);
	SELECT COUNT(*)
		INTO HAS_AUTO_INCREMENT_ID
		FROM `information_schema`.`COLUMNS`
		WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
			AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
			AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
			AND `COLUMN_TYPE` LIKE '%int%'
			AND `COLUMN_KEY` = 'PRI';
	IF HAS_AUTO_INCREMENT_ID THEN
		SELECT `COLUMN_TYPE`
			INTO PRIMARY_KEY_TYPE
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
				AND `COLUMN_TYPE` LIKE '%int%'
				AND `COLUMN_KEY` = 'PRI';
		SELECT `COLUMN_NAME`
			INTO PRIMARY_KEY_COLUMN_NAME
			FROM `information_schema`.`COLUMNS`
			WHERE `TABLE_SCHEMA` = (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA()))
				AND `TABLE_NAME` = TABLE_NAME_ARGUMENT
				AND `COLUMN_NAME` = COLUMN_NAME_ARGUMENT
				AND `COLUMN_TYPE` LIKE '%int%'
				AND `COLUMN_KEY` = 'PRI';
		SET SQL_EXP = CONCAT('ALTER TABLE `', (SELECT IFNULL(SCHEMA_NAME_ARGUMENT, SCHEMA())), '`.`', TABLE_NAME_ARGUMENT, '` MODIFY COLUMN `', PRIMARY_KEY_COLUMN_NAME, '` ', PRIMARY_KEY_TYPE, ' NOT NULL AUTO_INCREMENT;');
		SET @SQL_EXP = SQL_EXP;
		PREPARE SQL_EXP_EXECUTE FROM @SQL_EXP;
		EXECUTE SQL_EXP_EXECUTE;
		DEALLOCATE PREPARE SQL_EXP_EXECUTE;
	END IF;
END //
DELIMITER ;

CALL POMELO_BEFORE_DROP_PRIMARY_KEY(NULL, 'DopMaskRecords');
ALTER TABLE `DopMaskRecords` DROP PRIMARY KEY;

ALTER TABLE `DopMaskRecords` DROP COLUMN `IsSucess`;

ALTER TABLE `DopMaskRecords` RENAME `DOP_MASK_RECORD`;

ALTER TABLE `DOP_MASK_RECORD` ADD `IsSuccess` tinyint(1) NOT NULL DEFAULT FALSE;

ALTER TABLE `DOP_MASK_RECORD` ADD CONSTRAINT `PK_DOP_MASK_RECORD` PRIMARY KEY (`Id`);
CALL POMELO_AFTER_ADD_PRIMARY_KEY(NULL, 'DOP_MASK_RECORD', 'Id');

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210401082151_v1.5.377', '3.1.7');

DROP PROCEDURE `POMELO_BEFORE_DROP_PRIMARY_KEY`;

DROP PROCEDURE `POMELO_AFTER_ADD_PRIMARY_KEY`;

ALTER TABLE `DOP_MASK_RECORD` ADD `SequenceId` longtext CHARACTER SET utf8mb4 NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210406060702_v1.5.378', '3.1.7');

