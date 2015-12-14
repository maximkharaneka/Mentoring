using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DomainLibrary
{
    public class DomainManager
    {
        private AppDomainSetup domainSetup = new AppDomainSetup()
        {
            ApplicationBase = AppDomain.CurrentDomain.BaseDirectory
        };

        private ConcurrentDictionary<IPlugin, AppDomain> Domains =
            new ConcurrentDictionary<IPlugin, AppDomain>();

        public IPlugin StartPlugin(string AssemblyName, string typeName, string FriendlyName)
        {
            var domain = AppDomain.CreateDomain(FriendlyName, new Evidence(), domainSetup);
            var plugin = (IPlugin) domain.CreateInstanceAndUnwrap(AssemblyName, typeName);
            Domains.TryAdd(plugin, domain);

            return plugin;
        }

        public bool StopPlugin(IPlugin plugin)
        {
            AppDomain domain;

            Domains.TryRemove(plugin, out domain);

            try
            {
                AppDomain.Unload(domain);
            }
            catch (CannotUnloadAppDomainException ex)
            {
                Domains.TryAdd(plugin, domain);
                return false;
            }
            try
            {
                var val = plugin.GetDomainName();
            }
            catch (AppDomainUnloadedException ex)
            {
            }

            return true;
        }
    }
}