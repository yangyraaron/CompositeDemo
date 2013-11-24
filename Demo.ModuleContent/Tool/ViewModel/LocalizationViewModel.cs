using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Collections.ObjectModel;
using Demo.Infrastructure;
using Microsoft.Practices.Prism.ViewModel;
using Library.Client.Wpf;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Threading;
using Library.Common.Extension;
using System.Threading.Tasks;

namespace Demo.Content
{
    [Export]
    public class LocalizationViewModel:LocalizationModel
    {
        private DispatcherTimer _timer;

        public LocalizationViewModel()
        {
            InitialModel();

            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0,0,1);
            _timer.Tick += new EventHandler(timer_Tick);
        }

        private ObservableCollection<Entry> OriginalEntries
        {
            get
            {
                var entries = DataManager.Current.GetItem
                    <string, ObservableCollection<Entry>>
                        ("Original-Entries");
                return entries;
            }
        }

        private ObservableCollection<Entry> _filtedEntries = new ObservableCollection<Entry>();
        public ObservableCollection<Entry> FiltedEntries
        {
            get
            {
                return _filtedEntries;
            }
        }


        private LocManager _defaultLocMgr;
        private LocManager _targetLocMgr;

        public void InitialModel()
        {
            LoadOriginalEntries();
            LoadCommand = new DelegateCommand<object>(ExcuteLoad, CanExcuteLoad);
        }

        protected void LoadOriginalEntries()
        {
            _defaultLocMgr = new LocManager(ConfigurationMgr.Current.GetParamValue(Constant.FILE_FOLDER_KEY),Constant.DEFAULT_LOCFILE);
            //FireHandler(Excuting, null, null);
            LoadingStatus = true;

            TaskScheduler schedular = TaskScheduler.FromCurrentSynchronizationContext();
            Task<IEnumerable<Entry>> task = Task.Factory.StartNew(() => this._defaultLocMgr.GetAllEntries());

            task.ContinueWith(t =>
            {
                DataManager.Current.AddItem<string, ObservableCollection<Entry>>
                    ("Original-Entries", new ObservableCollection<Entry>(t.Result));

                LoadFiltedEntries(null, () => { LoadingStatus = false; });
            }, schedular);
            //AsyncExcutor.Process<IEnumerable<Entry>, LocManager>(
            //    x =>
            //    {
            //        DataManager.Current.AddItem<string, ObservableCollection<Entry>>
            //         ("Original-Entries", new ObservableCollection<Entry>(x));

            //        LoadFiltedEntries(null, () => { LoadingStatus = false; });// FireHandler(Excuted, null, null); });

            //    },
            //    y => { return y.GetAllEntries(); }
            //    , _defaultLocMgr);
        }

        protected void LoadFiltedEntries(Action beforeHandler, Action endHandler)
        {
            this._timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            this.LoadingStatus = true;

            if (string.IsNullOrWhiteSpace(this.FilterItem))
            {
                this.FiltedEntries.Clear();
                this.OriginalEntries.ToList().ForEach(x => { this.FiltedEntries.Add(x); });
            }
            else
            {
                var filteredEntries = this.OriginalEntries.Where(
                    x =>
                    {
                        return x.Item.ToLower().Trim().Contains(this.FilterItem.ToLower().Trim());
                    });

                this.FiltedEntries.Clear();
                if (!filteredEntries.IsNullOrEmpty())
                    filteredEntries.ToList().ForEach(x => { this.FiltedEntries.Add(x); });
            }

            this._timer.Stop();
            this.LoadingStatus = false;
        }

        private string _sourceLocName = Constant.DEFAULT_LOCFILE;
        public string SourceLocName
        {
            get { return _sourceLocName; }
            set
            {
                _sourceLocName = value;
                RaisePropertyChanged("SourceLocName");
            }
        }

        private string _filterItem = string.Empty;
        public string FilterItem
        {
            get { return _filterItem; }
            set
            {
                _filterItem = value;
                RaisePropertyChanged("FilterItem");
                OnFilterItemChanged();
            }
        }

        private bool _isAdd = false;
        public bool IsAdd
        {
            get { return _isAdd; }
            set
            {
                _isAdd = value;
                RaisePropertyChanged("IsAdd");
            }
        }

      
        private void OnFilterItemChanged()
        {

            LoadFiltedEntries(() => { LoadingStatus = true; },
            () => { LoadingStatus = false; });
            RaisePropertyChanged("FiltedEntries");

        }

        public DelegateCommand<Object> LoadCommand { get; set; }

        private bool CanExcuteLoad(object obj)
        {
            if (_targetLanguage == null)
                return false;
            if (string.IsNullOrEmpty(_targetLanguage.Name))
                return false;

            return true;
        }

        private bool _loadingStatus;
        public bool LoadingStatus
        {
            get { return _loadingStatus; }
            set
            {
                _loadingStatus = value;
                RaisePropertyChanged("LoadingStatus");
            }
        }

        private void ExcuteLoad(Object obj)
        {
            if (_targetLanguage.Name == string.Empty)
                return;
            if (_targetLocMgr == null || _targetLocMgr.FileName != _targetLanguage.Name)
                _targetLocMgr = new LocManager(ConfigurationMgr.Current.GetParamValue(Constant.FILE_FOLDER_KEY), _targetLanguage.Name);
            
            LoadingStatus = true;

            TaskScheduler schedular = TaskScheduler.FromCurrentSynchronizationContext();

            var task = Task.Factory.StartNew(() => this._targetLocMgr.GetAllEntries());

            task.ContinueWith(t => {
                OriginalEntries.ToList().ForEach(x =>
                {
                    Entry seletedEntry = t.Result.SingleOrDefault(y =>
                    {
                        return y.Item.Trim() == x.Item.Trim();
                    });

                    if (seletedEntry == null)
                    {
                        x.TargetValue = string.Empty;
                    }
                    else
                        x.TargetValue = seletedEntry.SourceValue;

                });


                LoadingStatus = false;
            });

          //  AsyncExcutor.Process<IEnumerable<Entry>, LocManager>(
          //      targetEntries =>
          //      {
          //          OriginalEntries.ToList().ForEach(x =>
          //          {
          //              Entry seletedEntry = targetEntries.SingleOrDefault(y =>
          //              {
          //                  return y.Item.Trim() == x.Item.Trim();
          //              });

          //              if (seletedEntry == null)
          //              {
          //                  x.TargetValue = string.Empty;
          //              }
          //              else
          //                  x.TargetValue = seletedEntry.SourceValue;

          //          });


          //          LoadingStatus = false;
          //      },

          //y => { return y.GetAllEntries(); }, _targetLocMgr);

        }
    }
}
