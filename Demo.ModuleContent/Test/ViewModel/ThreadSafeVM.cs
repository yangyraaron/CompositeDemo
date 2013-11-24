using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Commands;
using System.Threading;
using System.Collections.ObjectModel;
using System.Collections;
using System.Windows;
using System.Windows.Threading;
using Library.Common.Extension;

namespace Demo.Content.Test
{
    [Export]
    public class ThreadSafeVM : NotificationObject
    {
        
        private ProducerConsumerQueue<string> _queue; 
        private int _mainThreadId;

        public ThreadSafeVM()
        {
            _messages = new ObservableCollection<string>();
            StartCommand = new DelegateCommand<object>(OnStart);

            _mainThreadId = Thread.CurrentThread.ManagedThreadId;
        }

        private void Initialize()
        {
          _queue = new ProducerConsumerQueue<string>();
          _queue.EntityPoped += new EventHandler<QueueEntityEvetArgs<string>>(_queue_EntityPoped);
        }

        void _queue_EntityPoped(object sender, QueueEntityEvetArgs<string> e)
        {
            AddMessage(e.Entity);
        }

        private ObservableCollection<string> _messages;
        public ObservableCollection<string> Messages
        {
            get { return _messages; }
        }

        public DelegateCommand<object> StartCommand { get; set; }

        private void OnStart(object parameter)
        {
            var modelType = parameter.To<ThreadModelType>();
            switch(modelType)
            {
                case ThreadModelType.AutoResetEvent:
                    var model = new AutoResetEventThreadModel(_queue);
                    model.TestAutoResetEvent();
                    break;
                default:
                    break;
            }
        }

        private void AddMessage(string message)
        {
           Messages.Add(message);
        }

     
    }
}
