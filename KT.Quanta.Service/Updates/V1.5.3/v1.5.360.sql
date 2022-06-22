ALTER TABLE `pass_right_relation_card_device_right_group` 
DROP FOREIGN KEY `FK_PASS_RIGHT_RELATION_CARD_DEVICE_RIGHT_GROUP_CARD_DEVICE_RIGH~`,
DROP FOREIGN KEY `FK_PASS_RIGHT_RELATION_CARD_DEVICE_RIGHT_GROUP_PASS_RIGHT_PassR~`;
ALTER TABLE `pass_right_relation_card_device_right_group` 
CHANGE COLUMN `PassRightId` `PassRightId` VARCHAR(255) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL ,
CHANGE COLUMN `CardDeviceRightGroupId` `CardDeviceRightGroupId` VARCHAR(255) CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NOT NULL ;
ALTER TABLE `pass_right_relation_card_device_right_group` 
ADD CONSTRAINT `FK_PASS_RIGHT_RELATION_CARD_DEVICE_RIGHT_GROUP_CARD_DEVICE_RIGH~`
  FOREIGN KEY (`CardDeviceRightGroupId`)
  REFERENCES `card_device_right_group` (`Id`)
  ON DELETE CASCADE,
ADD CONSTRAINT `FK_PASS_RIGHT_RELATION_CARD_DEVICE_RIGHT_GROUP_PASS_RIGHT_PassR~`
  FOREIGN KEY (`PassRightId`)
  REFERENCES `pass_right` (`Id`)
  ON DELETE CASCADE;




ALTER TABLE `floor` 
ADD COLUMN `Direction` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL AFTER `EdificeId`;

ALTER TABLE `pass_right` 
ADD COLUMN `Direction` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL AFTER `FloorId`;

ALTER TABLE `pass_right_relation_floor` 
ADD COLUMN `Direction` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL AFTER `FloorId`;


ALTER TABLE `handle_elevator_device` ADD COLUMN `ProcessorId` VARCHAR(255) NULL DEFAULT NULL AFTER `ElevatorGroupId`;
ALTER TABLE `handle_elevator_device` ADD CONSTRAINT `FK_HANDLE_ELEVATOR_DEVICE_PROCESSOR_ProcessorId` FOREIGN KEY (`ProcessorId`) REFERENCES `processor` (`Id`);

ALTER TABLE `pass_record` 
ADD COLUMN `Temperature` DECIMAL(65,30) NULL DEFAULT NULL AFTER `Remark`,
ADD COLUMN `IsMask` TINYINT(1) NULL DEFAULT NULL AFTER `Temperature`;

ALTER TABLE `distribute_error` 
CHANGE COLUMN `DeviceKey` `DeviceId` LONGTEXT CHARACTER SET 'utf8mb4' COLLATE 'utf8mb4_0900_ai_ci' NULL DEFAULT NULL ;

ALTER TABLE `processor` 
DROP COLUMN `DeviceKey`,
DROP INDEX `IX_PROCESSOR_DeviceKey` ;

ALTER TABLE `card_device` 
DROP COLUMN `DeviceKey`,
DROP INDEX `IX_CARD_DEVICE_DeviceKey` ;

CREATE TABLE `pass_right_floor_direction` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Creator` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Editor` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `CreatedTime` bigint(20) NOT NULL,
  `EditedTime` bigint(20) NOT NULL,
  `Sign` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `FloorId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Direction` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_PASS_RIGHT_FLOOR_DIRECTION_SIGN_FloorId` (`Sign`,`FloorId`),
  CONSTRAINT `FK_PASS_RIGHT_FLOOR_DIRECTION_FLOOR_FloorId` FOREIGN KEY(`FloorId`) REFERENCES `floor` (`Id`) ON DELETE CASCADE
) ENGINE = InnoDB DEFAULT CHARSET = utf8mb4 COLLATE=utf8mb4_0900_ai_ci;



DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

LOCK TABLES `__efmigrationshistory` WRITE;
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` VALUES ('20210310055806_v1.5.360','3.1.7');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;
UNLOCK TABLES;




SET SQL_SAFE_UPDATES = 0;
UPDATE floor SET IsFront = 1; 
