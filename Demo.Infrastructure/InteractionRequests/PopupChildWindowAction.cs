using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Windows.Controls;

namespace Demo.Infrastructure
{
    public class PopupChildWindowAction : TriggerAction<FrameworkElement>
    {
        /// <summary>
        /// Displays the child window and collects results for <see cref="IInteractionRequest"/>.
        /// </summary>
        /// <param name="parameter">The parameter to the action. If the action does not require a parameter, the parameter may be set to a null reference.</param>
        protected override void Invoke(object parameter)
        {
            var args = parameter as InteractionRequestedEventArgs;
            if (args == null)
            {
                return;
            }

            var childWindow = this.GetChildWindow(args.Context);

            var callback = args.Callback;
            EventHandler handler = null;
            handler =
                (o, e) =>
                {
                    childWindow.Closed -= handler;
                    callback();
                };
            childWindow.Closed += handler;

            childWindow.Show();
        }


        /// <summary>
        /// Returns the child window to display as part of the trigger action.
        /// </summary>
        /// <param name="notification">The notification to display in the child window.</param>
        /// <returns></returns>
        protected ChildWindow GetChildWindow(Notification notification)
        {
            var childWindow = this.ChildWindow ?? new ChildWindow();
            childWindow.DataContext = notification;

            return childWindow;
        }

        /// <summary>
        /// The child window to display as part of the popup.
        /// </summary>
        public static readonly DependencyProperty ChildWindowProperty =
            DependencyProperty.Register(
                "ChildWindow",
                typeof(ChildWindow),
                typeof(PopupChildWindowAction),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the child window to pop up.
        /// </summary>
        /// <remarks>
        /// If not specified, a default child window is used instead.
        /// </remarks>
        public ChildWindow ChildWindow
        {
            get { return (ChildWindow)GetValue(ChildWindowProperty); }
            set { SetValue(ChildWindowProperty, value); }
        }
    }
}
