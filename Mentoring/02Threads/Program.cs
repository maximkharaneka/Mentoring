using System;
using System.Threading;

namespace _02Threads
{
    internal class Program
    {
        // methods
        private static void Main()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WindowLeft = Console.WindowTop = 0;
            Console.WindowHeight = Console.BufferHeight = Console.LargestWindowHeight;
            Console.WindowWidth = Console.BufferWidth = Console.LargestWindowWidth;
            Console.WriteLine("Hit Any Key To Continue");
            Console.ReadKey();
            Console.CursorVisible = false;

            var drawer = new Drawer();

            while (true)
            {
                UpdateAllColumns(drawer);
            }
        }

        private static void UpdateAllColumns(Drawer drawer)
        {
            int x;

            for (x = 0; x < drawer.width; ++x)
            {
                var oThread = new Thread(drawer.ParameterizedThreadStart);
                oThread.Start(x);
                //                drawer.ParameterizedThreadStart(x);
            }
        }
    }
}