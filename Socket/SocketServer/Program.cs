using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            TcpListener server = null;

            try
            {
                server = new TcpListener(IPAddress.Parse("127.0.0.1"), 9090); //IP, Port
                server.Start();
                Console.WriteLine("[Server] Started");

                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("[Server] Client Connected");

                    Task.Factory.StartNew(new Action<object>(HandleClient), client);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Socket] Exception");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (server != null)
                {
                    server.Stop();
                }
            }
        }

        //private static void HandleClient(object client)
        //{
        //    NetworkStream ns = null;
        //    StreamReader sr = null;
        //    StreamWriter sw = null;

        //    try
        //    {
        //        ns = ((TcpClient)client).GetStream();
        //        sr = new StreamReader(ns, Encoding.UTF8);
        //        sw = new StreamWriter(ns, Encoding.UTF8);

        //        string msg = null;

        //        while (true)
        //        {
        //            msg = sr.ReadLine();
        //            Console.WriteLine("[HandleClient] Received");
        //            Console.WriteLine(msg);

        //            if (msg.ToUpper() == "Q")
        //                break;

        //            sw.WriteLine(msg);
        //            sw.Flush();
        //        }

        //        Console.WriteLine("[HandleClient] Disconnected");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("[HandleClient] Exception");
        //        Console.WriteLine(ex.Message);
        //    }
        //    finally
        //    {
        //        if(sw != null) sw.Close();
        //        if(sr != null) sr.Close();
        //        if(ns != null) ns.Close();
        //    }
        //}

        private static void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            NetworkStream ns = null;

            try
            {
                ns = client.GetStream();

                int buffSize = 256;
                byte[] buffBytes = new byte[buffSize];
                
                while (true)
                {
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

                    Console.WriteLine("[HandleClient] Received");
                    Console.WriteLine(msgRecv);

                    if (msgRecv.ToUpper() == "Q")
                        break;

                    // Send
                    string msgSend = msgRecv;
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
                }

                Console.WriteLine("[HandleClient] Disconnected");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[HandleClient] Exception");
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
