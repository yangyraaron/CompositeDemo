using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Prism.Commands;
using System.ComponentModel.Composition;
using Demo.Service.Reports;
using Library.Client.Wpf;
using System.Windows.Data;
using System.ComponentModel;

namespace Demo.Content
{
    [Export]
    public class ReportDataTestVM:NotificationObject
    {
        private ReportsClient _proxy;

        public ReportDataTestVM()
        {
            LoadDataCommand = new DelegateCommand<object>(LoadData);
        }

     
        private ICollectionView _groupList;
        public ICollectionView GroupList
        {
            get { return _groupList; }
            set
            {
                if (_groupList == value)
                    return;
                _groupList = value;

                _groupList.GroupDescriptions.Add(
                    new PropertyGroupDescription("Group"));

                RaisePropertyChanged(() => GroupList);
            }
        }    
                

        private String _factory = "01";
        public String Factory
        {
            get { return _factory; }
            set
            {
                if (_factory == value)
                    return;
                _factory = value;
                this.RaisePropertyChanged(() => Factory);
            }
        }    
                
        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (_isLoading == value)
                    return;
                _isLoading = value;
                this.RaisePropertyChanged(() => IsLoading);
            }
        }    
                

        public DelegateCommand<object> LoadDataCommand { get; set; }

        private void LoadData(object obj)
        {
            try
            {
                _proxy = new ReportsClient();
                _proxy.Open();

                this.IsLoading = true;

                _proxy.BeginGetJobOutputVarianceList(
                    new JobOutputVarianceRequest
                    {
                        ContextString = @"<?xml version='1.0' encoding='utf-16'?>
                                            <context>
                                            <__env>RRS_ROCK_QA</__env>
                                            <__lang>en-US</__lang>
                                            <__user>G3/1COit</__user>
                                            <__domain>KSvD</__domain>
                                            <__appuser>HkJGUDbS0Q==</__appuser>
                                            <__sessid />
                                            <__kvp key='RrsModule::UserName'>demomgr</__kvp>
                                            <__kvp key='RrsModule::CompanyCode'>1</__kvp>
                                            </context>",
                        Factory = this.Factory,
                        PeriodType = 1
                    },
                    (ar) =>
                    {
                        if (ar.IsCompleted)
                        {


                            this.GroupList = new ListCollectionView(_proxy.EndGetJobOutputVarianceList(ar).Select
                                (
                                x => { return new JobVariance(x); }
                                ).ToList<dynamic>());
                            
                            _proxy.Close();
                            this.IsLoading = false;
                        }
                                    }
                                    ,
                                    null
                    );

              
            }
            catch
            {
                this.IsLoading = false;
            }
        }
                
    }
}
