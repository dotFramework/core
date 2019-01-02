using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace DotFramework.Core
{
    [DataContract]
    public class BindableBase : INotifyPropertyChanging, INotifyPropertyChanged
    {
        #region INotifyPropertyChange

        /// <summary>
        ///     Multicast event for property changing notifications.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        ///     Multicast event for property change notifications.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Notifies listeners that a property value is changing.
        /// </summary>
        /// <param name="propertyName">
        ///     Name of the property used to notify listeners.  This
        ///     value is optional and can be provided automatically when invoked from compilers
        ///     that support <see cref="CallerMemberNameAttribute" />.
        /// </param>
        /// <returns>
        ///     Return PropertyChangingCancelEventArgs
        /// </returns>
#if !NET40
        protected PropertyChangingCancelEventArgs<T> OnPropertyChanging<T>(T originalValue, T newValue, [CallerMemberName] string propertyName = null)
#else
        protected PropertyChangingCancelEventArgs<T> OnPropertyChanging<T>(T originalValue, T newValue, string propertyName = null)
#endif
        {
            PropertyChangingEventHandler eventHandler = PropertyChanging;

            if (eventHandler != null)
            {
                PropertyChangingCancelEventArgs<T> args = new PropertyChangingCancelEventArgs<T>(propertyName, originalValue, newValue);
                eventHandler(this, args);
                return args;
            }

            return null;
        }

        /// <summary>
        ///     Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">
        ///     Name of the property used to notify listeners.  This
        ///     value is optional and can be provided automatically when invoked from compilers
        ///     that support <see cref="CallerMemberNameAttribute" />.
        /// </param>
#if !NET40
        protected void OnPropertyChanged<T>(T previousValue, T currentValue, [CallerMemberName] string propertyName = null)
#else
        protected void OnPropertyChanged<T>(T previousValue, T currentValue, string propertyName = null)
#endif
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs<T>(propertyName, previousValue, currentValue));
        }

        /// <summary>
        ///     Checks if a property already matches a desired value.  Sets the property and
        ///     notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="field">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">
        ///     Name of the property used to notify listeners.  This
        ///     value is optional and can be provided automatically when invoked from compilers that
        ///     support CallerMemberName.
        /// </param>
        /// <returns>
        ///     True if the value was changed, false if the existing value matched the
        ///     desired value.
        /// </returns>
#if !NET40
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] String propertyName = null)
#else
        protected bool SetProperty<T>(ref T field, T value, String propertyName = null)
#endif
        {
            if (Equals(field, value))
            {
                return false;
            }

            PropertyChangingCancelEventArgs<T> args = OnPropertyChanging(field, value, propertyName);

            if (args != null)
            {
                if (!args.Cancel)
                {
                    var previousValue = field;
                    field = args.NewValue;
                    OnPropertyChanged(previousValue, args.NewValue, propertyName);
                }
            }
            else
            {
                var previousValue = field;
                field = value;
                OnPropertyChanged(previousValue, value, propertyName);
            }

            return true;
        }

#if !NET40
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
#else
        protected void RaisePropertyChanged(string propertyName = null)
#endif
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

#endregion
    }
}
