using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskUnmanaged;

namespace Sample01_UnmanagedFunctions
{
    [TestClass]
    public class SimpleUnmanagedFunctionCall
    {
        [TestMethod]
        public void TestMethod1()
        {
            var powrProfLib = (IPowerProfileLib) new PowerProfileLib();

            Console.WriteLine(
                powrProfLib.LastSleepTime());
            Console.WriteLine(
                powrProfLib.LastWakeTime());
            Console.WriteLine(
                powrProfLib.SystemReserveHiberFile(true));
            Console.WriteLine(
                powrProfLib.SystemReserveHiberFile(false));

            Console.WriteLine(powrProfLib.SystemBatteryState());

            Console.WriteLine(powrProfLib.SystemPowerInfo());
        }
    }
}