using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleServiceApplication
{
	class MyService : ServiceBase
	{
		internal MyService()
		{
			this.CanStop = true;
			this.ServiceName = "MyService";
			this.AutoLog = false;
		}

		protected override void OnStart(string[] args)
		{			
			EventLog.WriteEntry("MyService started");
		}

		protected override void OnStop()
		{
			EventLog.WriteEntry("MyService stopped");
		}
	}
}
