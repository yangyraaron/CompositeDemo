using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using System.ComponentModel;
using System.Linq.Expressions;
using Microsoft.Practices.Prism.ViewModel;

namespace Demo.Infrastructure
{
    public class ConfirmationNotificationObject : Confirmation, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RaisePropertyChanged<T>(Expression<Func<T>> lambda)
        {
            var name = PropertySupport.ExtractPropertyName<T>(lambda);
            OnPropertyChanged(name);
        }
    }
}
