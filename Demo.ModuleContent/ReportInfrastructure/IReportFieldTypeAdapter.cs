using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.Content
{
    public interface IReportFieldTypeAdapter
    {
        Type ReportFieldType { get; }
        string GetReportTypeString();
    }

    public interface IReportFieldDataAdatper : IReportFieldTypeAdapter
    {
        dynamic GetData();
    }
}
