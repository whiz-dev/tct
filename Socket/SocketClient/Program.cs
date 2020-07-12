using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketClient
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    TcpClient client = null;
        //    NetworkStream ns = null;
        //    StreamReader sr = null;
        //    StreamWriter sw = null;

        //    try
        //    {
        //        client = new TcpClient("127.0.0.1", 9090);
        //        Console.WriteLine("[Client] Connected");

        //        ns = client.GetStream();
        //        sr = new StreamReader(ns, Encoding.UTF8);
        //        sw = new StreamWriter(ns, Encoding.UTF8);

        //        string msgRecv = null;
        //        string msgSend = null;

        //        while (true)
        //        {
        //            msgSend = Console.ReadLine();

        //            sw.WriteLine(msgSend);
        //            sw.Flush();

        //            if (msgSend.ToUpper() == "Q")
        //            {
        //                break;
        //            }

        //            msgRecv = sr.ReadLine();
        //            Console.WriteLine("[Client] Received");
        //            Console.WriteLine(msgRecv);
        //        }

        //        Console.WriteLine("[Client] Disconnected");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("[Client] Exception");
        //        Console.WriteLine(ex.Message);
        //    }
        //    finally
        //    {
        //        if (sw != null)
        //            sw.Close();
        //        if (sr != null)
        //            sr.Close();
        //        if (ns != null)
        //            ns.Close();
        //        if (client != null)
        //            client.Close();
        //    }
        //}

        static void Main(string[] args)
        {
            TcpClient client = null;
            NetworkStream ns = null;

            try
            {
                client = new TcpClient("127.0.0.1", 9090);
                Console.WriteLine("[Client] Connected");

                ns = client.GetStream();

                int buffSize = 256;
                byte[] buffBytes = new byte[buffSize];
                
                while (true)
                {
                    // Send
                    //string msgSend = Console.ReadLine();
                    string msgSend = "박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 박지홍 ";
                    byte[] msgSendBytes = Encoding.UTF8.GetBytes(msgSend + Environment.NewLine);

                    int totBytes = 0;

                    do
                    {
                        int writeBytes = Math.Min(buffSize, msgSendBytes.Length - totBytes);
                        ns.Write(msgSendBytes, totBytes, writeBytes);

                        totBytes += writeBytes;

                        if (totBytes >= msgSendBytes.Length)
                        {
                            break;
                        }
                    }
                    while (true);

                    if (msgSend.ToUpper() == "Q")
                    {
                        break;
                    }

                    // Receive
                    string msgRecv = null;
                    List<byte> msgRecvBytes = new List<byte>();

                    do
                    {
                        int readBytes = ns.Read(buffBytes, 0, buffSize);

                        for (int i = 0; i < readBytes; i++)
                        {
                            msgRecvBytes.Add(buffBytes[i]);
                        }

                        if (EndsWithNewLine(msgRecvBytes))
                        {
                            break;
                        }
                    }
                    while (true);

                    msgRecv = Encoding.UTF8.GetString(msgRecvBytes.ToArray(), 0, msgRecvBytes.Count);
                    msgRecv = msgRecv.Substring(0, msgRecv.Length - 2);

                    Console.WriteLine("[Client] Received");
                    Console.WriteLine(msgRecv);
                }

                Console.WriteLine("[Client] Disconnected");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Client] Exception");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (ns != null)
                    ns.Close();
                if (client != null)
                    client.Close();
            }
        }

        private static bool EndsWithNewLine(List<byte> byteArray)
        {
            byte[] newLineBytes = Encoding.UTF8.GetBytes(Environment.NewLine);

            if (byteArray.Count < newLineBytes.Length)
            {
                return false;
            }

            bool isNewLine = true;
            int cnt = 1;

            for (int i = newLineBytes.Length - 1; i >= 0; i--)
            {
                if (newLineBytes[i] != byteArray[byteArray.Count - cnt])
                {
                    isNewLine = false;
                    break;
                }

                cnt++;
            }

            return isNewLine;
        }
    }
}
