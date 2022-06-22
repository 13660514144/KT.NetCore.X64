declare @TimeMin varchar(200)
set @TimeMin = '2020-02-13 15:25:00.000'
declare @TimeMax varchar(200)
set @TimeMax = '2020-02-13 15:41:00.000'

SELECT * FROM AccessLevelPlus              WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM AccessLevelReport            WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax
SELECT * FROM AccountMap                   WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax


SELECT * FROM AccessLevelEntrancePlus      WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax           
SELECT * FROM AccessLevelEntrances         WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM AccessLevelEntrancesReport   WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM AccessLevelPanel             WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM AccessLevelPlus              WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM AccessLevelReport            WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM AccessTreeEx                 WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM Account                      WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM AccountMap                   WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM ActionGroup                  WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM ActionMessage                WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM ActiveXdefaults              WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
--SELECT * FROM AttachedTimezones            WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM BadgeData                    WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM BadgeHeader                  WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM Card                         WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM CardAccessLevels             WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM CardEx                       WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM CardHolder                   WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM CardHolderReport             WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM CardHolderUserCodes          WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM CardReport                   WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM CmdFile                      WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM CmdFileReport                WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM Command                      WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM ControlTreeEx                WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM CustomAlchangedEntrances     WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM CustomAlremovedEntrances     WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM DaylightSaving               WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM DaylightSavingGroup          WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM Dbchanges                    WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM Device                       WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM DeviceAdvs                   WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM DeviceReport                 WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM Finuser                      WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM FloorPlan                    WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM FloorPlanItem                WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM FloorPlanReport              WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
--SELECT * FROM GroupAdvs                    WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM GuardTour                    WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM GuardTourCheckPoints         WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM GuardTourReport              WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM Header                       WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM Hidquery                     WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM History                      WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM HistoryReport                WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM Holiday                      WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM HolidayGroup                 WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM HwindependentDevices         WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM NftabLayout                  WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM NoteFieldTemplate            WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM Operator                     WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
--SELECT * FROM OperatorActionValues         WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM OperatorLevel                WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM OperatorLevelDb              WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM OperatorLevelDeviceEx        WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM OperatorLevelUi              WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM OperatorReport               WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM PanelLog                     WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM PanelLogEx                   WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM PanelTimeZone                WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM ReportSeg                    WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM ReportTemplate               WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM Reports                      WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM Schedule                     WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM ScheduleReport               WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM SystemConfig                 WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
--SELECT * FROM SystemOperators              WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM TimeZone                     WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM TimeZoneRange                WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM TimezoneReport               WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
SELECT * FROM TrackingTree                 WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax 
--SELECT * FROM Trailer                      WHERE @TimeMin < TimeStamp AND TimeStamp < @TimeMax                          

