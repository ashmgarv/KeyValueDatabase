namespace KevValDSXignite
{
    class MapNode<K, V>
    {
        public K Key;
        public V Value;
        public MapNode<K, V> next;
        public MapNode(K key, V value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}
