using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;

namespace Demo.Infrastructure
{
    public class LoadContentEvent : CompositePresentationEvent<IDictionary<object, object>>
    {
    }

    public delegate void LoadContentHandler(object sender,IDictionary<object,object> parameters);
}
