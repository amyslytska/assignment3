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
    private List <LinkedList> buckets_10 = new List <LinkedList>(InitialSize);
    private List<LinkedList> _buckets = new List <LinkedList>();
    private int num_buckets = 0;
    private double load_factor = 0.7;
    private int num_load_facktor = 0;
        
    public void Add(string key, string value)
    {

        if (num_buckets == 0)_buckets = buckets_10;
        if ((num_buckets / _buckets.Count) >= load_factor) 
        {
            _buckets.AddRange(buckets_10); 
            num_load_facktor ++;
            num_buckets = num_load_facktor * 10 - 1;  
        }

        int hash = CalculateHash(key);
        var index = (hash % _buckets.Length) + (10 * num_load_facktor);  
  

        var value = _buckets[index];


        if (value == null)
        {
            value = new LinkedList();
        }
        value.Add(new KeyValuePair(key, value));

        num_buckets ++;
    }
        


    public void Remove(string key)
    {
        int hash = CalculateHash(key);
        int index = hash % _buckets.Length;

        if (_buckets[index] != null)
        {
            _buckets[index].RemoveByKey(key);
        } 
        num_buckets --;       
    }
