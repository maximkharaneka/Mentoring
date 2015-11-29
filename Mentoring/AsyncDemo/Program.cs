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
            CancellationTokenSource source = null;
            Task<int> task = null;

            while (!isCalculated)
            {
                Console.Write("Input n:");
                var input = Console.ReadLine();
                int n;
                int.TryParse(input, out n);

                if (source != null)
                {
                    source.Cancel();
                }

                source = new CancellationTokenSource();

                if (!isCalculated)
                {
                    task = GetNSum(n, source.Token);
                    task
                        .ContinueWith(x => isCalculated = true, TaskContinuationOptions.NotOnCanceled);
                }
            }
        }

        private static async Task<int> GetNSum(int n, CancellationToken cancellationToken)
        {
            await Task.Delay(1);
            var result = 0;
            try
            {
                for (var i = 0; i < n; i++)
                {
                    Thread.Sleep(100);
                    result += i;
                    if (i%10 == 0)
                        cancellationToken.ThrowIfCancellationRequested();
                }
                Console.WriteLine();
                Console.WriteLine($"The result is {result} for 0-{n} ");
            }
            catch (Exception)
            {
                Console.WriteLine("Previous process was canceled");
            }
            return result;
        }
    }
}