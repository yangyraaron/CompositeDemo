using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using System.ComponentModel.Composition;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Commands;
using System.ComponentModel.Composition.Hosting;
using Library.Common.Extension;
using System.Xml.Serialization;
using Demo.Infrastructure;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Specialized;
using Demo.Content.Tool.Data;
using System.Windows.Input;
using System.Windows;

namespace Demo.Content
{
    [Export]
    public class ReportDataManagementVM:NotificationObject
    {
        private ReportFieldDataModel _dataModel;

        public ReportFieldsVM ReportFields { get; private set; }
        public ReportFieldsVM GroupFeilds { get; private set; }

        [Import]
        public ReportFieldTypeAdapterFactory FieldTypeAdapterFactory{get;set;}
        [Import]
        public ReportQueryCreator QueryCreator { get; set; }


        public ReportDataManagementVM()
        {
            _dataModel = new ReportFieldDataModel();
            ReportFields = new ReportFieldsVM(this);
            GroupFeilds = new ReportFieldsVM(this,FieldType.Group);

            _addReportFieldRequest = new InteractionRequest<ReportFieldInteractionVM>();

            SaveFieldsCommand = new DelegateCommand<object>(x => SaveFieldsToXaml(), x => CanLoadFields());
            LoadFieldsCommand = new DelegateCommand<object>(x => LoalFields(), x => CanLoadFields());
            CreateQueryCommand = new DelegateCommand<object>(x => CreateQuery(), x => CanCreateQuery());
            
        }

        private InteractionRequest<ReportFieldInteractionVM> _addReportFieldRequest;
        public InteractionRequest<ReportFieldInteractionVM> AddReportFieldRequest
        {
            get { return this._addReportFieldRequest; }
        }

        private string _reporName;
        public string ReportName 
        {
            get { return _reporName; }
            set
            {
                if (_reporName == value)
                    return;
                _reporName = value;
                this.RaisePropertyChanged(() => this.ReportName);

                SaveFieldsCommand.RaiseCanExecuteChanged();
                LoadFieldsCommand.RaiseCanExecuteChanged();
                CreateQueryCommand.RaiseCanExecuteChanged();
                GroupFeilds.AddItemCommand.RaiseCanExecuteChanged();
                ReportFields.AddItemCommand.RaiseCanExecuteChanged();
            }
        }

        private String _query;
        public String Query
        {
            get { return _query; }
            set
            {
                if (_query == value)
                    return;
                _query = value;
                this.RaisePropertyChanged(() => Query);
            }
        }

        private String _dataCount;
        public String DataCount
        {
            get { return _dataCount; }
            set
            {
                if (_dataCount == value)
                    return;
                _dataCount = value;
                RaisePropertyChanged(() => DataCount);
            }
        }
                
                
        public DelegateCommand<object> SaveFieldsCommand { get; set; }
        public DelegateCommand<object> LoadFieldsCommand { get; set; }
        public DelegateCommand<object> CreateQueryCommand { get; set; }

       

        public void SendRequestToField(ReportFieldInteractionVM reportField)
        {
            this._addReportFieldRequest.Raise(
             reportField,
              confirmation =>
              {
                  if (!confirmation.Confirmed)
                      return;

                  if (string.IsNullOrEmpty(confirmation.ReportField.ReportFieldName) ||
                      confirmation.ReportField.ReportFieldType == null)
                      return;

                  if(!this.ReportFields.Items.Any(x=>{return x.ReportField.ReportFieldName==confirmation.ReportField.ReportFieldName;}))
                      this.ReportFields.Items.Add(confirmation);

              });
        }

        public void SendRequestToGroup(ReportFieldInteractionVM reportField)
        {
            this._addReportFieldRequest.Raise(
           reportField,
            confirmation =>
            {
                if (!confirmation.Confirmed)
                    return;

                if (string.IsNullOrEmpty(confirmation.ReportField.ReportFieldName) ||
                    confirmation.ReportField.ReportFieldType == null)
                    return;

                if (!this.GroupFeilds.Items.Any(x => { return x.ReportField.ReportFieldName == confirmation.ReportField.ReportFieldName; }))
                    this.GroupFeilds.Items.Add(confirmation);

            });
        }

        private void SaveFieldsToXaml()
        {
            var groups = this.GroupFeilds.Items.Select(vm => vm.ReportField).ToList();
            var fields = this.ReportFields.Items.Select(vm => vm.ReportField).ToList();

            ReportDataContract report = new ReportDataContract { Fields = fields, Groups = groups };

            _dataModel.SaveFieldsToXaml<ReportDataContract>(this.ReportName,report);

            CreateQueryCommand.RaiseCanExecuteChanged();
        }

        private bool CanLoadFields()
        {
            return !this.ReportName.IsNullOrEmpty();
        }

        private void LoalFields()
        {
            var report = _dataModel.GetFieldsFromXml<ReportDataContract>(this.ReportName);

            this.GroupFeilds.Items.Clear();
            foreach (var field in report.Groups)
            {
                this.GroupFeilds.Items.Add(new ReportFieldInteractionVM(this.FieldTypeAdapterFactory, field, null,null));
            }

            var groups = new ObservableCollection<ReportFieldVM>(this.GroupFeilds.Items.Select(x => x.ReportField));

            foreach (var field in this.GroupFeilds.Items)
            {
                field.Groups = groups;
            }

            this.ReportFields.Items.Clear();
            foreach (var field in report.Fields)
            {
                this.ReportFields.Items.Add(new ReportFieldInteractionVM(this.FieldTypeAdapterFactory, field,
                    groups,
                    this.ReportFields));
            }

            CreateQueryCommand.RaiseCanExecuteChanged();
        }

        private void CreateQuery()
        {
            //var groups = this.GroupFeilds.Items.Select(x => x.ReportField).OrderBy(x => x.GroupLevel);
            var fields = this.ReportFields.Items.Select(x => x.ReportField).OrderBy(x => x.Group.ReportFieldName);

            //int length = ValueHelper.To<int>(this.DataCount, 10);

            //Query = QueryCreator.CreateQuery(fields, length);
        }

        private bool CanCreateQuery()
        {
            return this.ReportFields.Items.Any();
        }
    }
}
