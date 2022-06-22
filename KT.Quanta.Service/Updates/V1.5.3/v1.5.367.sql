ALTER TABLE `PASS_RIGHT_AUXILIARY` DROP COLUMN `HandleElevatorDeviceId`;

ALTER TABLE `PASS_RIGHT_AUXILIARY` ADD `ElevatorGroupId` longtext CHARACTER SET utf8mb4 NULL;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210319033756_v1.5.368', '3.1.7');

