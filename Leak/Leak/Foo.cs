using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Leak
{
    class Foo
    {
        // A delegate type for hooking up change notifications.
        public delegate void SomeEventHandler();
        public event SomeEventHandler FooEvent;
      
        public Foo()
        {
        }
    }
}
