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
using System.ComponentModel.Composition;
using Demo.Infrastructure;

namespace Demo.Content
{
    /// <summary>
    /// Interaction logic for ReportParametersGeneratorView.xaml
    /// </summary>
    [ModuleViewExport(ContentConstant.MODULE_TOOL)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ReportParametersGeneratorView : UserControl
    {
        public ReportParametersGeneratorView()
        {
            InitializeComponent();
        }

        [Import]
        ReportParametersGeneratorViewModel ViewModel
        {
            set {
                this.DataContext = value;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            txtContent.SelectionLength = this.txtContent.Text.Length), null);
        }
    }
}
