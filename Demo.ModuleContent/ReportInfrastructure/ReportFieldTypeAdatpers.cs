using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace Demo.Content
{
    [Export(typeof(IReportFieldDataAdatper))]
    public class ReportFieldIntegerAdatpers:IReportFieldDataAdatper
    {
        #region IReportFieldTypeAdapter Members

        public string GetReportTypeString()
        {
            return "Integer";
        }

        public Type ReportFieldType
        {
            get { return typeof(int); }
        }

        #endregion

        #region IReportFieldDataAdatper<int> Members


        public dynamic GetData()
        {
            return RandomValueFactory.CreateRandomInt();
        }

        #endregion
    }

    [Export(typeof(IReportFieldDataAdatper))]
    public class ReportFieldStringAdapter : IReportFieldDataAdatper
    {
        #region IReportFieldDataAdatper Members

        public dynamic GetData()
        {
            return RandomValueFactory.CreateRandomString(isLowCase:true);
        }

        #endregion

        #region IReportFieldTypeAdapter Members

        public Type ReportFieldType
        {
            get { return typeof(string); }
        }

        public string GetReportTypeString()
        {
            return "String";
        }

        #endregion
    }


    [Export(typeof(IReportFieldDataAdatper))]
    public class ReportFieldDateTimeAdapter : IReportFieldDataAdatper
    {
        #region IReportFieldTypeAdapter Members

        public string GetReportTypeString()
        {
            return "Date";
        }

        public Type ReportFieldType
        {
            get { return typeof(DateTime); }
        }

        #endregion

        #region IReportFieldDataAdatper<DateTime> Members


        public dynamic GetData()
        {
            return DateTime.FromOADate(RandomValueFactory.CreateRandomDouble());
        }

        #endregion
    }

     [Export(typeof(IReportFieldDataAdatper))]
    public class ReportFieldDecimalAdapter : IReportFieldDataAdatper
    {
        #region IReportFieldTypeAdapter Members

        public string GetReportTypeString()
        {
            return "Decimal";
        }

        public Type ReportFieldType
        {
            get { return typeof(decimal); }
        }

        #endregion

        #region IReportFieldDataAdatper<decimal> Members

        public dynamic GetData()
        {
            return RandomValueFactory.CreateRandomDecimal();
        }

        #endregion
    }
}
