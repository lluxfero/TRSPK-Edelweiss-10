// See https://aka.ms/new-console-template for more information
void SortBubble(NumberArray numbers) // сортировка пузырьком
{
    int i, j, temp;
    for (i = numbers.Amount - 1; i > 0; i--)
        for (j = 0; j < i; j++)
        {
            if (numbers[j] > numbers[j + 1])
            {
                temp = numbers[j];
                numbers[j] = numbers[j + 1];
                numbers[j + 1] = temp;
            }
        }
}

void SortInsert(NumberArray numbers) // сортировка вставками
{
    int i, j, temp;
    for (i = 1; i < numbers.Amount; i++)
        for (j = i; j > 0; j--)
        {
            if (numbers[j] > numbers[j - 1])
                break;
            temp = numbers[j];
            numbers[j] = numbers[j - 1];
            numbers[j - 1] = temp;
        }
}

void SortShell(NumberArray numbers) // сортировка Шелла
{
    int i, step, temp, cur, k = 1, c;
    int len = numbers.Amount;
    while (len / Math.Pow(2, k) >= 2)
        k++;
    for (i = 1; i <= k; i++)
    {
        c = 0;
        step = len / (int)Math.Pow(2, i);
        while (step + c < len)
        {
            cur = step + c;
            c++;
            while (cur - step >= 0)
            {
                if (numbers[cur - step] > numbers[cur])
                {
                    temp = numbers[cur];
                    numbers[cur] = numbers[cur - step];
                    numbers[cur - step] = temp;
                    cur = cur - step;
                }
                else break;
            }
        }
    }
}

delegate void SortDelegate(NumberArray numbers); // делегат, который совпадает по сигнатуре с сортировками

public class NumberArray
{
    // Свойство для получения кол-ва элементов в массиве (для удобства)
    public int Amount { get; }

    private int[] _array;

    // Конструктор, в котором выделяется место под массив размера [amount] и заполняется рандомными числами от 0 до 100
    public NumberArray(int amount)
    {
        Amount = amount;
        Random rand = new();
        _array = new int[amount];

        for (int i = 0; i < _array.Length; i++)
        {
            _array[i] = rand.Next() % 100;
        }
    }

    public void Sort(SortDelegate sort)
    {
        // вызов делегата c проверкой на NULL (запускается функция сортировки)
        sort?.Invoke(this);
    }

    // Индексатор
    public int this[int i]
    {
        get
        {
            if (i < _array.Length && i >= 0)
                return _array[i];
            else
                throw new ArgumentOutOfRangeException();
        }
        set
        {
            if (i < _array.Length && i >= 0)
                _array[i] = value;
            else
                throw new ArgumentOutOfRangeException();
        }
    }

    // Метод копирования(по значению)
    public int[] Copy()
    {
        int[] array = new int[_array.Length];
        for (int i = 0; i < _array.Length; i++)
        {
            array[i] = _array[i];
        }
        return array;
    }
}


