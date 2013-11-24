using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace Demo.Infrastructure
{
    public class ConfigurationMgr
    {
        private static ConfigurationMgr _instance;
        private const string _configurationKey = "Config.xml";

        static ConfigurationMgr()
        {
            _instance = new ConfigurationMgr();
        }

        public static ConfigurationMgr Current
        {
            get
            {
                return _instance;
            }
        }

        private XDocument XDoc
        {
            get
            {
                var doc = DataManager.Current.GetItem<string, XDocument>(_configurationKey);

                if (doc == null)
                    LoadFile();

                return DataManager.Current.GetItem<string, XDocument>(_configurationKey);
            }
        }

        private void LoadFile()
        {
            XDocument _xDoc;

            if (File.Exists(_configurationKey))
            {
                _xDoc = XDocument.Load(_configurationKey);
                DataManager.Current.AddItem<string, XDocument>(_configurationKey, _xDoc);
                return;
            }
        }

        public IEnumerable<XElement> GetNavigationMenus()
        {
            return from menu in XDoc.Descendants("Navigation").Descendants("Menus").Elements("Menu")
                   select menu;
        }

        public string GetParamValue(string paramName)
        {
            if (XDoc == null)
                return string.Empty;

            var query = from element in XDoc.Descendants("Parameter")
                        where element.Attribute("Name").Value == paramName.Trim()
                        select element.Attribute("Value").Value;

            return query.FirstOrDefault();

        }
    }
}
