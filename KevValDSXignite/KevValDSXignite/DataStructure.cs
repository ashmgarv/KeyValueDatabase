using System;
namespace KevValDSXignite
{
    public interface DataStructure<K,V>
    {
        V Get(K key);
        void Put(K key, V value);
        K GetRandomKey();
        bool Delete(K key);
    }
}
