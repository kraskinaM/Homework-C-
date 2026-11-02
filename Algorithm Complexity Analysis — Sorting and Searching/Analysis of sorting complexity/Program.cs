using System;
using System.Diagnostics;
 
long comparisons = 0;
int[] sizes = { 1000, 2000, 4000, 8000, 16000 };
Random rnd = new Random(12345); 
 
Console.WriteLine("n; пузырёк, мс; вставки, мс; Array.Sort, мс");
foreach (int n in sizes)
{
    int[] source = new int[n];
    for (int i = 0; i < n; i++) source[i] = rnd.Next(0, 1000000);
 
    int[] a1 = Copy(source);
    int[] a2 = Copy(source);
    int[] a3 = Copy(source);
 
    var sw = Stopwatch.StartNew();
    BubbleSort(a1);
    sw.Stop();
    double bubbleMs = sw.Elapsed.TotalMilliseconds; 
 
    sw = Stopwatch.StartNew();
    InsertionSort(a2);
    sw.Stop();
    double insertMs = sw.Elapsed.TotalMilliseconds;
 
    sw = Stopwatch.StartNew();
    Array.Sort(a3);
    sw.Stop();
    double stdMs = sw.Elapsed.TotalMilliseconds;
 
    Console.WriteLine($"{n}; {bubbleMs:F3}; {insertMs:F3}; {stdMs:F3}");
}

int n5 = 16000;
int[] sortedSource = new int[n5];
for (int i = 0; i < n5; i++) sortedSource[i] = i;

long comparisons5 = 0; 
var sw5 = Stopwatch.StartNew();
InsertionSort(sortedSource);
sw5.Stop();

Console.WriteLine("Задание 5");
Console.WriteLine($"Вставки на отсортированном массиве (16000): {sw5.Elapsed.TotalMilliseconds:F3} мс");
Console.WriteLine($"Сравнений: {comparisons5}"); 

static int[] Copy(int[] src)
{
    int[] dst = new int[src.Length];
    for (int i = 0; i < src.Length; i++) dst[i] = src[i];
    return dst;
}
 
static void BubbleSort(int[] a)
{
    int n = a.Length;
    for (int i = 0; i < n - 1; i++)
    for (int j = 0; j < n - 1 - i; j++)
        if (a[j] > a[j + 1])
        {
            int t = a[j]; a[j] = a[j + 1]; a[j + 1] = t;
        }
}
 
static void InsertionSort(int[] a)
{
    for (int i = 1; i < a.Length; i++)
    {
        int key = a[i];
        int j = i - 1;
        while (j >= 0 && a[j] > key)
        {
            a[j + 1] = a[j];
            j = j - 1;
        }
        a[j + 1] = key;
    }
}