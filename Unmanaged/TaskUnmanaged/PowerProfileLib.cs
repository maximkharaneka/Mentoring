using System;
using System.Runtime.InteropServices;

namespace TaskUnmanaged
{
    public struct SYSTEM_BATTERY_STATE
    {
        public int AcOnLine;
        public int BatteryPresent;
        public int Charging;
        public int Discharging;
        public int Spare1;
        public uint MaxCapacity;
        public uint RemainingCapacity;
        public uint Rate;
        public uint EstimatedTime;
        public uint DefaultAlert1;
        public uint DefaultAlert2;
    }

    public struct SYSTEM_POWER_INFORMATION
    {
        public uint MaxIdlenessAllowed;
        public uint Idleness;
        public uint TimeRemaining;
        public byte CoolingMode;
    }

    [ComVisible(true)]
    [Guid("8E2C74B2-8B52-4C12-8FCF-23F86DE02EE4")]
    [ClassInterface(ClassInterfaceType.None)]
    public class PowerProfileLib : IPowerProfileLib
    {
        ulong IPowerProfileLib.LastSleepTime()
        {
            ulong LastSleepTime;
            var status = CallNtPowerInformation(15, IntPtr.Zero, 0, out LastSleepTime, Marshal.SizeOf(typeof (ulong)));
            if (status == 0)
            {
                return LastSleepTime;
            }
            return status;
        }

        ulong IPowerProfileLib.LastWakeTime()
        {
            ulong LastWakeTime;

            var status = CallNtPowerInformation(14, IntPtr.Zero, 0, out LastWakeTime, Marshal.SizeOf(typeof (ulong)));
            if (status == 0)
            {
                return LastWakeTime;
            }
            return status;
        }

        string IPowerProfileLib.SystemBatteryState()
        {
            SYSTEM_BATTERY_STATE SystemBatteryState;
            var status = CallNtPowerInformation(
                5,
                IntPtr.Zero,
                0,
                out SystemBatteryState,
                Marshal.SizeOf(typeof (SYSTEM_BATTERY_STATE))
                );

            return string.Format(@"AcOnLine {0}, 
                BatteryPresent {1}, 
                Charging {2}, 
                Discharging {3},
                Spare1 {4}, 
                MaxCapacity {5}, 
                RemainingCapacity {6}, 
                Rate {7}, 
                EstimatedTime {8}, 
                DefaultAlert1 {9}, 
                DefaultAlert2 {10}",
                SystemBatteryState.AcOnLine,
                SystemBatteryState.BatteryPresent,
                SystemBatteryState.Charging,
                SystemBatteryState.Discharging,
                SystemBatteryState.Spare1,
                SystemBatteryState.MaxCapacity,
                SystemBatteryState.RemainingCapacity,
                SystemBatteryState.Rate,
                SystemBatteryState.EstimatedTime,
                SystemBatteryState.DefaultAlert1,
                SystemBatteryState.DefaultAlert2);
        }

        string IPowerProfileLib.SystemPowerInfo()
        {
            SYSTEM_POWER_INFORMATION SystemPowerInfo;
            var status = CallNtPowerInformation(
                12,
                IntPtr.Zero,
                0,
                out SystemPowerInfo,
                Marshal.SizeOf(typeof (SYSTEM_POWER_INFORMATION))
                );
            return string.Format(@"MaxIdlenessAllowed {0}, 
                Idleness {1}, 
                TimeRemaining {2}, 
                CoolingMode {3}",
                SystemPowerInfo.MaxIdlenessAllowed,
                SystemPowerInfo.Idleness,
                SystemPowerInfo.TimeRemaining,
                SystemPowerInfo.CoolingMode);
        }

        ulong IPowerProfileLib.SystemReserveHiberFile(bool Restore)
        {
            ulong SystemReserveHiberFile;
            var status = CallNtPowerInformation(10, Restore, Marshal.SizeOf(typeof (bool)), out SystemReserveHiberFile,
                Marshal.SizeOf(typeof (ulong)));
            if (status == 0)
            {
                return SystemReserveHiberFile;
            }
            return status;
        }

        bool IPowerProfileLib.SetSuspendState()
        {
            // Sleeps the machine
            return SetSuspendState(false, false, false);
        }

