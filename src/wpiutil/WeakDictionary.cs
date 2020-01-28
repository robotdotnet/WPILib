using System;
using System.Collections.Generic;
using System.Text;

namespace WPIUtil
{
    public class WeakDictionary<TKey, TValue>
        where TKey : class
    {
        private struct WeakPair
        {
            private WeakReference<TKey> keyReference;

            public TValue Value { get; set; }

            public TKey? Key
            {
                get
                {
                    if (keyReference.TryGetTarget(out var target))
                    {
                        return target;
                    }
                    return null;
                }
            }
        }
    }
}
