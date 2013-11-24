using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Regions;
using System.ComponentModel.Composition;

namespace Demo.Infrastructure
{
    [Export(typeof(AutoPopulateExportedViewsBehavior))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AutoPopulateExportedViewsBehavior : RegionBehavior, IPartImportsSatisfiedNotification
    {

        #region IPartImportsSatisfiedNotification Members

        public void OnImportsSatisfied()
        {
            AddRegisterViews();
        }

        #endregion

        protected override void OnAttach()
        {
            AddRegisterViews();
        }

        protected void AddRegisterViews()
        {
            if (this.Region == null)
                return;

            if (this.RegisteredViews == null||!this.RegisteredViews.Any())
                return;

            foreach (var registeredView in this.RegisteredViews)
            {
                if (registeredView.Metadata.RegionName == this.Region.Name)
                {
                    var view = registeredView.Value;
                    if (!this.Region.Views.Contains(view))
                        this.Region.Add(view);

                    break;
                }
            }
        }

        [ImportMany(AllowRecomposition=true)]
        public Lazy<object,IViewRegionRegistration>[] RegisteredViews { get; set; }
    }
}
