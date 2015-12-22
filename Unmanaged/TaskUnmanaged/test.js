liba = new ActiveXObject("TaskUnmanaged.PowrProfLibaCall");


WScript.Echo(liba.LastSleepTime());

WScript.Echo(liba.LastWakeTime());
WScript.Echo(liba.SystemReserveHiberFile(true));
WScript.Echo(liba.SystemReserveHiberFile(false));

WScript.Echo(liba.SystemBatteryState());

WScript.Echo(liba.SystemPowerInfo());
WScript.Echo(liba.SetSuspendState())