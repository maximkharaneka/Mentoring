using System;
using System.Runtime.InteropServices;
namespace TaskUnmanaged
{
    [ComVisible(true)]
    [Guid("69E39A4B-7106-41A6-B5CF-3A6FA0B4E6D5")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IPowerProfileLib
    {
        ulong LastSleepTime();
        ulong LastWakeTime();
        bool SetSuspendState();
        string SystemBatteryState();
        string SystemPowerInfo();
        ulong SystemReserveHiberFile(bool Restore);
    }
}