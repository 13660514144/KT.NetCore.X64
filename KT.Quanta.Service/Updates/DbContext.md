## 数据库更新
```
Add-Migration v1.5.603 -Context QuantaDbContext -v

Update-Database -Context QuantaDbContext -v

Script-Migration v1.5.602 -Output KT.Quanta.Service/Updates/V1.5.6/v1.5.603.sql -Context QuantaDbContext -v

Script-Migration -Output KT.Quanta.Service/Updates/V1.5.6/last.sql -Context QuantaDbContext -v



Script-Migration v1.5.360 -Context QuantaDbContext -v

```