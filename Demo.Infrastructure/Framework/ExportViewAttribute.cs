using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace Demo.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple=true,Inherited=true)]
    [MetadataAttribute]
    public class ModuleViewExportAttribute:ExportAttribute
    {
        public ModuleViewExportAttribute():base(typeof(object))
        {

        }

        public ModuleViewExportAttribute(string moduleName):base(moduleName,typeof(object))
        {

        }
    }
}
