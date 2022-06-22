﻿ALTER TABLE `HANDLE_ELEVATOR_DEVICE_AUXILIARY` MODIFY COLUMN `HandleElevatorDeviceId` varchar(255) CHARACTER SET utf8mb4 NULL;

CREATE UNIQUE INDEX `IX_HANDLE_ELEVATOR_DEVICE_AUXILIARY_HandleElevatorDeviceId` ON `HANDLE_ELEVATOR_DEVICE_AUXILIARY` (`HandleElevatorDeviceId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210321140233_v1.5.370', '3.1.7');
