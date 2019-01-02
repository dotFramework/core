using System;

namespace DotFramework.Core
{
    public class PropertyChangingEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="propertyName"></param>
        public PropertyChangingEventArgs(string propertyName)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; private set; }

    }
}