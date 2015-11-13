using System.IO;
using System.Threading;

namespace _03._1_Synchronization
{
    internal class FileLog : ILog
    {
        private static readonly Mutex mut = new Mutex();

        private readonly string outputFile = "log.log";

        public FileLog()
        {
            File.WriteAllText(this.outputFile, string.Empty);
        }

        public void Log(string text)
        {
            mut.WaitOne();
            File.AppendAllText(this.outputFile, text);
            File.AppendAllText(this.outputFile, "\t\n");
            mut.ReleaseMutex();
        }

        public void Log(string format, object args)
        {
            mut.WaitOne();
            File.AppendAllText(this.outputFile, string.Format(format, args));
            File.AppendAllText(this.outputFile, "\t\n");
            mut.ReleaseMutex();
        }
    }
}