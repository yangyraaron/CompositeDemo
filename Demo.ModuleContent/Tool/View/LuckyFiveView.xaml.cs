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

namespace Demo.Content.Tool
{
    /// <summary>
    /// Interaction logic for LuckyFiveView.xaml
    /// </summary>
    [ModuleViewExport(ContentConstant.MODULE_TOOL)]
    public partial class LuckyFiveView : UserControl
    {
        public LuckyFiveView()
        {
            InitializeComponent();
        }

        [Import]
        LuckyFiveViewModel ViewModel
        {
            set { this.DataContext = value; }
        }
    }
}
