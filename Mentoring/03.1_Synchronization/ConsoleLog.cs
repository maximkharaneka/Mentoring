using System;

namespace _03._1_Synchronization
{
    internal class ConsoleLog : ILog
    {
        public void Log(string text)
        {
            Console.WriteLine(text);
        }

        public void Log(string format, object args)
        {
            Console.WriteLine(format, args);
        }
    }
}