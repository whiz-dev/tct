using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoResetEventTest
{
    public class SyncManager
    {
        private object _objStart = new object();
        private object _objEnd = new object();

        private ConcurrentDictionary<string, object> _objCmdLock = new ConcurrentDictionary<string, object>();

        private object _objCount = new object();

        private AutoResetEvent _countSync = new AutoResetEvent(true);
        private ConcurrentDictionary<string, AutoResetEvent> _cmdSync = new ConcurrentDictionary<string, AutoResetEvent>();

        private int _maxCount = 0;

        private List<string> _cmds = new List<string>();

        public SyncManager(int maxCount)
        {
            _maxCount = maxCount;
        }

        public void SetStart(string cmd)
        {
            _objCmdLock.TryAdd(cmd, new object());
            _cmdSync.TryAdd(cmd, new AutoResetEvent(false));

            lock (_objCmdLock[cmd])
            {
                if (_cmds.Contains(cmd))
                {
                    _cmdSync[cmd].WaitOne();
                }
            }

            lock (_objCount)
            {
                if (_cmds.Count == 3)
                {
                    _countSync.WaitOne();
                }
            }

            _cmds.Add(cmd);

            // 입력된 cmd 순서대로 순차처리 필요시 lock
            //lock (_objStart)
            //{
            //    _objCmdLock.TryAdd(cmd, new object());
            //    _cmdSync.TryAdd(cmd, new AutoResetEvent(false));

            //    lock (_objCmdLock[cmd])
            //    {
            //        if (_cmds.Contains(cmd))
            //        {
            //            _cmdSync[cmd].WaitOne();
            //        }
            //    }

            //    lock (_objCount)
            //    {
            //        if (_cmds.Count == 3)
            //        {
            //            _countSync.WaitOne();
            //        }
            //    }

            //    _cmds.Add(cmd);
            //}
        }

        public void SetEnd(string cmd)
        {
            lock (_objEnd)
            {
                _cmds.Remove(cmd);

                _cmdSync[cmd].Set();
                _countSync.Set();
            }
        }
    }
}
