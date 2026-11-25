using System;

namespace NumericalMethodsLab4
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            //Прогон рабочей системы, ожидание: (1; 2; 3)
            
            Console.WriteLine("Задание.1 - Рабочая система (eps = 1e-6)");
            double[,] A1 = {
                { 10, 1, 2 },
                { 2, 10, 1 },
                { 1, 2, 10 }
            };
            double[] b1 = { 18, 25, 35 };

            var (res1, steps1) = Jacobi(A1, b1, 1e-6);
            Console.WriteLine($"Итог: x = {res1[0]:F6}; y = {res1[1]:F6}; z = {res1[2]:F6} (шагов: {steps1})");

            //Цена точности (eps = 1e-2, 1e-4, 1e-6)
            
            Console.WriteLine("Задание.2 - Цена точности");
            double[] epsilons = { 1e-2, 1e-4, 1e-6 };
            foreach (double eps in epsilons)
            {
                var (_, steps) = Jacobi(A1, b1, eps, printSteps: false);
                Console.WriteLine($"Точность eps = {eps:E0} -> итераций: {steps}");
            }
            Console.WriteLine();


            //Разлёт (переставленная система: x + 3y = 14; 5x + y = 14)
            
            Console.WriteLine("Задание.2 - Разлёт метода");
            double[,] A3 = {
                { 1, 3 },
                { 5, 1 }
            };
            double[] b3 = { 14, 14 };

            var (_, steps3) = Jacobi(A3, b3, 1e-6, maxSteps: 10);
            Console.WriteLine($"Статус сходимости: {(steps3 == -1 ? "Разлет не сошлось за 10 шагов" : "Сошлось")}");
            
            //Своя система 3x3 с целым решением (2; -1; 4)
            
            Console.WriteLine("Задание.2 - Своя система");
            
            double[,] A4 = {
                { 10, -2,  1 },
                {  3, 12, -2 },
                {  1,  2,  8 }
            };
        
            double[] b4 = { 26, -14, 32 };

            var (res4, steps4) = Jacobi(A4, b4, 1e-6);
            Console.WriteLine($"Итог: x = {res4[0]:F6}; y = {res4[1]:F6}; z = {res4[2]:F6} (шагов: {steps4})");

            Console.WriteLine("Проверка подстановкой:");
            for (int i = 0; i < b4.Length; i++)
            {
                double check = 0;
                for (int j = 0; j < b4.Length; j++)
                    check += A4[i, j] * res4[j];
                Console.WriteLine($"Уравнение {i + 1}: получено {check:F6}, ожидалось {b4[i]}");
            }
        }

        static double[] JacobiStep(double[,] A, double[] b, double[] x)
        {
            int n = b.Length;
            double[] next = new double[n];
            for (int i = 0; i < n; i++)
            {
                double s = 0;
                for (int j = 0; j < n; j++)
                    if (j != i)
                        s += A[i, j] * x[j];
                next[i] = (b[i] - s) / A[i, i];
            }
            return next;
        }

        static (double[] x, int steps) Jacobi(double[,] A, double[] b,
            double eps, int maxSteps = 100, bool printSteps = true)
        {
            int n = b.Length;
            double[] x = new double[n];               
            for (int k = 1; k <= maxSteps; k++)
            {
                double[] next = JacobiStep(A, b, x);
                double d = 0;
                for (int i = 0; i < n; i++)
                    d = Math.Max(d, Math.Abs(next[i] - x[i]));
                x = next;

                if (printSteps)
                {
                    if (n == 3)
                        Console.WriteLine($"{k,2} {x[0],10:F5} {x[1],10:F5} {x[2],10:F5} d = {d:E1}");
                    else
                        Console.WriteLine($"{k,2} {x[0],10:F5} {x[1],10:F5} d = {d:E1}");
                }

                if (d < eps) return (x, k);
            }
            return (x, -1);  
        }
    }
}