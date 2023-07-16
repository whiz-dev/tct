using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSync
{
    internal class ServerProcess
    {
        private string _serverName = null;
        public string ServerName
        {
            get
            {
                return _serverName;
            }
        }

        private object _obj = new object();

        private List<string> _cmds = new List<string>();

        public ServerProcess(string serverName)
        {
            _serverName = serverName;
        }

        public void ExecuteCommand(string cmd)
        {
            int code = SetStatus(true, cmd);

            if (code == 1)
            {
                throw new Exception("Duplicate Command!");
            }
            else if (code == 2)
            {
                throw new Exception("Exceed Count!");
            }

            switch (cmd)
            {
                case "A":
                    Thread.Sleep(5000);
                    break;

                case "B":
                    Thread.Sleep(5000);
                    break;

                case "C":
                    Thread.Sleep(5000);
                    break;
            }

            SetStatus(false, cmd);
        }

        private int SetStatus(bool isStart, string cmd)
        {
            lock (_obj)
            {
                if (isStart)
                {
                    if (!_cmds.Contains(cmd))
                    {
                        _cmds.Add(cmd);
                    }
                    else
                    {
                        return 1;
                    }

                    if (_cmds.Count > 3)
                    {
                        return 2;
                    }
                }
                else
                {
                    _cmds.Remove(cmd);
                }
            }

            return 0;
        }
    }
}
