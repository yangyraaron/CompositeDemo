using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Commands;
using System.ComponentModel;
using System.Threading;

namespace Demo.Content.Test
{
  [Export]
  public class BackgroundVM : NotificationObject
  {
    private ProducerConsumerQueue<string> _queue;
    private BackgroundWorker _worker;

    public BackgroundVM()
    {
      Messages = new ObservableCollection<string>();
      Progress = new ProgressVM { Maximum = 100, Minimum = 0 };

      StartCommand = new DelegateCommand<object>(o => { OnStart(); });
      CancelCommand = new DelegateCommand<object>(o => { OnCancel(); });
      CloseCommand = new DelegateCommand<object>(o => OnClose());
    }

    void Initilize()
    {
      _queue = new ProducerConsumerQueue<string>();

      _queue.EntityPoped += new EventHandler<QueueEntityEvetArgs<string>>(_queue_EntityPoped);
    }

    void IntializeWorker()
    {
      _worker = new BackgroundWorker
      {
        WorkerReportsProgress = true,
        WorkerSupportsCancellation = true
      };

      _worker.DoWork += new DoWorkEventHandler(_worker_DoWork);
      _worker.ProgressChanged += new ProgressChangedEventHandler(_worker_ProgressChanged);
      _worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_worker_RunWorkerCompleted);
    }

    void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (e.Cancelled)
      {
        _queue.EnqueueEntity("Work has been cancelled!");
      }
      else if (e.Error != null)
      {
        _queue.EnqueueEntity("Something is error! Error Message:");
        _queue.EnqueueEntity(e.Error.Message);
      }
      else
        _queue.EnqueueEntity("Work is done Successfully!");
    }

    void _worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      this.Progress.Caption = string.Format("Current Progress is: {0}", e.ProgressPercentage);
      this.Progress.Value = e.ProgressPercentage;
    }

    void _worker_DoWork(object sender, DoWorkEventArgs e)
    {
      for (int i = 1; i <= 100; i++)
      {
        if (_worker.CancellationPending)
        {
          e.Cancel = true;
          return;
        }

        Thread.Sleep(200);
        _worker.ReportProgress(i);
      }

      e.Result = 1;
    }

    void _queue_EntityPoped(object sender, QueueEntityEvetArgs<string> e)
    {
      Messages.Add(e.Entity);
    }

    #region Commands

    public DelegateCommand<Object> StartCommand { get; set; }
    public DelegateCommand<object> CancelCommand { get; set; }
    public DelegateCommand<object> CloseCommand { get; set; }

    #endregion

    #region Properties

    public ObservableCollection<string> Messages { get; set; }

    public ProgressVM Progress { get; set; }

    #endregion

    private void OnStart()
    {
      IntializeWorker();
      Initilize();

      _queue.EnqueueEntity("Work is starting off!");

      _worker.RunWorkerAsync();
    }

    private void OnCancel()
    {
      _queue.EnqueueEntity("Work is Cannlling!");
      _worker.CancelAsync();
    }

    private void OnClose()
    {
      _worker.DoWork -= new DoWorkEventHandler(_worker_DoWork);
      _worker.ProgressChanged -= new ProgressChangedEventHandler(_worker_ProgressChanged);
      _worker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(_worker_RunWorkerCompleted);

      _queue.Close();
    }
  }

  public class ProgressVM : NotificationObject
  {

    private String _caption;
    public String Caption
    {
      get { return _caption; }
      set
      {
        if (_caption == value)
          return;
        _caption = value;
        this.RaisePropertyChanged(() => Caption);
      }
    }

    private int _value;
    public int Value
    {
      get { return _value; }
      set
      {
        if (_value == value)
          return;
        _value = value;
        RaisePropertyChanged(() => Value);
      }
    }


    public int Maximum { get; set; }

    public int Minimum { get; set; }
  }
}
