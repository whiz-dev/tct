using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSync
{
    internal class Program
    {
        static ConcurrentDictionary<string, SyncManager> _syncManagers = new ConcurrentDictionary<string, SyncManager>();

        static void Main(string[] args)
        {
            List<string> reqCmds = new List<string>();
            reqCmds.Add("A");
            reqCmds.Add("A");
            reqCmds.Add("B");
            reqCmds.Add("C");
            reqCmds.Add("A");

            List<ServerProcess> svrs = new List<ServerProcess>();
            svrs.Add(new ServerProcess("SVR1"));
            svrs.Add(new ServerProcess("SVR2"));

            foreach (string cmd in reqCmds)
            {
                foreach (ServerProcess svr in svrs)
                {
                    Thread thr = new Thread(new ParameterizedThreadStart(Request));
                    thr.Start(new object[] { svr, cmd });
                }
            }
        }

        static void Request(object obj)
        {
            ServerProcess svr = ((object[])obj)[0] as ServerProcess;
            string cmd = ((object[])obj)[1] as string;

            _syncManagers.TryAdd(svr.ServerName, new SyncManager(3));
            _syncManagers[svr.ServerName].SetStart(cmd);

            try
            {
                Console.WriteLine($"[{DateTime.Now.ToString()}] [{svr.ServerName}] [{cmd}] Request Start!");
                svr.ExecuteCommand(cmd);
                Console.WriteLine($"[{DateTime.Now.ToString()}] [{svr.ServerName}] [{cmd}] Request Completed!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _syncManagers[svr.ServerName].SetEnd(cmd);
            }
        }
    }
}
