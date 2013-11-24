using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Demo.Infrastructure;
using System.Xml.Linq;
using Library.Client.Wpf;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.ViewModel;
using System.IO;
using System.Threading;
using Library.Client.Wpf;
using Microsoft.Practices.Prism.Commands;

namespace Demo.Content
{
    [Export]
    public class ReportParametersGeneratorViewModel:NotificationObject
    {
        private string _reportFileFolder;

        public DelegateCommand<object> GenerateCommand { get; set; }

        public ReportParametersGeneratorViewModel()
        {
            _reportFileFolder = ConfigurationMgr.Current.GetParamValue("ReportFileFolder");
            GenerateCommand = new DelegateCommand<object>(GenerateContent, CanGenerate);

            this.Reports = GetReports();
        }

        private dynamic _report;
        public dynamic Report
        { get { return _report; }
            set { _report = value;
            this.RaisePropertyChanged(() =>  this.Report);
            GenerateCommand.RaiseCanExecuteChanged();
            }
        }

        private string _content;
        public string ParametersContent
        { get{return _content;}
            set { _content = value;
            RaisePropertyChanged(()=>this.ParametersContent);
            }
        }

        public IEnumerable<dynamic> Reports { get; set; }

        private bool _isGenerating;
        public bool IsGenerating {
            get { return _isGenerating; }
            set { _isGenerating = value;
            this.RaisePropertyChanged(() => this.IsGenerating);
            }
        }

        private bool CanGenerate(object obj)
        {
            if (this.Report == null)
                return false;
            return !string.IsNullOrEmpty(this.Report.Path);
        }

        protected void GenerateContent(object obj)
        {
            IsGenerating = true;

            string path = this.Report.Path;

            //AsyncExcutor.Process<string, string>(
            //    content => { this.ParametersContent = content; },
            //    parameter => { Thread.Sleep(1000);
            //        return this.CreateParametersContent(parameter); },
            //    path);

            this.ParametersContent = CreateParametersContent(this.Report.Path);


            IsGenerating = false;
        }

        protected XDocument LoadXDoc(string reportPath)
        {
            if (string.IsNullOrEmpty(_reportFileFolder))
                return null;

            if (string.IsNullOrEmpty(reportPath))
                return null;

            return XDocument.Load(reportPath);
        }

        protected IEnumerable<string> GetAllParameters(XDocument xdoc)
        {
            if (xdoc == null)
                return null;

            XNamespace ns = @"http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition";
            var query = from element in xdoc.Descendants(ns + "ReportParameter")
                        select element.Attribute("Name").Value;

            return query;
        }

        protected string CreateParametersContent(string reportPath)
        {
            XDocument xdoc = LoadXDoc(reportPath);

            StringBuilder sb = new StringBuilder();
            sb.Append("<Parameters>\r\n");

            IEnumerable<string> parameters = GetAllParameters(xdoc);
            foreach (string parameter in parameters)
            {
                sb.Append(string.Format("<Parameter Name='{0}'>\r\n", parameter));
                sb.Append(string.Format("<Value>=Parameters!{0}.Value</Value>\r\n", parameter));
                sb.Append("</Parameter>\r\n");
            }

            sb.Append("</Parameters>\r\n");

            return sb.ToString();
        }

        private IEnumerable<dynamic> GetReports()
        {
            if (!Directory.Exists(this._reportFileFolder))
                return null;

            IEnumerable<string> paths = Directory.GetFiles(this._reportFileFolder,"*.rdl",SearchOption.TopDirectoryOnly);

            return paths.Select(x =>
            {
                var file = new FileInfo(x);
                return new 
                {
                    Name = file.Name,
                    Path = file.FullName
                };
            });
        }
    }
}
