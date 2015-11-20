using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace _04._2_Asynchronous
{
    internal class Program
    {
        public delegate string AsyncMethodCaller(int callDuration, string input, out string threadId);

        private static void Main()
        {
            var ad = new Async();

            AsyncMethodCaller caller = ad.TestMethod;

            var dummy = "Main+";

            var result = caller.BeginInvoke(3000,
                "Main+",
                out dummy,
                CallbackMethod,
                "The call executed on thread {0}, with return value \"{1}\".");

            Console.WriteLine("The main thread {0} continues to execute...",
                Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(4000);

            Console.WriteLine("The main thread ends.");
            Console.ReadLine();
        }


        private static void CallbackMethod(IAsyncResult ar)
        {
            var result = (AsyncResult) ar;
            var caller = (AsyncMethodCaller) result.AsyncDelegate;

            var formatString = (string) ar.AsyncState;

            var threadId = string.Empty;

            var returnValue = caller.EndInvoke(out threadId, ar);

            Console.WriteLine(formatString, threadId, returnValue);
        }
    }
}