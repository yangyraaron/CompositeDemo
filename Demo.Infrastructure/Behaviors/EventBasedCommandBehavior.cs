using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Commands;
using System.Reflection;
using System.Linq.Expressions;

namespace Demo.Infrastructure
{
    public class EventBasedCommandBehavior: CommandBehaviorBase<Control>
    {
        private string _eventName;  

        public EventBasedCommandBehavior(Control target,string eventName):base(target)
        {
            if (string.IsNullOrEmpty(eventName))
                throw new ArgumentException("the event name is empty!");

            _eventName = eventName;
            AttachEvent();
        }

       private void AttachEvent()
        {
            Type type =  this.TargetObject.GetType();
            EventInfo evt = type.GetEvent(_eventName);


            if (evt == null)
                throw new ArgumentException("the event can not be found based on the event name");

           evt.AddEventHandler(this.TargetObject, EventProxy.CreateDelegate(evt,
                () =>
                {
                    this.ExecuteCommand();
                }));
          }
    }

    public static class EventProxy
    {
        public static Delegate CreateDelegate(EventInfo evt,Action action)
        {
            var evtParams = evt.EventHandlerType.GetMethod("Invoke").GetParameters();
            var parameters = evtParams.Select(p => Expression.Parameter(p.ParameterType, "x"));
            //x=>x.action()
            var body = Expression.Call(Expression.Constant(action), action.GetType().GetMethod("Invoke"));
            var lamda = Expression.Lambda(body, parameters.ToArray());
            var d = lamda.Compile();

            return Delegate.CreateDelegate(evt.EventHandlerType, d, "Invoke", false);

        }
    }
}
