using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ComponentModel.Composition;
using Library.Common.Extension;

namespace Demo.Content
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ReportFieldTypeAdapterFactory 
    {
        [ImportMany(AllowRecomposition=true)]
        public Lazy<IReportFieldDataAdatper>[] ReportFieldTypeAdapters { get; set; }

        public IReportFieldDataAdatper GetReportFieldAdatper(Type type)
        {
            if (this.ReportFieldTypeAdapters.IsNullOrEmpty())
                return null;

            var lazyAdapter = this.ReportFieldTypeAdapters.FirstOrDefault(adpater =>
                {
                    if (adpater.Value.IsNull())
                        return false;
                    return adpater.Value.ReportFieldType == type;
                });

            if (lazyAdapter.Value.IsNull())
                return null;
            return lazyAdapter.Value;
        }

        public IReportFieldDataAdatper GetReportFieldAdatper(string typeName)
        {
            if (this.ReportFieldTypeAdapters.IsNullOrEmpty())
                return null;

            var lazyAdapter = this.ReportFieldTypeAdapters.SingleOrDefault(adpater =>
            {
                if (adpater.Value.IsNull())
                    return false;
                return adpater.Value.GetReportTypeString() == typeName;
            });

            if (lazyAdapter.Value.IsNull())
                return null;
            return lazyAdapter.Value;
        }

        public IReportFieldDataAdatper GetReportFieldDataAdapter(Type type)
        {
            if (this.ReportFieldTypeAdapters.IsNullOrEmpty())
                return null;

            var lazyAdapter = this.ReportFieldTypeAdapters.FirstOrDefault(adpater =>
            {
                if (adpater.Value.IsNull())
                    return false;
                return adpater.Value.ReportFieldType == type;
            });

            if (lazyAdapter.Value.IsNull())
                return null;
            return lazyAdapter.Value;
        }
    }

}
