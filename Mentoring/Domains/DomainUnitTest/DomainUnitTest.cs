using System;
using DomainLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestDomainApiLibrary
{
    [TestClass]
    public class DomainUnitTest
    {
        [TestMethod]
        public void BasicDomainTest()
        {

            var manager = new DomainManager();

            var pluginFoo = manager.StartPlugin("DomainLibrary", "DomainLibrary.Plugin", "PluginFoo");
            var pluginBar = manager.StartPlugin("DomainLibrary", "DomainLibrary.Plugin", "PluginBar");
            Console.WriteLine("Plugin foo domain: " + pluginFoo.GetDomainName());
            Console.WriteLine("Plugin bar domain: " + pluginBar.GetDomainName());
            Console.WriteLine(manager.StopPlugin(pluginFoo));
            Console.WriteLine(manager.StopPlugin(pluginBar));
        }
    }
}
