﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSRT2
{
    public static class Test
    {
        public static void Try(string message)
        {
            var x = message;
        }
    }

    public sealed class Test2
    {
        public void Echo(string message)
        {
            var x = message;
        }
    }
}
