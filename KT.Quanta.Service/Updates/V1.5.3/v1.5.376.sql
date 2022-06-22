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

