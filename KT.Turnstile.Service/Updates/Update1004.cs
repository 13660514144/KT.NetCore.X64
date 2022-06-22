using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Turnstile.Manage.Service.Updates
{
    public class Update1004
    {
        /*
    
        ALTER TABLE `carddevice` RENAME TO `card_device` ;
        ALTER TABLE `carddevicerightgroups` RENAME TO `card_device_right_group` ;
        ALTER TABLE `carddevicerightgrouprelationcarddevices` RENAME TO `card_device_right_group_relation_card_device` ;
        ALTER TABLE `distributeerrors` RENAME TO `distribute_error` ;
        ALTER TABLE `loginuser` RENAME TO `login_user` ;
        ALTER TABLE `passrecord` RENAME TO `pass_record` ;
        ALTER TABLE `passright` RENAME TO `pass_right` ;
        ALTER TABLE `passrightrelationcarddevicerightgroups` RENAME TO `pass_right_relation_card_device_right_group` ;
        ALTER TABLE `relaydevice` RENAME TO `relay_device` ;
        ALTER TABLE `serialconfig` RENAME TO `serial_config` ;
        ALTER TABLE `systemconfig` RENAME TO `system_config` ;
         
        DROP TABLE `passaisles`;
        DROP TABLE `passrightrelationcarddevices`;
        DROP TABLE `relaydeviceconfigs`;
        DROP TABLE `serialreceivedata`;
        
        DROP TABLE IF EXISTS `third_server`;
        CREATE TABLE `third_server` (
          `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
          `Creator` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
          `Editor` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
          `CreatedTime` bigint NOT NULL,
          `EditedTime` bigint NOT NULL,
          `DBAddr` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
          `DBName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
          `DBUser` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
          `DBPassword` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
          `PCAddr` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
          `PCUser` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
          `PCPassword` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
          `PCPort` int NOT NULL,
          `ServerType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
          PRIMARY KEY(`Id`)
        ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE = utf8mb4_0900_ai_ci;


        ALTER TABLE `card_device` 
            DROP COLUMN `OwnerTime`,
            ADD COLUMN `HandleElevatorDeviceId` LONGTEXT NULL AFTER `SerialConfigId`;

        ALTER TABLE `card_device_right_group` 
            DROP COLUMN `OwnerTime`;

        ALTER TABLE `card_device_right_group_relation_card_device` 
            ADD COLUMN `Creator` LONGTEXT NULL DEFAULT NULL AFTER `CardDeviceId`,
            ADD COLUMN `Editor` LONGTEXT NULL DEFAULT NULL AFTER `Creator`,
            ADD COLUMN `CreatedTime` BIGINT NULL DEFAULT 0 AFTER `Editor`,
            ADD COLUMN `EditedTime` BIGINT NULL DEFAULT 0 AFTER `CreatedTime`;

        ALTER TABLE `distribute_error` 
            DROP COLUMN `OwnerTime`;

        ALTER TABLE `login_user` 
            DROP COLUMN `OwnerTime`;

        ALTER TABLE `pass_record` 
            DROP COLUMN `OwnerTime`;

        ALTER TABLE `pass_right` 
            DROP COLUMN `OwnerTime`;

        ALTER TABLE `pass_right_relation_card_device_right_group` 
            DROP COLUMN `OwnerTime`;

        ALTER TABLE `processor` 
            DROP COLUMN `OwnerTime`;

        ALTER TABLE `relay_device` 
            DROP COLUMN `OwnerTime`;

        ALTER TABLE `serial_config` 
            DROP COLUMN `OwnerTime`;

        ALTER TABLE `system_config` 
            DROP COLUMN `OwnerTime`;


                 */
    }
}





















