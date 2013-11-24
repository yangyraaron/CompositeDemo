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

namespace Demo.Content.Tool.View
{
    /// <summary>
    /// Interaction logic for ReportDataTest.xaml
    /// </summary>
    [ModuleViewExport(ContentConstant.MODULE_TOOL)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ReportDataTestView : UserControl
    {
        public ReportDataTestView()
        {
            InitializeComponent();
        }

        [Import]
        ReportDataTestVM ViewModel
        {
            set { this.DataContext = value; }
        }
    }
}
