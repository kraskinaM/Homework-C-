using System;

class Program
{
    static double F (double x) => Math.Pow(x, 3) + x - 3;

    static void Main()
    {
        for (double x = 0; x <= 2; x += 0.5)
        {
            Console.WriteLine($"{x:F1} {F(x):F4}");
        }
        Console.WriteLine();
        
        double a = 1.0, b = 1.5;
        for (int step = 1; step <= 3; step++)
        {
            double m = (a + b) / 2;
            double fm = F(m);
            
            Console.WriteLine($"{step} {a:F4} {m:F4} {b:F4} {fm:F4}");

            if (F(a) * fm < 0) b = m;
            else a = m;
        }
        Console.WriteLine($"[{a:F4}; {b:F4}]");
        
        double len = 0.5;
        Console.WriteLine(Math.Ceiling(Math.Log2(len / 0.01)));
        Console.WriteLine(Math.Ceiling(Math.Log2(len / 1e-6)));
        Console.WriteLine();
        
        double finalM = (a + b) / 2;
        Console.WriteLine($"{finalM:F4} {F(finalM):F4}");
    }
}