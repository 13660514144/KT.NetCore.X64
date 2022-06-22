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

