using System;
using System.AddIn.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Collections.Concurrent;
using System.Linq;
using PluginHostView;
using System.Security.Policy;

namespace PluginApi
{
    public class ManagerOfApi
    {
        private AppDomainSetup domainSetup = new AppDomainSetup()
        {
            ApplicationBase = AppDomain.CurrentDomain.BaseDirectory
        };
        private ConcurrentDictionary<IPlugin, AppDomain> Domains =
            new ConcurrentDictionary<IPlugin, AppDomain>();

        public IPlugin StartPlugin(string FriendlyName)
        {
            var rootFolder = Path.Combine(Environment.CurrentDirectory, "Pipeline");
            var warnings = AddInStore.Update(rootFolder);
            var tokens = AddInStore.FindAddIns(typeof(IPlugin), rootFolder);
            var firstToken = tokens.First();

            var domain = AppDomain.CreateDomain(FriendlyName, new Evidence(), domainSetup);

            IPlugin plugin = firstToken.Activate<IPlugin>(domain);
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
