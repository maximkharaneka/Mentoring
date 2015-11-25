using System;
using System.Threading;

namespace _03._2_Synchronization
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var createdNew = true;
            using (var mutex = new Mutex(true, "Synchronization", out createdNew))
            {
                if (createdNew)
                {
                    Console.WriteLine("Runned Successfuly");
                    Console.ReadLine();
                    mutex.ReleaseMutex();
                }
                else
                {
                    Console.WriteLine("Already runned");
                    // Console.ReadLine();
                }
                
            }
        }
    }
}