using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using Demo.Infrastructure;
using Library.Common.Extension;
using System.Windows;

namespace Demo.Content
{
    [Export(typeof(ContentViewModel))]
    public class ContentViewModel : NotificationObject
    {
        private readonly IEventAggregator _eventAggregator;
        [ImportMany]
        Lazy<IController>[] _controllers;

        [ImportingConstructor]
        public ContentViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Initialize();
        }

        private void Initialize()
        {
            _eventAggregator.GetEvent<LoadContentEvent>().Subscribe(LoadContent);

            LoadResouces();
        }

        private void LoadResouces()
        {

            string path = @"pack://application:,,,/Demo.Content;component/Resources/Styles.xaml";

            var r = new ResourceDictionary();
            r.Source = new Uri(path, UriKind.RelativeOrAbsolute);

            if (Application.Current.Resources.MergedDictionaries.Contains(r))
                return;

            Application.Current.Resources.MergedDictionaries.Add(r);

        }

        private void LoadContent(IDictionary<object, object> dic)
        {
            string module = dic[Constant.CFG_NODE_ATTR_MODULE].ToString();
            string id = dic[Constant.CFG_NODE_ATTR_ID].ToString();

            if (!_controllers.Any())
                return;

            var controller = _controllers.FirstOrDefault(
                xc =>
                {
                    if (xc.Value.IsNull())
                        return false;
                    var controllerKey = string.Format("{0}Controller", module);
                    return xc.Value.GetType().Name == controllerKey;
                });

            if (controller == null)
                return;

            controller.Value.ActiveView(id);

        }

    }
}
