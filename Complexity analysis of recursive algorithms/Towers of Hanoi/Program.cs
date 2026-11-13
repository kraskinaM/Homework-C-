using System;
using System.Diagnostics;
 
long moves = 0;
 
void Hanoi(int n, char from, char to, char spare)
{
    if (n == 0) return;             
    Hanoi(n - 1, from, spare, to);  
    moves++;                          
    Hanoi(n - 1, spare, to, from);   
}
 
int[] sizes = { 5, 10, 15, 20, 25 };
foreach (int n in sizes)
{
    moves = 0;
    var sw = Stopwatch.StartNew();
    Hanoi(n, 'A', 'C', 'B');
    sw.Stop();
    Console.WriteLine(n + " дисков: " + moves + " ходов, " + sw.ElapsedMilliseconds + " мс");
}