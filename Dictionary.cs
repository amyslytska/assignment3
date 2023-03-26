namespace HashTable;

public class Dictionary
{
    private const int InitialSize = 10;

    private LinkedList[] _buckets = new LinkedList[InitialSize];

    public void Add(string key, string value)
    {
        int hash = CalculateHash(key,_buckets.Length);

        if (_buckets[hash] == null)
        {
            _buckets[hash] = new LinkedList();
        }
        _buckets[hash].Add(new KeyValuePair(key, value));
    }

    public void Remove(string key)
    {
        int hash = CalculateHash(key,_buckets.Length);

        if (_buckets[hash] != null)
        {
            _buckets[hash].RemoveByKey(key);
        }        
    }

    public string Get(string key)
    {
        int hash = CalculateHash(key,_buckets.Length);

        if (_buckets[hash] != null)
        {
            KeyValuePair pair = _buckets[hash].GetItemWithKey(key);
            if (pair != null)
            {
                return pair.Value;
            }
        }
        return null; 
    }


    private int CalculateHash(string key, int arraySize)
    {
        int hash = 0;
        foreach (char c in key)
        {
            hash = hash * 31 + c; // Changed the constant multiplier to 31 for better distribution
        }
        hash = (hash + key.Length) % arraySize; // Added modulo operator to limit hash within array bounds
        return hash;
    }
}