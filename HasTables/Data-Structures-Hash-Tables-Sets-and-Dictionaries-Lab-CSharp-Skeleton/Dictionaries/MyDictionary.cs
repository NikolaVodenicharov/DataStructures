namespace Hash_Table
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class MyDictionary<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
    {
        private HashTable<TKey, TValue> table;

        public MyDictionary()
        {
            this.table = new HashTable<TKey, TValue>();
        }

        public void Add (TKey key, TValue value)
        {
            this.table.Add(key, value);
        }

        public bool ContainsKey(TKey key)
        {
            return this.table.ContainsKey(key);
        }

        public TValue this[TKey key]
        {
            get
            {
                return this.table.Get(key);
            }
            set
            {
                this.table.AddOrReplace(key, value);
            }
        }

        public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
        {
            return this.table.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
