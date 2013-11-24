using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Demo.Infrastructure;

namespace Demo.Module.Menu
{
    [Export(typeof(LeftMenuViewModel))]
    public class LeftMenuViewModel
    {
        private readonly ObservableCollection<MenuModel> _menudata;
        private readonly DelegateCommand<MenuModel> _loadContentCommand;
        private readonly IEventAggregator _eventAggregator;

        [ImportingConstructor]
        public LeftMenuViewModel(IEventAggregator eventAggregator)
        {
            _menudata = new ObservableCollection<MenuModel>();
            _loadContentCommand = new DelegateCommand<MenuModel>(LoadContent);
            _eventAggregator = eventAggregator;

            IEnumerable<MenuModel> menuModels = NavigationManager.GetMenus();
            _menudata = new ObservableCollection<MenuModel>(menuModels);
        }

        #region MenuData

        public ObservableCollection<MenuModel> MenuData {
            get
            {
                return _menudata;
            }
        }

        #endregion

        #region Load Content Command

        public DelegateCommand<MenuModel> LoadContentCommand
        {
            get {
                return _loadContentCommand;
            }
        }

        private void LoadContent(MenuModel model)
        {
            if (model == null)
                return;

            if (model.ChildMenuData.Any())
                return;

            _eventAggregator.GetEvent<LoadContentEvent>().Publish(model.Properties);
        }

        #endregion
    }
}
