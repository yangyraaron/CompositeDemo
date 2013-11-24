using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using System.ComponentModel;
using System.Linq.Expressions;
using Demo.Infrastructure;
using Microsoft.Practices.Prism.Commands;
using System.Xml.Serialization;
using Library.Common.Extension;
using System.Collections.ObjectModel;

namespace Demo.Content
{
    enum PropertyWindowState
    {
        Add,
        Edit
    }

    public class ReportFieldInteractionVM : ConfirmationNotificationObject
    {
        private PropertyWindowState _state;
        private ReportFieldsVM _parent;
        private ReportFieldTypeAdapterFactory _fieldAdapterFactory;
        public ReportFieldTypeAdapterFactory FieldAdapterFactory
        {
            get { return _fieldAdapterFactory; }
            set { _fieldAdapterFactory = value; }
        }

         public ReportFieldInteractionVM(ReportFieldTypeAdapterFactory factory,
            ObservableCollection<ReportFieldVM> groups, ReportFieldsVM parent)
        {
            _state = PropertyWindowState.Add;
            Groups = groups;
            _parent = parent;

            this._reportField = new ReportFieldVM { FieldType = parent.ReportFieldType};
            this.ReportFieldTypes = new[] { typeof(string), typeof(int), typeof(DateTime), typeof(decimal) };
            this.ReportField.OriginalFieldType = this.ReportFieldTypes.FirstOrDefault();
            this._fieldAdapterFactory = factory;

            SaveCommand = new DelegateCommand<object>(x => Save());
            CancelCommand = new DelegateCommand<object>(x => { Result = false; });
        }

         public ReportFieldInteractionVM(ReportFieldTypeAdapterFactory factory, ReportFieldVM child,
             ObservableCollection<ReportFieldVM> groups, ReportFieldsVM parent)
         {
             _state = PropertyWindowState.Edit;
             Groups = groups;
             _parent = parent;

             this._reportField = child;
             this.ReportFieldTypes = new[] { typeof(string), typeof(int), typeof(DateTime), typeof(decimal) };

             this._fieldAdapterFactory = factory;
             SaveCommand = new DelegateCommand<object>(x => Save());
             CancelCommand = new DelegateCommand<object>(x => { Result = false; });
         }
      
        public IList<Type> ReportFieldTypes { get; set; }
                
        private ReportFieldVM _reportField;
        public ReportFieldVM ReportField
        {
            get
            { return _reportField; }
            
        }

        private ObservableCollection<ReportFieldVM> _groups;
        public ObservableCollection<ReportFieldVM> Groups
        {
            get { return _groups; }
            set { _groups = value;
            this.RaisePropertyChanged(() => this.Groups);
            }
        }

        public FieldType ReportFieldType
        {
            get { return _parent.ReportFieldType; }
        }
                
        private bool? _result;
        public bool? Result 
        {
            get { return _result; }
            set {
                _result = value;
            this.RaisePropertyChanged(() => this.Result);
            }
        }

        public DelegateCommand<object> SaveCommand { get; set; }

        public DelegateCommand<object> CancelCommand { get; set; }

        private void Save()
        {
            this.Confirmed = true;

            string fieldType = string.Empty;

            var adatper = this._fieldAdapterFactory.GetReportFieldAdatper(this.ReportField.OriginalFieldType);
            if (adatper != null)
                fieldType = adatper.GetReportTypeString();
            else
                fieldType = this.ReportField.OriginalFieldType.Name;

            this._reportField.ReportFieldType = fieldType;

            Result = true;
        }
    }
}
