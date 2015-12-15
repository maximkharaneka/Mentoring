using PluginApi;
using PluginHostView;
using System;
using System.AddIn.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
	class Program
	{
		static void Main(string[] args)
		{
            var manager = new ManagerOfApi();

            var plugin = manager.StartPlugin("Plugin1");
            var plugin2 = manager.StartPlugin( "Plugin2");
            Console.WriteLine(plugin.GetDomainName());
            Console.WriteLine(plugin2.GetDomainName());
            Console.WriteLine(manager.StopPlugin(plugin));
            Console.WriteLine(manager.StopPlugin(plugin2));
            Console.ReadKey();
        }
	}
}
