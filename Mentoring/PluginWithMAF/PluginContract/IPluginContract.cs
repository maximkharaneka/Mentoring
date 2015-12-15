using System;
using System.AddIn.Contract;
using System.AddIn.Pipeline;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginContract
{
	[AddInContract]
	public interface IPluginContract : IContract
	{
        string GetDomainName();
    }
}
