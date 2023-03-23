using HashTable;
using KeyValuePair = System.Collections.Generic.KeyValuePair;
namespace Hashtable;

/*Маючи зв'язний список ми можемо реалізувати вже і основну структуру - словник.
Пам'ятайте, що додавання у словник відбувається за наступним алгоритмом:
Отримати ключ та значення для додавання
1)Знайти хеш від ключу
2)Знайти відповідну корзину (bucket) для додавання - 
  для цього достатньо просто знайти залишок від ділення хешу на кількість бакетів
3)Якщо в цій корзині вже є зв'язний список - додати новий елемент в кінець,
 якщо ще ні - створити список та додати
4)Доступ до елемента за ключем:
Отримати ключ
1)Знайти хеш від ключу
2)Знайти відповідну корзину (bucket) для додавання
3)Якщо в цій корзині вже є зв'язний список - спробувати дістати зі списку елемент з таким ключем,
  якщо ще ні - повернути null*/
public class Dictionary
{
    private const int InitialSize = 10;
    private List<LinkedList> buckets_10 = new List<LinkedList>(InitialSize);
    private List<LinkedList> _buckets = new List<LinkedList>();
    private int num_buckets = 0;
    private double load_factor = 0.85;
    private int num_load_factor = 0;

    public void Add(string key, string value)
    {

        if (num_buckets == 0)
        {
            for (int i = 0; i < buckets_10.Capacity; i++) buckets_10.Add(new LinkedList());
            _buckets = buckets_10;
        }
        
        if ( (num_buckets / _buckets.Count) >= load_factor )
        {
            _buckets.AddRange(buckets_10);
            num_load_factor++;
            num_buckets = num_load_factor * 10 - 1;
        }

        // int hash = CalculateHash(key);
        long hash = CalculateHash(key);
        // var index = hash % _buckets.Count + (10 * num_load_factor);
        int index = Convert.ToInt32((hash % 10 + (10 * num_load_factor)));

        /*
        var value = _buckets[index];
        if (value == null)
        {
            value = new LinkedList();
        }
        value.Add(new KeyValuePair(key, value));
        */
        
        var new_KVP = new HashTable.KeyValuePair(key, value);
        
        if (_buckets[index] == null)    
        {
            var new_LL = new LinkedList();
            new_LL.Add(new_KVP);
            _buckets[index] = new_LL;
        }
        
        _buckets[index].Add(new_KVP);
        num_buckets++;
    }
    
    public void Remove(string key)
    {
        //int hash = CalculateHash(key);
        long hash = CalculateHash(key);
        int index = Convert.ToInt32(hash % _buckets.Count);

        if (_buckets[index] != null)
        {
            _buckets[index].RemoveByKey(key);
        }
        num_buckets--;
    }

    
    public string Get(string key)
    {
        //int hash = CalculateHash(key);
        long hash = CalculateHash(key);
        int index = Convert.ToInt32(hash % _buckets.Count);

        if (_buckets[index] != null)
        {
            HashTable.KeyValuePair pair = _buckets[index].GetItemWithKey(key);
            
            if (pair != null)
            {
                return pair.Value;
            }
        }

        return null;
    }

    
    //private int CalculateHash(string key)
    private long CalculateHash(string key)
    {
        //int hash = 0;
        long hash = 0;
        foreach (char c in key)
        {
            hash = hash * 13 + c;
        }
        hash = hash + key.Length;
        return hash;
    }
}
