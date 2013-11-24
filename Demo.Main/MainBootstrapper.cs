using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.MefExtensions;
using System.Windows;
using System.ComponentModel.Composition.Hosting;
using Microsoft.Practices.Prism.Regions;
using Demo.Infrastructure;

namespace Demo.Main
{
   public class MainBootstrapper:MefBootstrapper
    {
       /// <summary>
       /// create a shell
       /// <remarks>
        /// make sure overrid the ConfigureAggregateCatalog method to add the 
        /// the catalog that contains the shell class
       /// </remarks>
       /// </summary>
       /// <returns></returns>
        protected override DependencyObject CreateShell()
        {
            return this.Container.GetExportedValue<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Shell)this.Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureAggregateCatalog()
        {
            // add itself to the catalog
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(MainBootstrapper).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(IViewRegionRegistration).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Library.Client.Wpf.WpfExtensions).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Library.Common.Extension.Extensions).Assembly));

            DirectoryCatalog directoryCatalog = new DirectoryCatalog("Modules");
            this.AggregateCatalog.Catalogs.Add(directoryCatalog);

        }

        protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            var factory = base.ConfigureDefaultRegionBehaviors();

            factory.AddIfMissing("AutoPopulateExportedViewsBehavior", typeof(AutoPopulateExportedViewsBehavior));

            return factory;
        }
    }
}
