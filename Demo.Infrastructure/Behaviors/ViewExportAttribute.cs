using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace Demo.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    [MetadataAttribute]
    public class ViewExportAttribute : ExportAttribute, IViewRegionRegistration
    {
        public ViewExportAttribute():base(typeof(object))
        {

        }

        public ViewExportAttribute(string viewName):
            base(viewName,typeof(object))
        {

        }

        #region IViewRegionRegistration Members

        public string RegionName
        {
            get;
            set;
        }

        #endregion
    }
}
