using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using Demo.Infrastructure;
using System.IO;

namespace Demo.Content
{
    public class LocManager
    {
        private string _fileFolder;
        private string _fileName;
        private string _filePath;

        public LocManager(string fileFolder,string fileName)
        {
            this._fileFolder = fileFolder;
            this._fileName = fileName;
            this._filePath = string.Format("{0}/{1}.xml", _fileFolder, _fileName);
        }

        public string FileName
        {
            get { return _fileName; }
        }

        private XDocument XDoc
        {
            get
            {
                var doc = DataManager.Current.GetItem<string, XDocument>(_filePath);

                if (doc == null)
                    LoadFile();

                return DataManager.Current.GetItem<string, XDocument>(_filePath);
            }
        }

        private void LoadFile()
        {
            XDocument _xDoc;

            if (File.Exists(_filePath))
            {
                _xDoc = XDocument.Load(_filePath);
                DataManager.Current.AddItem<string, XDocument>(_filePath, _xDoc);
                return;
            }
        }

        public IEnumerable<Entry> GetAllEntries()
        {
            var query = from entry in XDoc.Descendants("Entry")
                        where entry.Attribute("groupname").Value == "BaseGroup"
                        select new Entry
                        {
                            Group = entry.Attribute("group").Value,
                            GroupName = entry.Attribute("groupname").Value,
                            LanguageName = entry.Attribute("language").Value,
                            Item = entry.Attribute("item").Value,
                            Type = entry.Attribute("type").Value,
                            SourceValue = entry.Value
                        };

            return query;
        }
    }
}
