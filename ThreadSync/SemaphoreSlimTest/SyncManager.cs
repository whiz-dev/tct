using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SemaphoreSlimTest
{
    public class SyncManager
    {
        private object _objStart = new object();

        private SemaphoreSlim _countSync = null;
        private ConcurrentDictionary<string, SemaphoreSlim> _cmdSync = new ConcurrentDictionary<string, SemaphoreSlim>();

        public SyncManager(int maxCount)
        {
            _countSync = new SemaphoreSlim(maxCount, maxCount);
        }

        public void SetStart(string cmd)
        {
            _cmdSync.TryAdd(cmd, new SemaphoreSlim(1, 1));

            _cmdSync[cmd].Wait();
            _countSync.Wait();

            // 입력된 cmd 순서대로 순차처리 필요시 lock
            //lock (_objStart)
            //{
            //    _cmdSync.TryAdd(cmd, new SemaphoreSlim(1, 1));

            //    _cmdSync[cmd].Wait();
            //    _countSync.Wait();
            //}
        }

        public void SetEnd(string cmd)
        {
            _cmdSync[cmd].Release();
            _countSync.Release();
        }
    }
}
