using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Demo.Content
{
    public class ReportFieldTypeAdaptersRegister
    {
        private static readonly Hashtable _registedFieldsType = Hashtable.Synchronized(new Hashtable());

        public void Register(Type type, IReportFieldTypeAdapter adapter = null)
        {
            if (adapter == null)
                return;

            _registedFieldsType[type] = adapter;
        }

        public void UnRegister(Type type)
        {
            if (type == null)
                return;

            if (Contains(type))
                _registedFieldsType.Remove(type);
        }

        public bool Contains(Type type)
        {
            return _registedFieldsType.Contains(type);
        }
    }
}
