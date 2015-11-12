using System;
using System.Threading;

namespace _02Threads
{
    internal class Drawer
    {
        private static readonly Mutex mut = new Mutex();

        private readonly Random rand = new Random();

        // setup array of starting y values
        private readonly int[] y;

        public int width, height;

        public Drawer()
        {
            this.height = Console.WindowHeight;
            this.width = Console.WindowWidth - 1;
            this.y = new int[this.width];

            Console.Clear();
            for (var x = 0; x < this.width; ++x)
            {
                this.y[x] = this.rand.Next(this.height);
            }
        }

        private char AsciiCharacter
        {
            get
            {
                var t = this.rand.Next(10);
                if (t <= 2)
                {
                    // returns a number
                    return (char)('0' + this.rand.Next(10));
                }
                if (t <= 4)
                {
                    // small letter
                    return (char)('a' + this.rand.Next(27));
                }
                if (t <= 6)
                {
                    // capital letter
                    return (char)('A' + this.rand.Next(27));
                }
                return (char)(this.rand.Next(32, 255));
            }
        }

        public void ParameterizedThreadStart(object obj)
        {
            mut.WaitOne();
            this.UpdateColumn((int)obj);
            mut.ReleaseMutex();
        }

        public void UpdateColumn(int x)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(x, this.y[x]);
            Console.Write(this.AsciiCharacter);

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            var temp = this.y[x] - 2;
            Console.SetCursorPosition(x, this.inScreenYPosition(temp, this.height));
            Console.Write(this.AsciiCharacter);

            var temp1 = this.y[x] - 20;
            Console.SetCursorPosition(x, this.inScreenYPosition(temp1, this.height));
            Console.Write(' ');

            this.y[x] = this.inScreenYPosition(this.y[x] + 1, this.height);
        }

        public int inScreenYPosition(int yPosition, int height)
        {
            if (yPosition < 0)
            {
                return yPosition + height;
            }
            if (yPosition < height)
            {
                return yPosition;
            }
            return 0;
        }
    }
}