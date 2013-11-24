using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Demo.Service.Reports;

namespace Demo.Content
{
    public class JobVariance
    {

        public JobVariance(JobOutputVariance parent)
        {
            _parent = parent;
        }

        private JobOutputVariance _parent;
        public JobOutputVariance Parent
        {
            get { return _parent; }
        }    
                
        
        private String _group;
        public String Group
        {
            get
            {
                return

                    _group = string.Format("job number:{0}  Product:{1}  ProcessStage:{2}",
                 _parent.JobNumber, _parent.Product, _parent.ProcessStage);
                
            }
        }    
                
    }
}
