namespace Mentoring
{
    using System;
    using System.Threading;

    internal class Program
    {
        public static void Beta()
        {
            Console.WriteLine("Thread " + Thread.CurrentThread.ManagedThreadId + " started.");
            Thread.Sleep(200);
            var oThread = new Thread(Beta);
            oThread.Start();
            Console.WriteLine("Thread " + Thread.CurrentThread.ManagedThreadId + " run subthread.");
            Thread.Sleep(1000);
            Console.WriteLine("Thread " + Thread.CurrentThread.ManagedThreadId + " finished.");
        }

        public static int Main()
        {
            Console.WriteLine("Thread Start/Stop/Join Sample");

            var oThread = new Thread(Beta);
            oThread.Start();

            Console.WriteLine("Program finished");
            Console.ReadLine();
            return 0;
        }
    }
}