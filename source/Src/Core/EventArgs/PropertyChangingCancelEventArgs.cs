namespace DotFramework.Core
{
    public class PropertyChangingCancelEventArgs : PropertyChangingEventArgs
    {
        public bool Cancel { get; set; }

        public PropertyChangingCancelEventArgs(string propertyName)
            : base(propertyName)
        {
        }
    }

    public class PropertyChangingCancelEventArgs<T> : PropertyChangingCancelEventArgs
    {
        public T OriginalValue { get; private set; }
        public T NewValue { get; set; }

        public PropertyChangingCancelEventArgs(string propertyName, T originalValue, T newValue)
            : base(propertyName)
        {
            OriginalValue = originalValue;
            NewValue = newValue;
        }
    }
}