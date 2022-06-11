using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();

            Console.WriteLine("Main Thread : " + Thread.CurrentThread.ManagedThreadId);

            while (true)
            {
                string msg = Console.ReadLine();

                if (msg.ToUpper() == "Q")
                {
                    break;
                }

                if (msg.ToUpper() == "N")
                {
                    GetRequestAync_ThreadNonblock(client);
                }
                else
                {
                    GetRequestAsync_ThreadBlock(client);
                    //PostRequestAsync_ThreadBlock(client);
                }
            }
        }

        /// <summary>
        /// GetAsync & Thread Nonblock
        /// </summary>
        /// <param name="client"></param>
        static async void GetRequestAync_ThreadNonblock(HttpClient client)
        {
            // Before Thread = Main Thread
            Console.WriteLine("Before Thread : " + Thread.CurrentThread.ManagedThreadId);
            
            HttpResponseMessage res = await client.GetAsync("http://127.0.0.1:8080/helloworld?name=park&age=40");

            // After Thread = Worker Thread
            Console.WriteLine("After Thread : " + Thread.CurrentThread.ManagedThreadId);

            Console.WriteLine("Response : " + res.StatusCode);
            Console.WriteLine("Response : " + await res.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// GetAsync & Thread Block
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        static void GetRequestAsync_ThreadBlock(HttpClient client)
        {
            Task<HttpResponseMessage> tsk = client.GetAsync("http://127.0.0.1:8080/helloworld?name=park&age=40");
            tsk.Wait();

            HttpResponseMessage res = tsk.Result;

            Console.WriteLine("Response : " + res.StatusCode);
            Console.WriteLine("Response : " + res.Content.ReadAsStringAsync().Result);
        }

        static void PostRequestAsync_ThreadBlock(HttpClient client)
        {
            StringContent body = new StringContent("안녕!", Encoding.UTF8);
            Task<HttpResponseMessage> tsk = client.PostAsync("http://127.0.0.1:8080/helloworld", body);
            tsk.Wait();

            HttpResponseMessage res = tsk.Result;

            Console.WriteLine("Response : " + res.StatusCode);
            Console.WriteLine("Response : " + res.Content.ReadAsStringAsync().Result);
        }
    }
}
