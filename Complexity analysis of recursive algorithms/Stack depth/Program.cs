using System;
 
int n = 100000; 
long calls = 0;
 
long Fact(int k)
{
    calls++;
    if (k <= 1) return 1;
    return k * Fact(k - 1);
}
 
long last = Fact(n);
Console.WriteLine("n=" + n + ": посчитано, вызовов " + calls + ", остаток " + last % 10);