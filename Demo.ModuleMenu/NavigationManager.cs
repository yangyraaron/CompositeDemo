using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Demo.Infrastructure;

namespace Demo.Module.Menu
{
    public class NavigationManager
    {
        public static IEnumerable<MenuModel> GetMenus()
        {
            IEnumerable<XElement> query = ConfigurationMgr.Current.GetNavigationMenus();

            if (query.Any())
            {
                return query.Select(x => { return GetElementMenus(x); });
            }

            return null;
        }

        private static MenuModel GetElementMenus(XElement element)
        {
            var model = new MenuModel();

            model.Id = element.Attribute("Id").Value;
            model.Name = element.Attribute("Name").Value;

            var query = from attr in element.Attributes()
                        select new KeyValuePair<object, object>(attr.Name, attr.Value);

            foreach (var attr in query)
            {
                model.Properties.Add(attr.Key.ToString(), attr.Value);
            }

            var cqury = from ce in element.Descendants("Menu")
                        select ce;

            if (cqury.Any())
            {
                foreach (var child in cqury)
                {
                    model.ChildMenuData.Add(GetElementMenus(child));
                }
            }

            return model;
        }

    }
}
