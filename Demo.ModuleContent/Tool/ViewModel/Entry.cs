using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;

namespace Demo.Content
{
    public class Entry:NotificationObject
    {
        public string LanguageName { get; set; }

        private string _group = "default";
        public string Group
        {
            get
            {
                return _group;
            }
            set
            {
                _group = value;
            }
        }

        private string _groupName = "default";
        public string GroupName
        {
            get
            {
                return _groupName;
            }
            set
            {
                _groupName = value;
            }
        }

        private string _item = string.Empty;
        public event EventHandler ItemChanged;
        public string Item
        {
            get { return _item; }
            set
            {
                if (_item == value)
                    return;
                _item = value;
                RaisePropertyChanged("Item");
                OnItemChanged();
            }
        }
        private void OnItemChanged()
        {
            var handler = ItemChanged;
            if (handler != null)
                handler(this, null);
        }

        private string _type = "str";
        public string Type { get { return _type; } set { _type = value; } }

        private string _targetValue = string.Empty;
        public event EventHandler TargetValueChanged;
        public string TargetValue
        {
            get
            {
                return _targetValue;
            }
            set
            {
                string oldValue = _targetValue;
                _targetValue = value;
                RaisePropertyChanged("TargetValue");
                OnTargetValueChanged();
            }
        }

        private void OnTargetValueChanged()
        {
            var handler = TargetValueChanged;
            if (handler != null)
                handler(this, null);
        }


        private string _sourceValue = string.Empty;
        public event EventHandler SourceValueChanged;
        public string SourceValue
        {
            get { return _sourceValue; }
            set
            {
                if (_sourceValue == value)
                    return;
                _sourceValue = value;
                RaisePropertyChanged("SourceValue");
                OnSourceValueChanged();
            }
        }
        private void OnSourceValueChanged()
        {
            var handler = SourceValueChanged;
            if (handler != null)
                handler(this, null);
        }
    }
}
