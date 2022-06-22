CREATE TABLE `ELI_OPEN_ACCESS_FOR_DOP_MESSAGE_TYPE` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `PassRightSign` longtext CHARACTER SET utf8mb4 NULL,
    `HandleElevatorDeviceId` longtext CHARACTER SET utf8mb4 NULL,
    `MessageType` int NOT NULL,
    CONSTRAINT `PK_ELI_OPEN_ACCESS_FOR_DOP_MESSAGE_TYPE` PRIMARY KEY (`Id`)
);

CREATE TABLE `ELI_PASS_RIGHT_HANDLE_ELEVATOR_DEVICE_CALL_TYPE` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `PassRightSign` longtext CHARACTER SET utf8mb4 NULL,
    `HandleElevatorDeviceId` longtext CHARACTER SET utf8mb4 NULL,
    `CallType` int NOT NULL,
    CONSTRAINT `PK_ELI_PASS_RIGHT_HANDLE_ELEVATOR_DEVICE_CALL_TYPE` PRIMARY KEY (`Id`)
);

CREATE TABLE `RCGIF_PASS_RIGHT_HANDLE_ELEVATOR_DEVICE_CALL_TYPE` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Creator` longtext CHARACTER SET utf8mb4 NULL,
    `Editor` longtext CHARACTER SET utf8mb4 NULL,
    `CreatedTime` bigint NOT NULL,
    `EditedTime` bigint NOT NULL,
    `PassRightSign` longtext CHARACTER SET utf8mb4 NULL,
    `HandleElevatorDeviceId` longtext CHARACTER SET utf8mb4 NULL,
    `CallType` int NOT NULL,
    CONSTRAINT `PK_RCGIF_PASS_RIGHT_HANDLE_ELEVATOR_DEVICE_CALL_TYPE` PRIMARY KEY (`Id`)
);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210424082331_v1.5.408', '3.1.7');

