using System;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        double eps = 1e-6;

        double[,] A1 = {
            { 10, 1, 2 },
            { 2, 10, 1 },
            { 1, 2, 10 }
        };
        double[] b1 = { 18, 25, 35 };

        double[,] A2 = {
            { 3, 1, 1 },
            { 1, 3, 1 },
            { 1, 1, 3 }
        };
        double[] b2 = { 8, 10, 12 };

        double[,] A3 = {
            { 3, 1 },
            { 1, 4 }
        };
        double[] b3 = { 5, 9 };

        Console.WriteLine("Задание.1: Прогон рабочей системы (Зейдель, eps = 1e-6)");
        var (res1, steps1) = Seidel(A1, b1, eps, printLog: true);
        Console.WriteLine($"Ответ: ({res1[0]:F5}; {res1[1]:F5}; {res1[2]:F5}), шагов: {steps1}");

        Console.WriteLine("Задание.2: Сравнение базовых итераций и Зейделя (eps = 1e-6)");
        Console.WriteLine($"{"Система"} {"Базовый"} {"Зейдель"}");
        
        var (_, jSteps1) = Jacobi(A1, b1, eps);
        var (_, sSteps1) = Seidel(A1, b1, eps);
        Console.WriteLine($"{"10x + y + 2z = 18; 2x + 10y + z = 25; x + 2y + 10z = 35",-58} {jSteps1,-8} {sSteps1,-8}");

        var (_, jSteps2) = Jacobi(A2, b2, eps);
        var (_, sSteps2) = Seidel(A2, b2, eps);
        Console.WriteLine($"{"3x + y + z = 8; x + 3y + z = 10; x + y + 3z = 12",-58} {jSteps2,-8} {sSteps2,-8}");

        var (_, jSteps3) = Jacobi(A3, b3, eps);
        var (_, sSteps3) = Seidel(A3, b3, eps);
        Console.WriteLine($"{"3x + y = 5; x + 4y = 9",-58} {jSteps3,-8} {sSteps3,-8}");

        Console.WriteLine("Задание.3: Ловушка порядка строк (лимит 10 шагов)");
        double[,] A_trap = {
            { 2, 10, 1 },
            { 10, 1, 2 },
            { 1, 2, 10 }
        };
        double[] b_trap = { 25, 18, 35 };
        var (resTrap, stepsTrap) = Seidel(A_trap, b_trap, eps, maxSteps: 10, printLog: true);
        Console.WriteLine(stepsTrap == -1 ? "Результат: Метод разошёлся (лимит 10 шагов исчерпан)." : $"Сошлось за {stepsTrap}");

        Console.WriteLine("Задание.4: Цена точности на рабочей системе");
        double[] tolerances = { 1e-2, 1e-4, 1e-6 };
        foreach (double e in tolerances)
        {
            var (_, jSt) = Jacobi(A1, b1, e);
            var (_, sSt) = Seidel(A1, b1, e);
            Console.WriteLine($"Точность eps = {e:E0} -> Базовый: {jSt,2} итер. Зейдель: {sSt,2} итер.");
        }
    }

    static (double[] x, int steps) Seidel(double[,] A, double[] b, double eps, int maxSteps = 100, bool printLog = false)
    {
        int n = b.Length;
        double[] x = new double[n];
        for (int k = 1; k <= maxSteps; k++)
        {
            double d = 0;
            for (int i = 0; i < n; i++)
            {
                double old = x[i];
                double s = 0;
                for (int j = 0; j < n; j++)
                    if (j != i) s += A[i, j] * x[j]; 
                x[i] = (b[i] - s) / A[i, i];
                d = Math.Max(d, Math.Abs(x[i] - old));
            }
            if (printLog)
            {
                string coords = string.Join(" ", Array.ConvertAll(x, val => $"{val,11:F5}"));
                Console.WriteLine($"{k,2}  {coords}  d = {d:E1}");
            }
            if (d < eps) return (x, k);
        }
        return (x, -1);
    }

    static (double[] x, int steps) Jacobi(double[,] A, double[] b, double eps, int maxSteps = 100)
    {
        int n = b.Length;
        double[] x = new double[n];
        for (int k = 1; k <= maxSteps; k++)
        {
            double[] xNew = new double[n]; 
            double d = 0;
            for (int i = 0; i < n; i++)
            {
                double s = 0;
                for (int j = 0; j < n; j++)
                    if (j != i) s += A[i, j] * x[j]; 
                xNew[i] = (b[i] - s) / A[i, i];
                d = Math.Max(d, Math.Abs(xNew[i] - x[i]));
            }
            x = xNew;
            if (d < eps) return (x, k);
        }
        return (x, -1);
    }
}