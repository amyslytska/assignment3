namespace HashTable;

public class KeyValuePair
{
    public string Key { get; }

    public string Value { get; }

    public KeyValuePair(string key, string value)
    {
        Key = key;
        Value = value;
    }
}
public class LinkedListNode
{
    public KeyValuePair Pair { get; }
        
    public LinkedListNode Next { get; set; }

    public LinkedListNode(KeyValuePair pair, LinkedListNode next = null)
    {
        Pair = pair;
        Next = next;
    }
}
public class LinkedList
{
    private LinkedListNode _first;
    private LinkedListNode _last;

    public void Add(KeyValuePair pair)
    {
        if (_first == null)
        {
            _first = new LinkedListNode(pair);
            _last = _first;
            return; //
        }

        _last.Next = new LinkedListNode(pair);
        _last = _last.Next;
        /*
        _last = _first;
        while (_last.Next != null)
            _last = _last.Next;
        _last.Next = new LinkedListNode(pair);
        _last = _last.Next;
        */
    }

    public void RemoveByKey(string key)
    {
        if (_first.Pair.Key == key)
        {
            _first = _first.Next; // set the first node to the next node
            if (_first == null)
            {
                _last = null;
            }
            return;
        }
        
        LinkedListNode current = _first;
        while (current.Next != null)
        {
            if (current.Next.Pair.Key == key)
            {
                current.Next = current.Next.Next; // remove the next node
                if (current.Next == null)
                {
                    _last = current; // update the reference to the last node
                }
                return;
            }
            current = current.Next; // move to the next node
        }
    }

    public KeyValuePair GetItemWithKey(string key)
    {
        // get pair with provided key, return null if not found
        LinkedListNode current = _first;
        while (current != null)
        {
            if (current.Pair.Key == key)
            {
                return current.Pair;
            }
            current = current.Next;
        }
        return null;
    }
}
