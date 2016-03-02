using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leak
{
    class Bar
    {
        private List<int> arr;
        public Bar()
        {
            arr = new List<int>(9999);
            arr.Add(1);
        }

        public void BarMethod()
        {
             
        }
    }
}
