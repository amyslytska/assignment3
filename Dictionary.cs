namespace HashTable;

public class Dictionary
{
    private static int InitialSize = 10;

    private LinkedList[] _buckets = new LinkedList[InitialSize];
    private const float LoadFactorThreshold = 0.75f;
    private int _occupied;
    private float LoadFactor => (float)_occupied / InitialSize;

    public void Add(string key, string value)
    {
        if (LoadFactor >= LoadFactorThreshold) Expansion();
        int hash = CalculateHash(key,_buckets.Length);

        if (_buckets[hash] == null)
        {
            _buckets[hash] = new LinkedList();
        }
        _buckets[hash].Add(new KeyValuePair(key, value));
    }
    private void Expansion()
    {
        InitialSize *= 2;
        var addBuckets = new LinkedList[InitialSize];
        foreach (LinkedList element in _buckets)
        {
            if (element == null) continue;
            foreach (KeyValuePair pair in element)
            {
                var newBucketIndex = CalculateHash(pair.Key, addBuckets.Length);
                if (addBuckets[newBucketIndex] == null)
                {
                    addBuckets[newBucketIndex] = new LinkedList();
                }

                addBuckets[newBucketIndex].Add(pair);
            }
            _occupied++; // update _occupied for each non-null element
        }

        _buckets = addBuckets;
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
        // Added modulo operator to limit hash within array bounds
        // and added arraySize check to avoid dividing by 0.
        hash = ((hash % arraySize) + arraySize) % arraySize;
        return hash;
    }
}