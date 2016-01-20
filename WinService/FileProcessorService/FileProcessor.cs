using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileProcessorService
{
	class FileProcessor : ServiceBase
	{
		internal const string SericeName = "FileProcessorService";

		private Thread workThread;
		private ManualResetEvent stopWorkEvent = new ManualResetEvent(false);
		private AutoResetEvent sourceDirectoryChangedEvent = new AutoResetEvent(false);
		private string sourcePath;
		private string destinationPath;

		private FileSystemWatcher fileWatcher;

		public FileProcessor(string inRootPath, string outRootPath)
		{
			this.CanStop = true;
			this.ServiceName = FileProcessor.SericeName;
			this.AutoLog = false;

			sourcePath = inRootPath;
			destinationPath = outRootPath;
			workThread = new Thread(WorkProcedure);
			fileWatcher = new FileSystemWatcher(sourcePath);
			fileWatcher.EnableRaisingEvents = false;
			
			fileWatcher.Changed += SourceDirectoryChanged;
			fileWatcher.Created += SourceDirectoryChanged;
			fileWatcher.Renamed += SourceDirectoryChanged;
		}

		private void SourceDirectoryChanged(object sender, FileSystemEventArgs e)
		{
			sourceDirectoryChangedEvent.Set();
		}

		protected override void OnStart(string[] args)
		{
			stopWorkEvent.Reset();
			sourceDirectoryChangedEvent.Reset();
			fileWatcher.EnableRaisingEvents = true;
			workThread.Start();
		}

		protected override void OnStop()
		{
			fileWatcher.EnableRaisingEvents = false;
			stopWorkEvent.Set();
			workThread.Join();
		}

		protected void WorkProcedure(object obj)
		{
		    var driveService = DriveConnector.GetDriveService();
            do
			{
				foreach (var fileInfo in Directory.EnumerateFiles(sourcePath))
				{
					if (stopWorkEvent.WaitOne(TimeSpan.Zero))
						return;

					FileStream file;

					if (TryOpen(fileInfo, out file, FileMode.Open, FileAccess.ReadWrite, FileShare.None, 3))
					{
						file.Close();

						File.Move(fileInfo, Path.Combine(destinationPath, Path.GetFileName(fileInfo)));
					    driveService.Files.Create(new Google.Apis.Drive.v3.Data.File(), file,"").Upload();
					}					
				}


			}
			while (WaitHandle.WaitAny(new WaitHandle[] { stopWorkEvent, sourceDirectoryChangedEvent }, 
				TimeSpan.FromMilliseconds(10000)) != 0);
		}

		private bool TryOpen(string fileName, out FileStream file, FileMode fileMode, FileAccess fileAccess, FileShare fileShare, int count)
		{
			for (int i = 0; i < count; i++)
			{
				try
				{
					file = new FileStream(fileName, fileMode, fileAccess, fileShare);

					return true;
				}
				catch (IOException)
				{
					Thread.Sleep(5000);
				}
			}

			file = null;
			return false;
		}
	}
}
