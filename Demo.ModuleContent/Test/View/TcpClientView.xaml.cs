using Demo.Content.Test;
using Demo.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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

namespace Demo.Content.Test
{
    /// <summary>
    /// Interaction logic for TcpClient.xaml
    /// </summary>
    [ModuleViewExport(ContentConstant.MODULE_TEST)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class TcpClientView : UserControl
    {
        public TcpClientView()
        {
            InitializeComponent();
        }

        [Import]
        TcpClientVM ViewModel {
            set {
                this.DataContext = value;
            }
        }
    }
}
