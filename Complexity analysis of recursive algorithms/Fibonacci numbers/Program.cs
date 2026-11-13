using System;
using System.Diagnostics;

int n = 35; 
long calls = 0;

var sw = Stopwatch.StartNew();
long naive = FibNaive(n);
sw.Stop();
long naiveMs = sw.ElapsedMilliseconds;
long naiveCalls = calls;

calls = 0;
sw = Stopwatch.StartNew();
long[] cache = new long[n + 1];
long fast = FibMemo(n, cache);
sw.Stop();
Console.WriteLine(n + " : наивный " + naiveCalls + " вызовов, " + naiveMs + " мс" + " | кэш: " + calls + " вызовов, " + sw.ElapsedMilliseconds + " мс" + " | результат " + naive + " = " + fast);
long FibNaive(int k)
{
    calls++; 
    if (k <= 1) return k;
    return FibNaive(k - 1) + FibNaive(k - 2);
}

long FibMemo(int k, long[] cache)
{
    calls++;
    if (k <= 1) return k;
    if (cache[k] != 0) return cache[k]; 
    cache[k] = FibMemo(k - 1, cache) + FibMemo(k - 2, cache);
    return cache[k];
}