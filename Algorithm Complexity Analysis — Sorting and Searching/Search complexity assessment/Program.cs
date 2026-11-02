using System;
using System.Diagnostics;

class Program
{
    static long comparisons = 0;

    static void Main()
    {
        int[] sizes = { 1000, 10000, 100000, 1000000 };

        Console.WriteLine("n\t\tЛин.есть\tЛин.нет\t\tДвоич.есть\tДвоич.нет\t\tВремя лин(мс)\tВремя двоич(мс)");

        foreach (int n in sizes)
        {
            int[] a = new int[n];
            for (int i = 0; i < n; i++) a[i] = i * 2 + 1;
            
            comparisons = 0;
            var sw = Stopwatch.StartNew();
            LinearSearch(a, a[n - 1]);
            sw.Stop();
            long linHit = comparisons;
            double linHitMs = sw.Elapsed.TotalMilliseconds;
            
            comparisons = 0;
            sw.Restart();
            LinearSearch(a, a[n - 1] + 1);
            sw.Stop();
            long linMiss = comparisons;
            double linMissMs = sw.Elapsed.TotalMilliseconds;
            
            comparisons = 0;
            sw.Restart();
            BinarySearch(a, a[n - 1]);
            sw.Stop();
            long binHit = comparisons;
            double binHitMs = sw.Elapsed.TotalMilliseconds;
            
            comparisons = 0;
            sw.Restart();
            BinarySearch(a, a[n - 1] + 2);
            sw.Stop();
            long binMiss = comparisons;
            double binMissMs = sw.Elapsed.TotalMilliseconds;

            Console.WriteLine($"{n}\t\t{linHit}\t\t{linMiss}\t\t{binHit}\t\t{binMiss}\t\t" + $"{linHitMs:F3}\t\t{binHitMs:F3}");
        }
    }

    static int LinearSearch(int[] a, int key)
    {
        for (int i = 0; i < a.Length; i++)
        {
            comparisons++;
            if (a[i] == key) return i;
        }
        return -1;
    }

    static int BinarySearch(int[] a, int key)
    {
        int left = 0;
        int right = a.Length - 1;
        while (left <= right)
        {
            int mid = (left + right) / 2;
            comparisons++;
            if (a[mid] == key) return mid;
            if (a[mid] < key) left = mid + 1;
            else right = mid - 1;
        }
        return -1;
    }
}