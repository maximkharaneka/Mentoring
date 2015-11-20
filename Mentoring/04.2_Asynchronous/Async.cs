using System;
using System.Threading;

namespace _04._2_Asynchronous
{
    public class Async
    {
        public string TestMethod(int callDuration, string input, out string threadId)
        {
            Console.WriteLine("Test method begins.");
            Thread.Sleep(callDuration);
            threadId = input + Thread.CurrentThread.ManagedThreadId;
            return string.Format("My call time was {0}.", callDuration);
        }
    }
}