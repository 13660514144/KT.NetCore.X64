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

