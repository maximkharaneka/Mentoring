using System;
using System.Threading;

namespace _05._2_TPL
{
    public class Async
    {
        public string TestMethod(int callDuration, string input, out string threadId)
        {
            Console.WriteLine("Test method "+input+" begins.");
            Thread.Sleep(callDuration);
            threadId = input + Thread.CurrentThread.ManagedThreadId;
            return string.Format("My call " + input + " time was {0}.", callDuration);
        }
    }
}