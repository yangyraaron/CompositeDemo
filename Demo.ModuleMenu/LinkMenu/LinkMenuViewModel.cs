using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using System.ComponentModel.Composition;
using System.Collections.ObjectModel;

namespace Demo.Module.Menu
{
    [Export(typeof(LinkMenuViewModel))]
    public class LinkMenuViewModel:NotificationObject
    {
        public LinkMenuViewModel()
        {
            LinkData = new ObservableCollection<MenuModel>();
        }

        public ObservableCollection<MenuModel> LinkData { get; private set; }
    }
}
