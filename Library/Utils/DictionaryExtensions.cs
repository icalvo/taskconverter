using System.Collections.Generic;

namespace Library
{
    public static class DictionaryExtensions
    {
        public static TV TryGet<TK, TV>(this IDictionary<TK, TV> dic, TK key)
        {
            dic.TryGetValue(key, out TV value);
            return value;
        }
    }
}