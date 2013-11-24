using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;

namespace Demo.Content
{
     [Serializable]
    public class ReportDataContract
    {
        public IEnumerable<ReportFieldVM> Groups { get; set; }

        public IEnumerable<ReportFieldVM> Fields { get; set; }
    }

    [Serializable]
    public class ReportFieldVM : NotificationObject
    {

        private string _fieldName;
        public string ReportFieldName
        {
            get { return _fieldName; }
            set
            {
                if (_fieldName == value)
                    return;
                _fieldName = value;
                this.RaisePropertyChanged(() => this.ReportFieldName);
            }
        }

        private string _fieldType;
        public string ReportFieldType
        {
            get { return _fieldType; }
            set
            {
                if (_fieldType == value)
                    return;
                _fieldType = value;
                this.RaisePropertyChanged(() => this.ReportFieldType);
            }
        }

        private Type _originalFieldType;
        public Type OriginalFieldType
        {
            get { return _originalFieldType; }
            set
            {
                if (_originalFieldType == value)
                    return;
                _originalFieldType = value;
                this.RaisePropertyChanged(() => this.OriginalFieldType);
            }
        }

        private int _groupSize;
        public int GroupSize
        {
            get { return _groupSize; }
            set
            {
                if (_groupSize == value)
                    return;
                _groupSize = value;
                RaisePropertyChanged(() => GroupSize);
            }
        }    
                
        private int _groupLevel = 1;
        public int GroupLevel
        {
            get { return _groupLevel; }
            set
            {
                if (_groupLevel == value)
                    return;
                _groupLevel = value;
                RaisePropertyChanged(() => GroupLevel);
            }
        }

        public FieldType FieldType
        {
            get;
            set;
        }


        private ReportFieldVM _group;
        public ReportFieldVM Group
        {
            get { return _group; }
            set
            {
                if (_group == value)
                    return;
                _group = value;
                RaisePropertyChanged(() => Group);
                OnGroupChanged();
            }
        }

        private void OnGroupChanged()
        {
            if(Group != null)
                GroupLevel = Group.GroupLevel;
        }



    }
}
