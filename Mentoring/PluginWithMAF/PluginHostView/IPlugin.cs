using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginHostView
{
	public interface IPlugin
	{
        string GetDomainName();
    }
}
