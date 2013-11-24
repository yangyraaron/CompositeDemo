using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Controls;
using System.Windows;

namespace Demo.Infrastructure
{
    public class SelectedCommandBehavior:CommandBehaviorBase<TreeView>
    {
        public SelectedCommandBehavior(TreeView treeView):base(treeView)
        {
            treeView.SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(OnSelectedItemChanged);
        }

        private void OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue != null && this.TargetObject != null)
            {
                this.CommandParameter = e.NewValue;
                this.ExecuteCommand();
            }
        }
    }
}
