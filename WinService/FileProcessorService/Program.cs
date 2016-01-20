using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileProcessorService
{
	class Program
	{
		static void Main(string[] args)
		{
			string currentDir = Path.GetDirectoryName(Path.GetFullPath(Process.GetCurrentProcess().MainModule.FileName));

			string sourceDir = Path.Combine(currentDir, "Source");
			string dstDir = Path.Combine(currentDir, "Dst");

			//Debugger.Launch();
				
			var fileProcessor = new FileProcessor(sourceDir, dstDir);

			ServiceBase.Run(fileProcessor);
		}
	}
}
