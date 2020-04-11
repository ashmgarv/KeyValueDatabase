using System;
using System.Linq;
using System.Collections.Generic;

namespace KevValDSXignite
{

    public class HashMap<K, V> : DataStructure<K, V>
    {
        //Creating a list of nullable key and value pairs
        List<MapNode<K, V>> bucket = new List<MapNode<K, V>>();
        List<K> setKeys = new List<K>();
        private int capacity = 10;
        private int currentSize;

        public HashMap()
        {
            //Initializing the data structuref
            for (int i = 0; i < capacity; i++)
                bucket.Add(null);
        }

        //Asking the CLR for a hash code and taking a mod
        private Int32 GetIndex(K key)
        {
            return key.GetHashCode() % capacity;
        }

        public V Get(K key)
        {
            //Use the hash function to get index of the given key
            int indexOfKey = GetIndex(key);
            MapNode<K, V> pair = bucket[indexOfKey];

            //Travese the node in case we might have had a collision and we had to store the key somewhere in the MapNode
            while (pair != null)
            {
                if (key.Equals(pair.Key))
                    return pair.Value;
                pair = pair.next;
            }
            return default(V);
        }

        public void Put(K key, V value)
        {
            int indexOfKey = GetIndex(key);
            var toInsertKvPair = new MapNode<K, V>(key, value);

            //Check if the retrieved index is empty, if so set the bucket index to the current kvpair
            var pair = bucket[indexOfKey];
            if (pair == null)
            {
                bucket[indexOfKey] = toInsertKvPair;
                setKeys.Add(key);
                currentSize++;
            }

            //Else, look for the key in the list, if it is present update it with the new value
            else
            {
                while(pair != null)
                {
                    if (key.Equals(pair.Key))
                    {
                        bucket[indexOfKey] = toInsertKvPair;
                        currentSize++;
                        break;
                    }
                    else pair = pair.next;
                }

                //If the kay was not already present, we had a collision, resolve collision
                if (pair == null)
                {
                    pair = bucket[indexOfKey];
                    toInsertKvPair.next = pair;
                    setKeys.Add(key);
                    bucket[indexOfKey] = toInsertKvPair;
                    currentSize++;
                }
            }

            //Resize if needed
            Resize();
        }

        public bool Delete(K key)
        {
            //Return false if index not present
            int indexOfKey = GetIndex(key);
            var pair = bucket[indexOfKey];

            if (pair == null)
                return false;

            //Check if current node is the node we were looking for, if so jut shift values
            if(key.Equals(pair.Key))
            {
                var value = pair.Value;
                pair = pair.next;
                bucket[indexOfKey] = pair;
                setKeys.Remove(key);
                currentSize--;
                return true;
            }

            //Check along the linked list if the key is present
            else
            {
                MapNode<K, V> lastNode = null;
                while(pair != null)
                {
                    //Shift nodes along the linked list once you find the key
                    if (pair.Key.Equals(key))
                    {
                        lastNode.next = pair.next;
                        currentSize--;
                        setKeys.Remove(key);
                        return true;
                    }
                    lastNode = pair;
                    pair = pair.next;
                }
                currentSize--;
                return false;
            }
        }

        //Returns a random key out of the existing keys in our data structure
        public K GetRandomKey()
        {
            if (setKeys.Count == 0)
                throw new IndexOutOfRangeException();
            var randIndex = new Random().Next(setKeys.Count);
            return setKeys[randIndex];
        }


        //Resize if the buckets are more than 80 % filled
        private void Resize()
        {

            if(currentSize * 1.0 / capacity > 0.8)
            {
                //Create a new bucket
                List<MapNode<K, V>> tmp = bucket;
                bucket = new List<MapNode<K, V>>();

                //Double the capacity
                capacity = 2 * capacity;
                for (int i = 0; i < capacity; i++)
                {
                    bucket.Add(null);
                }


                foreach (var kvPair in tmp)
                {
                    var tmpKvPair = kvPair;
                    while (tmpKvPair != null)
                    {
                        Put(tmpKvPair.Key, tmpKvPair.Value);
                        tmpKvPair = tmpKvPair.next;
                    }
                }
            }
        }
    }
}
