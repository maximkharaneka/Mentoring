using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleServiceApplication
{
	class Program
	{
		static void Main(string[] args)
		{
			ServiceBase.Run(new MyService());
		}
	}
}
