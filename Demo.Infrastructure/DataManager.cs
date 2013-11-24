using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Demo.Infrastructure
{
    public class DataManager
    {
        private static Hashtable _cache;

        static DataManager()
        {
            _cache = Hashtable.Synchronized(new Hashtable());
        }

        private static DataManager _instance;

        public static DataManager Current
        {
            get
            {
                if (_instance == null)
                    _instance = new DataManager();
                return _instance;
            }
        }

        public void AddWeakReferenceItem<TKey, TValue>(TKey key, TValue item)
        {
            var weakRef = new WeakReference(item);

            _cache[key] = weakRef;
        }

        public void AddItem<Tkey, TValue>(Tkey key, TValue value)
        {
            _cache[key] = value;
        }

        public TValue GetItem<Tkey, TValue>(Tkey key)
            where TValue : class
        {
            if (!_cache.Contains(key))
                return null;

            return _cache[key] as TValue;

        }

        public TValue GetWeakReferenceItem<TKey, TValue>(TKey key) where TValue : class
        {
            if (!_cache.Contains(key))
                return null;

            var weakRef = _cache[key] as WeakReference;
            if (weakRef == null)
                return null;

            var obj = weakRef.Target;
            if (obj == null)
                return null;

            return obj as TValue;
        }

        public TValue GetWeakReferenceItemFunc<TKey, TValue>(TKey key, Func<TValue> func)
            where TValue : class
        {
            if (!_cache.Contains(key))
            {
                TValue item = func();
                AddWeakReferenceItem(key, item);

                return item;
            }

            var weakRef = _cache[key] as WeakReference;
            if (weakRef == null)
            {
                TValue item = func();
                AddWeakReferenceItem(key, item);

                return item;
            }

            var obj = weakRef.Target;
            if (obj == null)
            {
                TValue item = func();
                AddWeakReferenceItem(key, item);

                return item;
            }

            return obj as TValue;
        }
    }
}
