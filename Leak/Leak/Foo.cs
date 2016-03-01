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
        public event EventHandler<Action> BarEvent;

        public void Baz()
        {

        }

        public Foo()
        {
        }
    }
}
