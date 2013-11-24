using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using System.ComponentModel.Composition;
using Library.Common.Extension;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Demo.Content.Tool
{
    [Export]
    public class LuckyFiveViewModel:NotificationObject,IDataErrorInfo
    {
        public LuckyFiveViewModel()
        {
            this.Numbers = new ObservableCollection<int>();
        }

        private ListSelector<int> _firstSelector;
        public ListSelector<int> FirstSelector
        {
            get
            {
                if (this._firstSelector == null)
                {
                    this._firstSelector = new ListSelector<int>(_firstRange);
                    this._firstSelector.SelectedValueChangedCallback = v => OnValueChanged(v);
                }
                return _firstSelector;
            }
        }

        private ListSelector<int> _secondSelector;
        public ListSelector<int> SecondSelector
        {
            get
            {
                if (this._secondSelector == null)
                {
                    this._secondSelector = new ListSelector<int>(_secondRange);
                    this._secondSelector.SelectedValueChangedCallback = v => OnValueChanged(v);
                }
                return _secondSelector;
            }
        }

        private ListSelector<int> _thirdSelector;
        public ListSelector<int> ThirdSelector
        {
            get
            {
                if (this._thirdSelector == null)
                {
                    this._thirdSelector = new ListSelector<int>(_thirdRange);
                    this._thirdSelector.SelectedValueChangedCallback = v => OnValueChanged(v);
                }
                return _thirdSelector;
            }
        }

        private ListSelector<int> _fourthSelector;
        public ListSelector<int> FourthSelector
        {
            get
            {
                if (this._fourthSelector == null)
                {
                    this._fourthSelector = new ListSelector<int>(_fourthRange);
                    this._fourthSelector.SelectedValueChangedCallback = v => OnValueChanged(v);
                }
                return _fourthSelector;
            }
        }


        private ListSelector<int> _fifthSelector;
        public ListSelector<int> FifthSelector
        {
            get
            {
                if (this._fifthSelector == null)
                {
                    this._fifthSelector = new ListSelector<int>(_fifthRange);
                    this._fifthSelector.SelectedValueChangedCallback = v => OnValueChanged(v);
                }
                return _fifthSelector;
            }
        }

        private ListSelector<int> _sixthSelector;
        public ListSelector<int> SixthSelector
        {
            get
            {
                if (this._sixthSelector == null)
                {
                    this._sixthSelector = new ListSelector<int>(_sixthRange);
                    this._sixthSelector.SelectedValueChangedCallback = v => OnValueChanged(v);
                }
                return _sixthSelector;
            }
        }

        private ListSelector<int> _seventhSelector;
        public ListSelector<int> SeventhSelector
        {
            get
            {
                if (this._seventhSelector == null)
                {
                    this._seventhSelector = new ListSelector<int>(_seventhRange);
                    this._seventhSelector.SelectedValueChangedCallback = v => OnValueChanged(v);
                }
                return _seventhSelector;
            }
        }

        int[] _firstRange = new  int[]{1,2,3,4,5};
        int[] _secondRange = new int[] { 6, 7, 8, 9, 10 };
        int[] _thirdRange = new int[] { 11, 12, 13, 14, 15 };
        int[] _fourthRange = new int[] { 16, 17, 18, 19, 20 };
        int[] _fifthRange = new int[] { 21, 22, 23, 24, 25 };
        int[] _sixthRange = new int[] { 26, 27, 28, 29, 30 };
        int[] _seventhRange = new int[] { 31, 32, 33, 34, 35 };

        //1:3奇2偶+2奇3偶 2: 80至121

        public void OnValueChanged(int value)
        {

            var seqs = this.Numbers.ToList();

            if(seqs.Count == 5)
                seqs.RemoveAt(0);

            seqs.Add(value);
            this.Numbers= seqs.OrderBy(s => s).ToList();

            Total = this.Numbers.Sum();
            CaculateRate();
        }

        private void CaculateRate()
        {
            int j = 0;
            int o = 0;

            foreach (int i in this.Numbers)
            { 
                int r = 0;
                Math.DivRem(i, 2, out r);

                if (r == 0)
                    o++;
                else
                    j++;
            }

            this.Rate = string.Format("{0}:{1}", j, o);
        }

        private IEnumerable<int> _numbers;
        public IEnumerable<int> Numbers
        {
            get { return _numbers; }
            set { _numbers = value;
            this.RaisePropertyChanged(() => Numbers);
            }
        }

        private int _total;
        public int Total
        {
            get { return _total; }
            set { _total = value;
            this.RaisePropertyChanged(() => Total);
            }
        }

        private string _rate;
        public string Rate
        {
            get { return _rate; }
            set { _rate = value;
            this.RaisePropertyChanged(() => Rate);
            }
        }

        #region IDataErrorInfo Members

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Total")
                {
                    if (Total < 80 || Total > 121)
                        return "total should be between 80 and 121";
                    return null;
                }

                return null;
            }
        }

        #endregion
    }

    public class ListSelector<T>:NotificationObject
    {

        public ListSelector(IEnumerable<T> list)
        {
            this.List = list;
        }

        private IEnumerable<T> _list;
        public IEnumerable<T> List
        {
            get
            {
                return _list;
            }
            private set { _list = value;
            this.RaisePropertyChanged(() => List);
            }
        }

        private T _selectedValue;
        public T SelectedValue
        {
            get { return _selectedValue; }
            set { _selectedValue = value;
            this.RaisePropertyChanged(() => SelectedValue);
            SelectedValueChangedCallback.SafeFire(value);
            }
        }

        public Action<T> SelectedValueChangedCallback { get; set; }
    }
}
