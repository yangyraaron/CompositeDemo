using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace Demo.Infrastructure
{
    public static class RegisteredBehaviors
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached(
            "Command",
            typeof(ICommand),
            typeof(RegisteredBehaviors),
            new PropertyMetadata(OnSetCommandCallback));

        private static readonly DependencyProperty SelectedCommandBehaviorProperty = DependencyProperty.RegisterAttached(
            "SelectedCommandBehavior", 
            typeof(SelectedCommandBehavior), 
            typeof(RegisteredBehaviors), 
            null);

        private static void OnSetCommandCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            TreeView tree = dependencyObject as TreeView;
            if (tree == null)
                return;

            SelectedCommandBehavior behavior = GetOrCreateBehavior(tree);
            behavior.Command = e.NewValue as ICommand;
        }

        public static ICommand GetCommand(TreeView tree)
        {
            return tree.GetValue(CommandProperty) as ICommand;
        }

        public static void SetCommand(TreeView tree,ICommand command)
        {
            tree.SetValue(CommandProperty, command);
        }

        private static SelectedCommandBehavior GetOrCreateBehavior(TreeView tree)
        {
            SelectedCommandBehavior behavior = tree.GetValue(SelectedCommandBehaviorProperty) as SelectedCommandBehavior;

            if (behavior == null)
            {
                behavior = new SelectedCommandBehavior(tree);
                tree.SetValue(SelectedCommandBehaviorProperty, behavior);
            }

            return behavior;
        }



        public static readonly DependencyProperty EventBasedCommandProperty = DependencyProperty.RegisterAttached(
            "EventBasedCommand",
            typeof(ICommand),
            typeof(RegisteredBehaviors),
            new FrameworkPropertyMetadata(OnSetEventBasedCommandCallback));

        private static void OnSetEventBasedCommandCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var behavior = GetOrCreateEventBasedBehavior(dependencyObject);

            behavior.Command = e.NewValue as ICommand;
        }

        private static EventBasedCommandBehavior GetOrCreateEventBasedBehavior(DependencyObject obj)
        {
            Control ctl = obj as Control;
            if (ctl == null)
                throw new ArgumentException("Please attach the property to the control instance.");

            var behavior = obj.GetValue(EventBasedCommandBehaviorProperty) as EventBasedCommandBehavior;

            if (behavior == null)
            {
                string evtName = (string) ctl.GetValue(BasedEventNameProperty);
                behavior = new EventBasedCommandBehavior(ctl,evtName);

                ctl.SetValue(EventBasedCommandBehaviorProperty, behavior);
            }

            return behavior;
        }

        public static ICommand GetEventBasedCommand(DependencyObject d)
        {
            return d.GetValue(EventBasedCommandProperty) as ICommand;
        }

        public static void SetEventBasedCommand(DependencyObject d, ICommand value)
        {
            d.SetValue(EventBasedCommandProperty, value);
        }

        private static readonly DependencyProperty EventBasedCommandBehaviorProperty = DependencyProperty.RegisterAttached(
            "EventBasedCommandBehavior",
            typeof(EventBasedCommandBehavior),
            typeof(RegisteredBehaviors), null);

        public static readonly DependencyProperty BasedEventNameProperty = DependencyProperty.RegisterAttached(
            "BasedEventName",
            typeof(string),
            typeof(RegisteredBehaviors),
            new FrameworkPropertyMetadata(string.Empty,
                OnSetEventBasedCommandCallback));

        public static String GetBasedEventName(DependencyObject obj)
        {
            return (string)obj.GetValue(BasedEventNameProperty);
        }

        public static void SetBasedEventName(DependencyObject obj, string value)
        {
            obj.SetValue(BasedEventNameProperty, value);
        }

        private static readonly DependencyProperty EventBasedCommandParameterProperty = DependencyProperty.RegisterAttached(
            "EventBasedCommandParameter",
            typeof(object),
            typeof(RegisteredBehaviors),
            new FrameworkPropertyMetadata(null,
                OnEventBasedCommandParameterChanged));

        private static void OnEventBasedCommandParameterChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if(e.OldValue!=e.NewValue)
            {
                var behavior = dependencyObject.GetValue(EventBasedCommandBehaviorProperty) as EventBasedCommandBehavior;
                if(behavior == null)
                    return;
                behavior.CommandParameter = e.NewValue;
            }
        }

        public static object GetEventBasedCommandParameter(DependencyObject obj)
        {
            return obj.GetValue(EventBasedCommandParameterProperty);
        }

        public static void SetEventBasedCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(EventBasedCommandParameterProperty, value);
        }
        
    }
}
