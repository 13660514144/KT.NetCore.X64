
```
Add-Migration v1.5.371 -Context ElevatorUnitDbContext -v
 
Update-Database -Context ElevatorUnitDbContext -v

Script-Migration v1.5.368 -Output KT.Elevator.Unit.Processor.ClientApp/Updates/V1.5.3/v1.5.371.sql -Context ElevatorUnitDbContext -v
Script-Migration -Output KT.Elevator.Unit.Processor.ClientApp/Updates/V1.5.3/last.sql -Context ElevatorUnitDbContext -v

```