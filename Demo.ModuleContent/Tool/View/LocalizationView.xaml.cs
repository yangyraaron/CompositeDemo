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
    /// Interaction logic for LocalizationView.xaml
    /// </summary>
    [ModuleViewExport(ContentConstant.MODULE_TOOL)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class LocalizationView : UserControl
    {
        public LocalizationView()
        {
            InitializeComponent();
        }

        [Import]
        LocalizationViewModel ViewModel
        {
            set {
                this.DataContext = value;
            }
        }
    }
}