        /*
        NTSTATUS WINAPI CallNtPowerInformation(
  _In_  POWER_INFORMATION_LEVEL InformationLevel,
  _In_  PVOID                   lpInputBuffer,
  _In_  ULONG                   nInputBufferSize,
  _Out_ PVOID                   lpOutputBuffer,
  _In_  ULONG                   nOutputBufferSize
);

            */


        /*
        LastSleepTime
15
The lpInBuffer parameter must be NULL; otherwise, the function returns ERROR_INVALID_PARAMETER.
The lpOutputBuffer buffer receives a ULONGLONG that specifies the interrupt-time count, 
in 100-nanosecond units, at the last system sleep time.


            LastWakeTime
14
The lpInBuffer parameter must be NULL; otherwise, the function returns ERROR_INVALID_PARAMETER.
The lpOutputBuffer buffer receives a ULONGLONG that specifies the interrupt-time count, 
in 100-nanosecond units, at the last system wake time.
            */

        [DllImport("PowrProf.dll")]
        public static extern uint CallNtPowerInformation(
            int InformationLevel,
            IntPtr lpInputBuffer,
            int nInputBufferSize,
            out ulong lpOutputBuffer,
            int nOutputBufferSize);

        /*
        SystemBatteryState
        5
        The lpInBuffer parameter must be NULL; otherwise, the function returns ERROR_INVALID_PARAMETER.
        The lpOutputBuffer buffer receives a SYSTEM_BATTERY_STATE structure containing information about the current system battery.

            typedef struct {
      BOOLEAN AcOnLine;
      BOOLEAN BatteryPresent;
      BOOLEAN Charging;
      BOOLEAN Discharging;
      BOOLEAN Spare1[4];
      DWORD   MaxCapacity;
      DWORD   RemainingCapacity;
      DWORD   Rate;
      DWORD   EstimatedTime;
      DWORD   DefaultAlert1;
      DWORD   DefaultAlert2;
    } SYSTEM_BATTERY_STATE, *PSYSTEM_BATTERY_STATE;    

            */

        [DllImport("PowrProf.dll")]
        public static extern uint CallNtPowerInformation(
            int InformationLevel,
            IntPtr lpInputBuffer,
            int nInputBufferSize,
            out SYSTEM_BATTERY_STATE sbs,
            int nOutputBufferSize);

        [DllImport("PowrProf.dll")]
        public static extern uint CallNtPowerInformation(
            int InformationLevel,
            bool lpInputBuffer,
            int nInputBufferSize,
            out ulong lpOutputBuffer,
            int nOutputBufferSize);

        /*
        SystemPowerInformation
12
The lpInBuffer parameter must be NULL; otherwise, the function returns ERROR_INVALID_PARAMETER.
The lpOutputBuffer buffer receives a SYSTEM_POWER_INFORMATION structure.
Applications can use this level to retrieve information about the idleness of the system.

        typedef struct _SYSTEM_POWER_INFORMATION {
  ULONG MaxIdlenessAllowed;
  ULONG Idleness;
  ULONG TimeRemaining;
  UCHAR CoolingMode;
} SYSTEM_POWER_INFORMATION, *PSYSTEM_POWER_INFORMATION;
            */

        [DllImport("powrprof.dll")]
        private static extern uint CallNtPowerInformation(
            int InformationLevel,
            IntPtr lpInputBuffer,
            int nInputBufferSize,
            out SYSTEM_POWER_INFORMATION spi,
            int nOutputBufferSize
            );


        /// <summary>
        ///     Suspends the system by shutting power down. Depending on the Hibernate parameter, the system either enters a
        ///     suspend (sleep) state or hibernation (S4).
        /// </summary>
        /// <param name="hibernate">
        ///     If this parameter is TRUE, the system hibernates. If the parameter is FALSE, the system is
        ///     suspended.
        /// </param>
        /// <param name="forceCritical">
        ///     Windows Server 2003, Windows XP, and Windows 2000:  If this parameter is TRUE,
        ///     the system suspends operation immediately; if it is FALSE, the system broadcasts a PBT_APMQUERYSUSPEND event to
        ///     each
        ///     application to request permission to suspend operation.
        /// </param>
        /// <param name="disableWakeEvent">
        ///     If this parameter is TRUE, the system disables all wake events. If the parameter is
        ///     FALSE, any system wake events remain enabled.
        /// </param>
        /// <returns>If the function succeeds, the return value is true.</returns>
        [DllImport("Powrprof.dll", SetLastError = true)]
        private static extern bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);
    }
}