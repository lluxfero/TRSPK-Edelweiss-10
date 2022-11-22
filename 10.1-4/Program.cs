using System.Diagnostics;

NumberArray numberArray = new(10);

// Добавление в делегат функции BubbleSort
SortDelegate delegates = SortBubble;

#region Демонстрация функционала

// Копирование массива
NumberArray newArr = numberArray.Copy();

// Вывод изначального состояния
Console.Write($"Изначальный массив: {numberArray}");

// Сортировка через делегат 
numberArray.Sort(delegates);

// Вывод после сортировки
Console.Write($"Отсортированный массив: {numberArray}");

// Вывод скопированного массива
Console.WriteLine($"Скопированный массив: {newArr}");
#endregion

//номер 10.4
SortDelegate delegateBubble = SortBubble;
SortDelegate delegateInsert = SortInsert;
SortDelegate delegateShell = SortShell;

NumberArray arr = new(25);
while (true)
{
    switch (Menu())
    {
        case "1":
            Console.Write($"\n--Sort Bubble--\nИзначальный массив: {arr}");
            NumberArray arrCopy = arr.Copy();
            arrCopy.Sort(delegateBubble);
            Console.Write($"Отсортированный массив: {arrCopy}");
            break;

        case "2":
            Console.Write($"\n--Sort Insert--\nИзначальный массив: {arr}");
            arrCopy = arr.Copy();
            arrCopy.Sort(delegateInsert);
            Console.Write($"Отсортированный массив: {arrCopy}");
            break;

        case "3":
            Console.Write($"\n--Sort Shell--\nИзначальный массив: {arr}");
            arrCopy = arr.Copy();
            arrCopy.Sort(delegateShell);
            Console.Write($"Отсортированный массив: {arrCopy}");
            break;

        default:
            return 0;
    }
}


void SortBubble(NumberArray numbers) // сортировка пузырьком
{
    int i, j, temp;
    Stopwatch time = new Stopwatch();
    time.Start();
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
    time.Stop();
    Console.WriteLine($"Сортировка заняла {time.Elapsed.TotalMilliseconds} миллисекунд");
}

void SortInsert(NumberArray numbers) // сортировка вставками
{
    int i, j, temp;
    Stopwatch time = new Stopwatch();
    time.Start();
    for (i = 1; i < numbers.Amount; i++)
        for (j = i; j > 0; j--)
        {
            if (numbers[j] > numbers[j - 1])
                break;
            temp = numbers[j];
            numbers[j] = numbers[j - 1];
            numbers[j - 1] = temp;
        }
    time.Stop();
    Console.WriteLine($"Сортировка заняла {time.Elapsed.TotalMilliseconds} миллисекунд");
}

void SortShell(NumberArray numbers) // сортировка Шелла
{
    int i, step, temp, cur, k = 1, c;
    int len = numbers.Amount;
    Stopwatch time = new Stopwatch();
    time.Start();
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
    time.Stop();
    Console.WriteLine($"Сортировка заняла {time.Elapsed.TotalMilliseconds} миллисекунд");
}

string Menu()
{
    Console.Write("\n===== MENU =====\n1. SortBubble\n2. SortInsert\n" +
           "3. SortShell\nPress another key to exit\n");
    Console.Write("Введите значение: ");
    string? key = Console.ReadLine();
    return key;
}


public delegate void SortDelegate(NumberArray numbers); // делегат, который совпадает по сигнатуре с сортировками

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

    // Метод копирования
    public NumberArray Copy()
    {
        NumberArray array = new(_array.Length);
        for (int i = 0; i < _array.Length; i++)
        {
            array[i] = _array[i];
        }
        return array;
    }

    public NumberArray Copy2()
    {
        NumberArray array = new(_array.Length);
        for (int i = 0; i < _array.Length; i++)
        {
            array[i] = _array[i];
        }
        return array;
    }

    public override string ToString()
    {
        string s = "";
        for (int i = 0; i < Amount; i++)
        {
            s = string.Concat(s, $"{_array[i]} ");
        }
        s = string.Concat(s, "\n");
        return s;
    }
}


