using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Demo.Infrastructure;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;

namespace Demo.Content
{
    [ViewExport(RegionName=RegionNames.ContentRegion)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ContentView : UserControl
    {
        public ContentView()
        {
            InitializeComponent();
        }

        [Import]
        ContentViewModel ViewModel
        {
            set {
                this.DataContext = value;
            }
        }


    }
}
