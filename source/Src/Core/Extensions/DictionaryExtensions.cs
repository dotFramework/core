namespace System.Collections.Generic
{
    public static class DictionaryExtensions
    {
        public static TValue TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key)
        {
            if (self.ContainsKey(key))
            {
                return self[key];
            }
            else
            {
                return default(TValue);
            }
        }
    }
}
