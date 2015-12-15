using PluginContract;
using PluginHostView;
using System;
using System.AddIn.Pipeline;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc1HostSideAdapter
{
	[HostAdapter]
	public class PluginContractToHostView : IPlugin
	{
		IPluginContract contract;

		PluginContractToHostView(IPluginContract contract)
		{
			this.contract = contract;
		}

		public string GetDomainName()
		{
			return contract.GetDomainName();
		}
    }
}
