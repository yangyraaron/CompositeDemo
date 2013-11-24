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

namespace Demo.Content.Test
{
    /// <summary>
    /// Interaction logic for ShareSizedGroupView.xaml
    /// </summary>
    [ModuleViewExport(ContentConstant.MODULE_TEST)]
    public partial class ShareSizedGroupView : UserControl
    {
        public ShareSizedGroupView()
        {
            InitializeComponent();
        }

        [Import]
        ShareSizedGroupViewModel ViewModel
        {
            set { this.DataContext = value; }
        }

        private void setTrue(object sender, System.Windows.RoutedEventArgs e)
        {
            Grid.SetIsSharedSizeScope(dp1, true);
            txt1.Text = "IsSharedSizeScope Property is set to " + Grid.GetIsSharedSizeScope(dp1).ToString();
        }

        private void setFalse(object sender, System.Windows.RoutedEventArgs e)
        {
            Grid.SetIsSharedSizeScope(dp1, false);
            txt1.Text = "IsSharedSizeScope Property is set to " + Grid.GetIsSharedSizeScope(dp1).ToString();
        }
    }
}
