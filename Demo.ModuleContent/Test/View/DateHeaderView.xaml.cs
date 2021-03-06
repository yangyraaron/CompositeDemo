﻿using System;
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
    /// Interaction logic for DateHeaderView.xaml
    /// </summary>
    [ModuleViewExport(ContentConstant.MODULE_TEST)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class DateHeaderView : UserControl
    {
        public DateHeaderView()
        {
            InitializeComponent();
        }

        [Import]
        DateHeaderViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
            }
        }
    }
}
