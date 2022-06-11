using Newtonsoft.Json.Linq;
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
            client.BaseAddress = new Uri("http://127.0.0.1:8080/");

            Console.WriteLine("Main Thread : " + Thread.CurrentThread.ManagedThreadId);

            while (true)
            {
                Console.WriteLine("Method : ");
                string method = Console.ReadLine();

                if (method.ToUpper() == "GET")
                {
                    GetRequestAync_ThreadNonblock(client);
                }
                else if (method.ToUpper() == "POST")
                {
                    PostRequestAsync_ThreadNonblock(client);
                }
                else
                {
                    break;
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
            
            HttpResponseMessage res = await client.GetAsync("helloworld?name=park&age=40");

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
            Task<HttpResponseMessage> tsk = client.GetAsync("helloworld?name=park&age=40");
            tsk.Wait();

            HttpResponseMessage res = tsk.Result;

            Console.WriteLine("Response : " + res.StatusCode);
            Console.WriteLine("Response : " + res.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// PostAsync & Thread Nonblock
        /// </summary>
        /// <param name="client"></param>
        static async void PostRequestAsync_ThreadNonblock(HttpClient client)
        {
            // Before Thread = Main Thread
            Console.WriteLine("Before Thread : " + Thread.CurrentThread.ManagedThreadId);

            JObject obj = new JObject();
            StringContent body = new StringContent("안녕!", Encoding.UTF8);

            HttpResponseMessage res = await client.PostAsync("helloworld", body);

            // After Thread = Worker Thread
            Console.WriteLine("After Thread : " + Thread.CurrentThread.ManagedThreadId);

            Console.WriteLine("Response : " + res.StatusCode);
            Console.WriteLine("Response : " + await res.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// PostAsync & Thread Block
        /// </summary>
        /// <param name="client"></param>
        static void PostRequestAsync_ThreadBlock(HttpClient client)
        {
            StringContent body = new StringContent("안녕!", Encoding.UTF8);
            Task<HttpResponseMessage> tsk = client.PostAsync("helloworld", body);
            tsk.Wait();

            HttpResponseMessage res = tsk.Result;

            Console.WriteLine("Response : " + res.StatusCode);
            Console.WriteLine("Response : " + res.Content.ReadAsStringAsync().Result);
        }
    }
}
