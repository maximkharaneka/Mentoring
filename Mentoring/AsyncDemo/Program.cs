using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var isCalculated = false;
            var isTaskRunned = false;
            var source = new CancellationTokenSource();
            while (!isCalculated)
            {
                Console.WriteLine("Input n:");
                var input = Console.ReadLine();
                int n;
                int.TryParse(input, out n);
                if (isTaskRunned)
                {
                    source.Cancel();
                }
               
                if (!isCalculated && !isTaskRunned)
                {
                    isTaskRunned = true;
                    var task =
                        Task.Run(() => GetNSum(n, source.Token), source.Token)
                            .ContinueWith(x => isCalculated = true, source.Token)
                            .ContinueWith(x => source = new CancellationTokenSource())
                            .ContinueWith(x=>isTaskRunned=false);
                }
            }
            Console.WriteLine("Calculated!");
        }

        private async Task TryTask()
        {
            var source = new CancellationTokenSource();
            source.CancelAfter(TimeSpan.FromSeconds(1));
            var task = Task.Run(() => GetNSum(1000, source.Token), source.Token);

            // (A canceled task will raise an exception when awaited).
            await task;
        }

        private static int GetNSum(int n, CancellationToken cancellationToken)
        {
            var result = 0;
            for (var i = 0; i < n; i++)
            {
                Thread.Sleep(100);
                result += i;
                if (i%100 == 0)
                    cancellationToken.ThrowIfCancellationRequested();
            }
            Console.WriteLine();
            Console.WriteLine(result);
            return result;
        }
    }
}