using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Demo.Infrastructure;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using Library.Common.Extension;

namespace Demo.Content
{
    [Export(typeof(IController))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ToolController : IController
    {
        [ImportMany(ContentConstant.MODULE_TOOL)]
        public Lazy<object>[] Views;

        private IRegionManager _regionMgr;

        [ImportingConstructor]
        public ToolController (IRegionManager regionMgr)
	    {
            _regionMgr = regionMgr;
	    }

        #region IController Members

        public void ActiveView(string viewName)
        {
            string viewKey = string.Format("{0}View", viewName);

            if (!Views.Any())
                return;

            var view = Views.FirstOrDefault(
                xv => {
                    if (xv.Value.IsNull())
                        return false;
                    return xv.Value.GetType().Name == viewKey;
                }
                );

            if (view == null)
                return;

            AddOrActiveView(view.Value);
        }

        #endregion

        private void AddOrActiveView(object view)
        {
            var region = _regionMgr.Regions[RegionNames.ContentRegion];

            if (!region.Views.Contains(view))
                region.Add(view);

            region.Activate(view);
        }
    }
}
