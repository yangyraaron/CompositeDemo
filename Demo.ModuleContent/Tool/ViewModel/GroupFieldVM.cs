using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.Content
{
    public class GroupFieldVM : ReportFieldVM
    {
        
        private GroupFieldVM _parentGroup;
        public GroupFieldVM ParentGroup
        {
            get { return _parentGroup; }
            set
            {
                if (_parentGroup == value)
                    return;
                _parentGroup = value;
                RaisePropertyChanged(() => ParentGroup);
            }
        }    
                
        private int _groupSize;
        public int GroupSize
        {
            get { return _groupSize; }
            set
            {
                if (_groupSize == value)
                    return;
                _groupSize = value;
                RaisePropertyChanged(() => GroupSize);
            }
        }

    }
}
