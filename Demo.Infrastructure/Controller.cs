using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Regions;

namespace Demo.Infrastructure
{
    public abstract class Controller:IController
    {
        protected abstract IEnumerable<object> Views { get; }

        protected abstract string RegionName { get; }

        private IRegionManager _regionMgr;

        public Controller(IRegionManager regionMgr)
        {
            _regionMgr = regionMgr;
        }

        #region IController Members

        public virtual void ActiveView(string viewName)
        {
            if(Views == null || !Views.Any())
                return;

            string viewKey = string.Format("{0}View", viewName);

            var view = Views.FirstOrDefault(
                xv =>
                {
                    return xv.GetType().Name == viewKey;
                }
                );

            if (view == null)
                return;

            AddOrActiveView(view);
        }

        #endregion

        private void AddOrActiveView(object view)
        {
            var region = _regionMgr.Regions[RegionName];

            if (!region.Views.Contains(view))
                region.Add(view);

            region.Activate(view);
        }

    }
}
