﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03._1_Synchronization
{
    interface ILog
    {
        void Log(string text);

        void Log(string format, object args);
    }
}
