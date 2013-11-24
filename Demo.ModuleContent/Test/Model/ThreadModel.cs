using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Demo.Content.Test
{
    public class AutoResetEventThreadModel:IDisposable
    {
        private ProducerConsumerQueue<string> _queue;

        private EventWaitHandle _waitHandle = new AutoResetEvent(false);
        
        public AutoResetEventThreadModel(ProducerConsumerQueue<string> queue)
        {
            _queue = queue;
        }

        #region AutoResetEvent

        public void TestAutoResetEvent()
        {
            var thread = new Thread(Waiter);

            thread.Start();

            _waitHandle.WaitOne();

            _queue.EnqueueEntity("Main Thread is sleeping");
            
            Thread.Sleep(1000);

            _queue.EnqueueEntity("Main Thread wakes up");

            _waitHandle.Set();

        }

        private void Waiter()
        {
            _queue.EnqueueEntity("Thread 2 is waiting...");

            _waitHandle.Set();

            _waitHandle.WaitOne();

            _queue.EnqueueEntity("Thread 2 is notified!");
        }

        #endregion

        public void Close()
        {
          _waitHandle.Set();
          _waitHandle.Close();
        }


        #region IDisposable Members

        public void Dispose()
        {
          this.Close();
        }

        #endregion
    }
}
