using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using System.Collections.ObjectModel;

namespace Demo.Module.Menu
{
    public class MenuModel:NotificationObject
    {
        private readonly IDictionary<object, object> _properties;
        public MenuModel()
        {
            _properties = new Dictionary<object, object>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public IDictionary<object, object> Properties { get { return _properties; } }

        private ObservableCollection<MenuModel> _childMenuData;
        public ObservableCollection<MenuModel> ChildMenuData {
            get {
                if (_childMenuData == null)
                    _childMenuData = new ObservableCollection<MenuModel>();
                return _childMenuData;
            }
        }
    }
}
