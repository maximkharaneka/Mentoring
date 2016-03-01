using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Leak
{
    [TestClass]
    public class Class1
    {
        [TestMethod]
        public void Test1()
        {
            Foo object1 = new Foo();
            Foo object2 = new Foo();
            // ...
            object1.SomeEvent += object2.Bar;
            // ...

            // Should call this
            // object1.SomeEvent -= object2.myEventHandler;

            object2.Dispose();
        }
        [TestMethod]
        public void Test2()
        {
            Assert.AreEqual(1,2);
        }
    }
}
