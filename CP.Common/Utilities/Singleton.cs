using System;
using System.Collections.Generic;
using System.Text;

namespace CP.Common.Utilities
{
    public abstract class SingletonBase<T> where T : SingletonBase<T>, new()
    {
        private static bool IsInstanceCreated = false;
        private static bool IsInstanceLoaded = false;
        private static object InstanceLoadedLock = new object();
        private static readonly Lazy<T> LazyInstance = new Lazy<T>(() =>
        {
            T instance = new T();
            IsInstanceCreated = true;
            return instance;
        });

        protected SingletonBase()
        {
            if (IsInstanceCreated)
            {
                throw new InvalidOperationException("Constructing a " + typeof(T).Name + " manually is not allowed, use the Instance property.");
            }
        }

        protected virtual void Load()
        {

        }

        public static T Instance
        {
            get
            {
                if(!IsInstanceLoaded)
                {
                    lock (InstanceLoadedLock)
                    {
                        if (!IsInstanceLoaded)
                        {
                            IsInstanceLoaded = true;
                            LazyInstance.Value.Load();
                        }
                    }
                }

                return LazyInstance.Value;
            }
        }
    }
}
