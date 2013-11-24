using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Demo.Infrastructure;
using Microsoft.Practices.Prism.Regions;

namespace Demo.Content.Test
{
    [Export(typeof(IController))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class TestController:Controller
    {
        [ImportMany(ContentConstant.MODULE_TEST)]
        public Lazy<object>[] TestViews;

        private IRegionManager _regionMgr;

        [ImportingConstructor]
        public TestController(IRegionManager regionMgr):base(regionMgr)
	    {
            _regionMgr = regionMgr;
	    }

        protected override string RegionName
        {
            get { return RegionNames.ContentRegion; }
        }

        protected override IEnumerable<object> Views
        {
            get {
                if (TestViews == null || !TestViews.Any()
                    )
                    return null;

                return TestViews.Select(x => x.Value);
            }
        }
    }
}
