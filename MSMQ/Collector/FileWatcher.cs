using System.IO;
using System.Threading;

namespace Collector
{
    internal class FileWatcher
    {
        private readonly FileSystemWatcher fileWatcher;
        private readonly string sourcePath;
        private readonly ManualResetEvent stopWorkEvent = new ManualResetEvent(false);
        public MSMQWrapper msmq;
        private string queueName;
        private AutoResetEvent sourceDirectoryChangedEvent = new AutoResetEvent(false);
        private Thread workThread;

        public FileWatcher(string inRootPath, string qName)
        {
            sourcePath = inRootPath;
            queueName = qName;
            //workThread = new Thread(WorkProcedure);
            fileWatcher = new FileSystemWatcher(sourcePath);
            fileWatcher.EnableRaisingEvents = false;

            fileWatcher.Changed += SourceDirectoryChanged;
            fileWatcher.Created += SourceDirectoryChanged;
            fileWatcher.Renamed += SourceDirectoryChanged;

            msmq = new MSMQWrapper(queueName);
        }

        private void SourceDirectoryChanged(object sender, FileSystemEventArgs e)
        {
            //sourceDirectoryChangedEvent.Set();
            msmq.SendObject(e.FullPath);
        }

        public void Start()
        {
            stopWorkEvent.Reset();
            // sourceDirectoryChangedEvent.Reset();
            fileWatcher.EnableRaisingEvents = true;
            //workThread.Start();
        }

        public void Stop()
        {
            fileWatcher.EnableRaisingEvents = false;
            stopWorkEvent.Set();
           // workThread.Join();
        }
    }
}