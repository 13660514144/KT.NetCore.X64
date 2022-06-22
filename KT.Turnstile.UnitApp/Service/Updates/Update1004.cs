using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Turnstile.Unit.ClientApp.Service.Updates
{
    public class Update1004
    {
        /*
         
        ALTER TABLE PassRecord RENAME TO PASS_RECORD;
        ALTER TABLE PassRight RENAME TO PASS_RIGHT;
        ALTER TABLE PassRightDetail RENAME TO PASS_RIGHT_DETAIL;
        ALTER TABLE RightGroup RENAME TO RIGHT_GROUP;
        ALTER TABLE RightGroupDetail RENAME TO RIGHT_GROUP_DETAIL;
        ALTER TABLE SystemConfig RENAME TO SYSTEM_CONFIG;

        CREATE TABLE [CARD_DEVICE](
          [Id] TEXT CONSTRAINT [PK_CARD_DEVICE] PRIMARY KEY NOT NULL, 
          [Creator] TEXT, 
          [Editor] TEXT, 
          [CreatedTime] INTEGER NOT NULL, 
          [EditedTime] INTEGER NOT NULL, 
          [ProductType] TEXT, 
          [DeviceType] TEXT, 
          [PortName] TEXT, 
          [Baudrate] INTEGER NOT NULL, 
          [Databits] INTEGER NOT NULL, 
          [Stopbits] INTEGER NOT NULL, 
          [Parity] INTEGER NOT NULL, 
          [ReadTimeout] INTEGER NOT NULL, 
          [Encoding] TEXT, 
          [RelayCommunicateType] TEXT, 
          [RelayDeviceIp] TEXT, 
          [RelayDevicePort] INTEGER NOT NULL, 
          [RelayDeviceOut] INTEGER NOT NULL, 
          [RelayOpenCmd] TEXT, 
          [HandleElevatorDeviceId] TEXT);
         
        INSERT INTO [CARD_DEVICE] 
        SELECT
          "Id",
          "Creator",
          "Editor",
          "CreatedTime",
          "EditedTime",
          "Type",
          "CardType",
          "PortName",
          "Baudrate",
          "Databits",
          "Stopbits",
          "Parity",
          "ReadTimeout",
          "Encoding",
          "RelayCommunicateType",
          "RelayDeviceIp",
          "RelayDevicePort",
          "RelayDeviceOut",
          "RelayOpenCmd",
          NULL 
        FROM "CardDevice";

        DROP TABLE CardDevice;

                 */
    }
}





















