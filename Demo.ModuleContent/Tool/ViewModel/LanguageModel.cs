using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.Content
{
    public class LanguageModel
    {
        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }
        public override string ToString()
        {
            return string.Format("{0}-{1}", Description, Name);
        }
    }
}
