using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Commands;
using System.Threading.Tasks;
using System.Threading;
using Library.Common;

namespace Demo.Content.Test
{
    [Export]
    public class CoroutineViewModel : NotificationObject
    {
        public CoroutineViewModel()
        {
            this.Message = new ObservableCollection<string>();
            this.BeginCommand = new DelegateCommand<object>((o) => Begin());
            this.StartCommand = new DelegateCommand<object>(o => StartParallel());
        }

        public ObservableCollection<String> Message
        {
            get;
            private set;
        }

        public DelegateCommand<object> BeginCommand { get; set; }
        public DelegateCommand<object> StartCommand { get; set; }
        private void Begin()
        {
            this.Message.Clear();

            Coroutine.Start(CreateTasks(),
                                   (rs) =>
                                   {
                                       this.Message.Add("Completed!");
                                   },
                                   (e) =>
                                   {
                                       this.IsBusy = false;
                                       Message.Add(e.Message);
                                   });
        }

        private IEnumerable<IResult> CreateTasks()
        {
            this.Message.Add("1 starts!");
            yield return CreateResult(1);
            this.Message.Add("1 finished!");

            this.ExecutingMessage = "2 starts...";
            this.IsBusy = true;
            yield return CreateResultAsync(2,1000);//suspend

            this.IsBusy = false;
            this.Message.Add("2 finished!");

            this.Message.Add("3 starts!");
            yield return CreateResult(3);
            this.Message.Add("3 finished!");

            this.ExecutingMessage = "4 starts...";
            this.IsBusy = true;
            yield return CreateResultAsync(4,1000);
            this.IsBusy = false;
            this.Message.Add("4 finished!");

            this.Message.Add("5 starts!");
            yield return CreateResult(5);
            this.Message.Add("5 finished!");

            this.ExecutingMessage = "6 starts...";
            this.IsBusy = true;
            yield return CreateResultAsync(6,1000);
            this.IsBusy = false;
            this.Message.Add("6 finished!");
        }

        private Result<string> CreateResult(int index)
        {
            var result = new Result<string>(() => index.ToString());
            return result;
        }

        private AsyncResult<string> CreateResultAsync(int index,int time)
        {
            Func<string> func = () =>
             {
                 //if (index == 4 || index == 2 || index == 6)
                 //    throw new Exception("exception test");

                 Thread.Sleep(time);
                 return index.ToString();
             };
            var result =  new AsyncResult<string>(func);

            return result;
        }

        void result_Completed(object sender, ResultCompletionArgs e)
        {
            this.IsBusy = false;
            this.Message.Add(e.Result.ToString());
        }

        private void StartParallel()
        {
            this.Message.Clear();
            this.ExecutingMessage = "Parallel starts...";
            this.IsBusy = true;

            Coroutine.ParallelStart(CreateParallelTasks(),
                (r) =>
                {
                    this.IsBusy = false;

                    foreach (var str in r)
                        Message.Add(str.ToString());
                },
                (e) => { this.IsBusy = false; Message.Add(e.Message); });
        }

        private IEnumerable<IResult> CreateParallelTasks()
        {
            yield return CreateResultAsync(1, 6000);
            yield return CreateResultAsync(2, 6000);
            yield return CreateResultAsync(3, 6000);
            yield return CreateResultAsync(4, 6000);
            yield return CreateResultAsync(5, 6000);
            yield return CreateResultAsync(6, 6000);
        }


        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value;
            this.RaisePropertyChanged(() => this.IsBusy);
            }
        }

        

        private String _ExecutingMessage;
        public String ExecutingMessage
        {
            get { return _ExecutingMessage; }
            set
            {
                if (_ExecutingMessage == value)
                    return;
                _ExecutingMessage = value;
                this.RaisePropertyChanged(() => ExecutingMessage);
            }
        }    
                

    }
}
