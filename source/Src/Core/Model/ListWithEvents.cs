using System;
using System.Collections.Generic;

namespace DotFramework.Core
{
    public class ListEventArgs<T> : EventArgs
    {
        public ListEventArgs(T NewObject)
        {
            _NewObject = NewObject;
        }

        private T _NewObject;
        public T NewObject
        {
            get { return _NewObject; }
            set { _NewObject = value; }
        }
    }

    public class ListWithEvents<T> : List<T>
    {
        public ListWithEvents(IEnumerable<T> collection)
            : base(collection)
        {

        }

        public event EventHandler<ListEventArgs<T>> ItemAdding;
        public event EventHandler<ListEventArgs<T>> ItemAdded;

        public new void Add(T item)
        {
            ItemAdding?.Invoke(item, new ListEventArgs<T>(item));

            base.Add(item);

            ItemAdded?.Invoke(item, new ListEventArgs<T>(item));

        }
    }
}
