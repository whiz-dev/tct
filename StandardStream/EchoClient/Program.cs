using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EchoClient
{
    class Program
    {
        static int _buffSize = 1024;

        static void Main(string[] args)
        {
            // Receive
            char[] buffCharArray = new char[_buffSize];
            List<char> msgRecvBytes = new List<char>();
            int readBytes = 0;

            do
            {
                readBytes = Console.In.Read(buffCharArray, 0, _buffSize);

                for (int i = 0; i < readBytes; i++)
                {
                    msgRecvBytes.Add(buffCharArray[i]);
                }
            }
            while (readBytes > 0);

            Console.Out.Write(new string(msgRecvBytes.ToArray()));
        }
    }
}
