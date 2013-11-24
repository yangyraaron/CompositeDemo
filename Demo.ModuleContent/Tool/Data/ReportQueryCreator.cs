using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Xml.Linq;
using Library.Common.Extension;

namespace Demo.Content.Tool.Data
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
   public class ReportQueryCreator
    {
       public const string strItems = @"Items{0}/Item{1}";

       [Import]
       public ReportFieldTypeAdapterFactory AdapterFactory { get; set; }

       private string CreateFieldsString(IEnumerable<ReportFieldVM> reportFields)
       {
           StringBuilder sb = new StringBuilder();

           sb.Append("{");
           foreach (ReportFieldVM field in reportFields)
           {
               sb.AppendFormat("{0} ({1}),", field.ReportFieldName, field.ReportFieldType);
           }
           sb.Remove(sb.Length - 1, 1);
           sb.Append("}");

           return sb.ToString();
       }

     
       public string CreateQuery(IEnumerable<ReportFieldVM> groupFields,IEnumerable<ReportFieldVM> reportFeilds,int size = 10)
       {
           //var items =  CreateFieldElementsData(reportFeilds, size).ToList();
           //string path = string.Format(strItems,"{}",CreateFieldsString(reportFeilds));

           //XDocument doc = new XDocument(
           //    new XElement("Query",
           //        new XElement("XmlData",
           //            new XElement("Items", items)),
           //        new XElement("ElementPath",path)
           //        )
           //    );

           //return doc.ToString();
           return string.Empty;
       }

       private IEnumerable<XElement> CreateGroupElementsData(IEnumerable<ReportFieldVM> groupFields,IEnumerable<ReportFieldVM> fields)
       {
           if (groupFields == null)
               return null;
           var sortedGroups = groupFields.OrderByDescending(x => x.GroupLevel);

           int totalSize = 0;
           foreach (var group in sortedGroups)
           {
               int size = group.GroupSize == 0 ? 1 : group.GroupSize;

               totalSize *= size;
           }

           //var items = CreateFieldElementsData(fields, totalSize);

           return null;
       }

       

       private IEnumerable<XElement> CreateFieldElementsData(IEnumerable<ReportFieldVM> reportFields,XElement parentElement,ReportFieldVM parentGroup)
       {
           if (reportFields == null)
               return null;

           var group = reportFields
               .GroupBy(x => x.Group)
               .OrderBy(x=>x.Key.GroupLevel)
               .FirstOrDefault(x=>x.Key.Group.ReportFieldName == parentGroup.Group.ReportFieldName);

            int p=0;
            while (p++ < group.Key.GroupSize)
            {
                var elements = CreateFieldsElements(group);
            }

            return null;
       }
        

       private IEnumerable<XElement> CreateFieldsElements(IEnumerable<ReportFieldVM> reportFields)
        {
                foreach (var field in reportFields)
                    yield return CreateFieldElement(field);
            
        }

        private XElement CreateFieldElement(ReportFieldVM field)
       {
            var adapter = AdapterFactory.GetReportFieldDataAdapter(field.OriginalFieldType);

            return new XElement(field.ReportFieldName, adapter.GetData());
       }

    }
}
