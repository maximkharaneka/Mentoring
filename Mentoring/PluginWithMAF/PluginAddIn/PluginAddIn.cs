using PluginAddInView;
using System;
using System.AddIn;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddInV1
{
	[AddIn("Plugin AddIn", Version="1.0.0.8")]
	public class PluginAddIn : IPlugin
	{
        private string DomainName { get; set; }
        public PluginAddIn()
        {
            DomainName = AppDomain.CurrentDomain.FriendlyName;
        }

        public string GetDomainName()
        {
            return DomainName;
        }
    }
}
