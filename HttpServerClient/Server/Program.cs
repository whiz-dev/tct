using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HttpListener server = new HttpListener();
            server.Prefixes.Add("http://127.0.0.1:8080/");
            server.Start();
            Console.WriteLine("Server Started");

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            Task tsk = Task.Factory.StartNew(() => { AcceptRequest(server, token); });

            Console.ReadLine();

            cts.Cancel();
            tsk.Wait();
            Console.WriteLine("Cancel Accept Request");

            server.Close();
            Console.WriteLine("Server Closed");

            Console.ReadLine();
        }

        static void AcceptRequest(HttpListener server, CancellationToken token)
        {
            //ManagedThreadPool 최소값 설정
            //SetMinThreadPool(30);

            while (true)
            {
                Task<HttpListenerContext> tsk = server.GetContextAsync();

                try
                {
                    tsk.Wait(token);

                    // Managed Thread
                    Task.Factory.StartNew(() => { ProcessRequest(tsk.Result); });

                    // Unmanaged Thread
                    //Thread thr = new Thread(new ParameterizedThreadStart(ProcessRequest));
                    //thr.Start(tsk.Result);
                }
                catch
                {
                    break;
                }
            }
        }

        static void ProcessRequest(object result)
        {
            HttpListenerContext context = (HttpListenerContext)result;
            HttpListenerRequest request = (HttpListenerRequest)context.Request;
            HttpListenerResponse response = (HttpListenerResponse)context.Response;

            // Request Info
            Console.WriteLine("### Request Info ###");
            Console.WriteLine("Url : " + request.Url);
            Console.WriteLine("RawUrl : " + request.RawUrl);
            Console.WriteLine("HttpMethod : " + request.HttpMethod);
            Console.WriteLine("ContentType : " + request.ContentType);
            Console.WriteLine("ContentEncoding : " + request.ContentEncoding.EncodingName);
            Console.WriteLine("ContentLength : " + request.ContentLength64);

            foreach (string key in context.Request.QueryString.AllKeys)
            {
                Console.WriteLine("QueryString : " + key + ", " + context.Request.QueryString[key]);
            }

            System.IO.StreamReader reader = new System.IO.StreamReader(request.InputStream, request.ContentEncoding);
            string body = reader.ReadToEnd();
            reader.Close();
            Console.WriteLine("Body : " + body);

            // Do some work for 5 Seconds
            Thread.Sleep(5000);
            byte[] data = Encoding.UTF8.GetBytes("안녕!");

            // Response
            response.ContentEncoding = Encoding.UTF8;
            response.StatusCode = 200;
            response.OutputStream.Write(data, 0, data.Length);
            response.Close();
        }

        static bool SetMinThreadPool(int workerCount)
        {
            int minWorker, minIOC;
            ThreadPool.GetMinThreads(out minWorker, out minIOC);

            return ThreadPool.SetMinThreads(workerCount, minIOC);
        }
    }
}
