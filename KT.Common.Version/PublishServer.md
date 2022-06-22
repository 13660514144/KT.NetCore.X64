New-Service -Name KT.Quanta.WebApi -BinaryPathName D:\QuantaPublish\quanta-web-api-win-x86-stand\KT.Quanta.WebApi.exe -Description "QuantaData 设备通信服务，请务关闭，否则相关程序无法运行！" -DisplayName "QuantaData 设备通信服务" -StartupType Automatic

New-Service -Name KT.Prowatch.WebApi -BinaryPathName D:\QuantaPublish\prowatch-web-api-win-x86-stand\KT.Prowatch.WebApi.exe -Description "Prowatch 门禁服务，请务关闭，否则相关程序无法运行！" -DisplayName "Prowatch 门禁服务" -StartupType Automatic

New-Service -Name KT.WinPak.WebApi.V44 -BinaryPathName D:\QuantaPublish\winpak-web-api-v44-win-x86-stand\KT.WinPak.WebApi.V44.exe -Description "WinPakV44 门禁服务，请务关闭，否则相关程序无法运行！" -DisplayName "WinPakV44 门禁服务" -StartupType Automatic

New-Service -Name KT.WinPak.WebApi.V48 -BinaryPathName D:\QuantaPublish\winpak-web-api-v48-win-x86-stand\KT.WinPak.WebApi.V48.exe -Description "WinPakV48 门禁服务，请务关闭，否则相关程序无法运行！" -DisplayName "WinPakV48 门禁服务" -StartupType Automatic
