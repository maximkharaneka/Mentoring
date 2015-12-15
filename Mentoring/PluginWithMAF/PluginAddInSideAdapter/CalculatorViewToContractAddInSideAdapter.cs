using PluginAddInView;
using PluginContract;
using System;
using System.AddIn.Pipeline;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginAddInSideAdapter
{
	[AddInAdapter]
	public class PluginViewToContractAddInSideAdapter
        : ContractBase, IPluginContract
	{
        IPlugin view;

		public PluginViewToContractAddInSideAdapter(IPlugin view)
		{
			this.view = view;
		}

		public string GetDomainName()
		{
			return view.GetDomainName();
		}
        
	}
}
