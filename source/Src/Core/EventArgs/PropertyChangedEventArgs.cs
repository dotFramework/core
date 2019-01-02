using System.ComponentModel;

namespace DotFramework.Core
{
    public class PropertyChangedEventArgs<T> : PropertyChangedEventArgs
    {
        public T PreviousValue { get; private set; }
        public T CurrentValue { get; private set; }

        public PropertyChangedEventArgs(string propertyName, T previousValue, T currentValue)
            : base(propertyName)
        {
            PreviousValue = previousValue;
            CurrentValue = currentValue;
        }
    }
}
