using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo.Infrastructure
{
    public interface IController
    {
        void ActiveView(string viewName);
    }
}
