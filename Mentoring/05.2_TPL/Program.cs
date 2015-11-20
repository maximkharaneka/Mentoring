using System;
using System.Threading.Tasks;

namespace _05._2_TPL
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var ad = new Async();
            Task task = new Task(() => Parallel.Invoke(
                () =>
                {
                    Console.WriteLine("Begin first task...");
                    string output;
                    ad.TestMethod(2000, "First", out output);
                },
                () =>
                {
                    Console.WriteLine("Begin second task...");
                    string output;
                    ad.TestMethod(1000, "Second", out output);
                }
                ));

            task.Start();
           
            Console.WriteLine("Main thread finished");
            task.Wait();
            Console.WriteLine("Tasks waited");
            Console.ReadLine();
        }
    }
}