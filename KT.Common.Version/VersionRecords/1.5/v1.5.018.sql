﻿
ALTER TABLE `CARD_DEVICE` DROP COLUMN `DeviceKey`;
ALTER TABLE `PROCESSOR` DROP COLUMN `DeviceKey`, DROP INDEX `IX_PROCESSOR_DeviceKey`;
