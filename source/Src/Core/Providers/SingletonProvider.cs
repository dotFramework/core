using System;

namespace DotFramework.Core
{
    public class SingletonProvider<T> : IDisposable where T : class
    {
        private static T _Instance;
        private static readonly object padlock = new object();

        public static T Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (padlock)
                    {
                        if (_Instance == null)
                        {
                            _Instance = CreateInstance();
                        }
                    }
                }

                return _Instance;
            }
        }

        private static T CreateInstance()
        {
            return Activator.CreateInstance(typeof(T), true) as T;
        }

        public virtual void Dispose()
        {
            
        }
    }
}
