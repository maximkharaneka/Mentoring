using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace _02_Threads
{
    internal class Program
    {
        private static readonly string outputFile = "output.txt";

        private static readonly List<string> fileNames = new List<string> { "test1.txt", "test2.txt" };

        private static readonly Mutex mut = new Mutex();

        private static void Main(string[] args)
        {
            Console.WriteLine("Cleanup: ", outputFile);
            File.WriteAllText(outputFile, string.Empty);

            foreach (var fileName in fileNames)
            {
                var oThread = new Thread(ReadWriteFile);
                oThread.Start(fileName);
                //ReadWriteFile(fileName);
            }
            Console.WriteLine("Done.");
            Console.ReadLine();
        }

        private static void ReadWriteFile(object fileName)
        {
            mut.WaitOne();
            ReadWriteFile((string)fileName);
            mut.ReleaseMutex();
        }

        private static void ReadWriteFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                Console.WriteLine("Reading: ", fileName);
                var content = File.ReadAllText(fileName);
                //                Console.WriteLine("Current content of file:");
                //                Console.WriteLine(content);
                Console.WriteLine("Writing: ", fileName);
                File.AppendAllText(outputFile, content);
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }
        }
    }
}