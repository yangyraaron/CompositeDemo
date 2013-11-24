using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Demo.Infrastructure;
using System.Xml.Serialization;
using System.IO;
using System.Xaml;

namespace Demo.Content
{
    public class ReportFieldDataModel
    {

        private string GetFilePath(string reportName)
        {
            var folder = ConfigurationMgr.Current.GetParamValue("ReportXmlFolder");

            if (string.IsNullOrEmpty(folder))
                return null;

            StringBuilder sb = new StringBuilder(folder);
            sb.Append("/").Append(reportName).Append(".xaml");

            return sb.ToString();
        }

        public void SaveFieldsToXaml<T>(string reportName,T instance)
        {
            string path = GetFilePath(reportName);
            if (string.IsNullOrEmpty(path))
                return;

            using (var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                XamlServices.Save(stream, instance);
            }
        }

        public T GetFieldsFromXml<T>(string reportName)
        {
            string path = GetFilePath(reportName);
            if (string.IsNullOrEmpty(path))
                return default(T);

            using (var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
            {
                try
                {
                    return (T)XamlServices.Load(stream);
                }
                catch (Exception ex)
                {
                    return default(T);
                }
            }
        }
    }
}
