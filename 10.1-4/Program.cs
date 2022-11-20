// See https://aka.ms/new-console-template for more information
void SortBubble(NumberArray numbers)
{
    int i, j, temp;
    for (i = numbers.array.Length - 1; i > 0; i--)
        for (j = 0; j < i; j++)
        {
            if (numbers.array[j] > numbers.array[j + 1])
            {
                temp = numbers.array[j];
                numbers.array[j] = numbers.array[j + 1];
                numbers.array[j + 1] = temp;
            }
        }
}

void SortInsert(NumberArray numbers)
{
    int i, j, temp;
    for (i = 1; i < numbers.array.Length; i++)
        for (j = i; j > 0; j--)
        {
            if (numbers.array[j] > numbers.array[j - 1])
                break;
            temp = numbers.array[j];
            numbers.array[j] = numbers.array[j - 1];
            numbers.array[j - 1] = temp;
        }
}

void SortShell(NumberArray numbers)
{
    int i, step, temp, cur, k = 1, c;
    int len = numbers.array.Length;
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
                if (numbers.array[cur - step] > numbers.array[cur])
                {
                    temp = numbers.array[cur];
                    numbers.array[cur] = numbers.array[cur - step];
                    numbers.array[cur - step] = temp;
                    cur = cur - step;
                }
                else break;
            }
        }
    }
}
class NumberArray
{
    public int[] array;
    public NumberArray(int[] array)
    {
        this.array = array;
    }
}


delegate void SortDelegate(NumberArray numbers);
