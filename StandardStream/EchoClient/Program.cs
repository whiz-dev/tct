﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EchoClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string msg = Console.In.ReadToEnd();

            Console.Out.Write(msg);
        }
    }
}