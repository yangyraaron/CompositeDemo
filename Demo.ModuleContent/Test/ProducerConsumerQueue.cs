using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Library.Common.Extension;
using System.Windows;

namespace Demo.Content.Test
{
    public class QueueEntityEvetArgs<T>:EventArgs
    {
        private T _obj;
        public QueueEntityEvetArgs(T obj)
        {
            _obj = obj;
        }

        public T Entity
        {
            get { return _obj; }
        }
    }

    public class ProducerConsumerQueue<T>:IDisposable    
    {
        private Queue<T> _queue;
        private AutoResetEvent _waiter;
        private Thread _tracker;
        private static object _locker = new object();
        private bool _isActive =false;

        public event EventHandler<QueueEntityEvetArgs<T>> EntityPoped;
        
        public ProducerConsumerQueue()
        {
            _queue = new Queue<T>();
            _waiter = new AutoResetEvent(false);
            _isActive = true;
            _tracker = new Thread (Track);
            _tracker.Start();
        }

        public void EnqueueEntity(T entity)
        {
            lock(_locker)
            {
                _queue.Enqueue(entity);
            }

            _waiter.Set();
        }

        private void Track()
        {
            while (_isActive)
            {
                //to indicate if the entity is from the queue.
                int count = 0;

                T entity = default(T);
                lock (_locker)
                {
                    if (_queue.Count > 0)//only the count of queue is >0,then update the count
                    {                    // and entity,otherwise only the count>0 ,the entity is from queue   
                        count = _queue.Count;
                        entity = _queue.Dequeue();
                    }
                }

                if (count > 0)//if the entity is from queue,then fire the event
                {
                    var handler = EntityPoped;
                    if (handler != null)
                        Application.Current.Dispatcher.BeginInvoke(
                            new Action(() =>
                            {
                                handler(this, new QueueEntityEvetArgs<T>(entity));
                            }));

                }
                else// if the queue is empty then wait
                {
                    _waiter.WaitOne();
                }
            } 
        }

        public void Close()
        {
            _isActive = false;
            _waiter.Set();

            _tracker.Join();
            _waiter.Close();
        }
    
        #region IDisposable Members

        public void  Dispose()
        {
            Close();
        }

        #endregion
}
}
