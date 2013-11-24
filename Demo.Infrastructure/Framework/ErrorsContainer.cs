using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Library.Common.Extension;

namespace Demo.Infrastructure
{
    public class ErrorsContainer<T>
    {
        private readonly Action<string> raiseErrorsChanged;
        private readonly Dictionary<string, T> validationResult;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorsContainer{T}"/> class.
        /// </summary>
        /// <param name="raiseErrorsChanged">The action that raises the <see cref="INotifyDataErrorInfo.ErrorsChanged"/>
        /// event.</param>
        public ErrorsContainer(Action<string> raiseErrorsChanged)
        {
            if (raiseErrorsChanged == null)
            {
                throw new ArgumentNullException("raiseErrorsChanged");
            }

            this.raiseErrorsChanged = raiseErrorsChanged;
            this.validationResult = new Dictionary<string, T>();
        }

        /// <summary>
        /// Gets a value that indicates whether the object has validation errors. 
        /// </summary>
        public bool HasErrors
        {
            get
            {
                return this.validationResult.Count != 0;
            }
        }

        /// <summary>
        /// Gets the validation errors for a specified property.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The validation errors of type <typeparamref name="T"/> for the property.</returns>
        public T GetErrors(string propertyName)
        {
            var localPropertyName = propertyName ?? string.Empty;
            T currentValidationResult = default(T);
            if (this.validationResult.TryGetValue(localPropertyName, out currentValidationResult))
            {
                return currentValidationResult;
            }

            return currentValidationResult;
        }

        /// <summary>
        /// Sets the validation errors for the specified property.
        /// </summary>
        /// <remarks>
        /// If a change is detected then the errors changed event is raised.
        /// </remarks>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="newValidationResult">The new validation errors.</param>
        public void SetErrors(string propertyName, T newValidationResult)
        {
            if (newValidationResult == null)
                return;

            var localPropertyName = propertyName ?? string.Empty;

            this.validationResult[localPropertyName] = newValidationResult;
            this.raiseErrorsChanged(localPropertyName);
        }

        /// <summary>
        /// remove the validation errors for the specified property
        /// </summary>
        /// <param name="propertyName"></param>
        public void RemoveErrors(string propertyName)
        {
            var localPropertyName = propertyName ?? string.Empty;

            if (!this.validationResult.ContainsKey(localPropertyName))
                return;

            this.validationResult.Remove(localPropertyName);
            this.raiseErrorsChanged(localPropertyName);
        }
    }
}
