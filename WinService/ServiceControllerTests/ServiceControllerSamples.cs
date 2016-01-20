using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceProcess;

namespace ServiceControllerTests
{
	[TestClass]
	public class ServiceControllerSamples
	{
		[TestMethod]
		public void StartService()
		{
			var controller = new ServiceController("FileProcessorService");
			controller.Start();
		}

		[TestMethod]
		public void StopService()
		{
			var controller = new ServiceController("FileProcessorService");
			controller.Stop();
		}

	}
}
