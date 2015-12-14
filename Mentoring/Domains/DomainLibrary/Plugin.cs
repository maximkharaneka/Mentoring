using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLibrary
{
    public interface IPlugin {
        string GetDomainName();
    }

    public class Plugin : MarshalByRefObject, IPlugin
    {
        private string DomainName { get; set; }
        public Plugin()
        {
            DomainName = AppDomain.CurrentDomain.FriendlyName;
        }

        public string GetDomainName()
        {
            return DomainName;
        }
    }
}
