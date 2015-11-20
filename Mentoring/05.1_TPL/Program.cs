using System;
using System.Linq;

namespace _05._1_TPL
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var rand = new Random();
            var numbers = new int[1000000];

            for (var i = 0; i < numbers.Length; i++)
            {
                numbers[i] = rand.Next(1000000);
            }

            var output = numbers.AsParallel().Where(x => x%2 == 0).ToList();

            output.ForEach(x => Console.WriteLine(x));
        }
    }
}