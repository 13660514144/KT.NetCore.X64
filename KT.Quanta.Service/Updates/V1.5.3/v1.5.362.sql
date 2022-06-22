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

