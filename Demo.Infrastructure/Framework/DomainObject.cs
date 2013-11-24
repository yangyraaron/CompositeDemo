using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using System.ComponentModel;

namespace Demo.Infrastructure
{
    public class DomainObject:NotificationObject,IDataErrorInfo
    {
        private ErrorsContainer<string> errorsContainer;

        /// <summary>
        /// Event raised when the validation status changes.
        /// </summary>
        /// <seealso cref="INotifyDataErrorInfo"/>
        public event EventHandler<ValidateionErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// Gets the error status.
        /// </summary>
        /// <seealso cref="INotifyDataErrorInfo"/>
        public bool HasErrors
        {
            get { return this.ErrorsContainer.HasErrors; }
        }

        /// <summary>
        /// Gets the container for errors in the properties of the domain object.
        /// </summary>
        protected ErrorsContainer<string> ErrorsContainer
        {
            get
            {
                if (this.errorsContainer == null)
                {
                    this.errorsContainer =
                        new ErrorsContainer<string>(pn => this.RaiseErrorsChanged(pn));
                }

                return this.errorsContainer;
            }
        }

        /// <summary>
        /// Raises the <see cref="ErrorsChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property which changed its error status.</param>
        protected void RaiseErrorsChanged(string propertyName)
        {
            this.OnErrorsChanged(new ValidateionErrorsChangedEventArgs(propertyName:propertyName));
        }

        /// <summary>
        /// Raises the <see cref="ErrorsChanged"/> event.
        /// </summary>
        /// <param name="e">The argument for the event.</param>
        protected virtual void OnErrorsChanged(ValidateionErrorsChangedEventArgs e)
        {
            var handler = this.ErrorsChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// validate property's value
        /// </summary>
        /// <param name="propertyName">property value</param>
        /// <returns></returns>
        protected virtual String ValidateProperty(string propertyName)
        {
            return null;
        }

        private string RaisePropertyValidating(string columnName)
        {
            string result = ValidateProperty(columnName);

            if (result == null)
                this.ErrorsContainer.RemoveErrors(columnName);
            else
                this.ErrorsContainer.SetErrors(columnName, result);

            return result;
        }

        #region IDataErrorInfo Members

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get { return RaisePropertyValidating(columnName); }
        }

        #endregion
    }

    public class ValidateionErrorsChangedEventArgs:EventArgs
    {
        private string _propertyName ;
        public ValidateionErrorsChangedEventArgs(string propertyName = null)
        {
            _propertyName = propertyName;
        }

        public string PropertyName
        {
            get { return _propertyName; }
        }
    }
}
