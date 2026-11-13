using System;

namespace NumericalMethods_Topic3
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            SolveGaussTask();
            SolveSeidelTask();
        }

        static void SolveGaussTask()
        {
            Console.WriteLine("1.Метод Гаусса");

            double[,] matrix = {
                { 1,  2,  1,  8 },
                { 2,  1, -1,  1 },
                { 1, -1, -1, -4 }
            };

            int n = 3;

            for (int k = 0; k < n - 1; k++)
            {
                if (Math.Abs(matrix[k, k]) < 1e-12)
                {
                    for (int i = k + 1; i < n; i++)
                    {
                        if (Math.Abs(matrix[i, k]) > 1e-12)
                        {
                            for (int j = 0; j <= n; j++)
                            {
                                (matrix[k, j], matrix[i, j]) = (matrix[i, j], matrix[k, j]);
                            }
                            break;
                        }
                    }
                }

                for (int i = k + 1; i < n; i++)
                {
                    double factor = matrix[i, k] / matrix[k, k];
                    for (int j = k; j <= n; j++)
                    {
                        matrix[i, j] -= factor * matrix[k, j];
                    }
                }
            }

            Console.WriteLine("Треугольная матрица после прямого хода:");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"{matrix[i, 0],6:F1} {matrix[i, 1],6:F1} {matrix[i, 2],6:F1} {matrix[i, 3],6:F1}");
            }

            double[] x = new double[n];
            for (int i = n - 1; i >= 0; i--)
            {
                double sum = 0;
                for (int j = i + 1; j < n; j++)
                {
                    sum += matrix[i, j] * x[j];
                }
                x[i] = (matrix[i, n] - sum) / matrix[i, i];
            }

            Console.WriteLine($"Решение методом Гаусса: x = {x[0]:F2}; y = {x[1]:F2}; z = {x[2]:F2}");
        }

        static void SolveSeidelTask()
        {
            Console.WriteLine("2.Метод Зейделя");

            double x = 0.0;
            double y = 0.0;

            Console.WriteLine("Итерация x y");
            for (int iter = 1; iter <= 3; iter++)
            {
                x = (10.0 - y) / 4.0;
                y = (12.0 - x) / 3.0; 
                Console.WriteLine($"{iter,8} {x,9:F3} {y,9:F3}");
            }

            double exactX = 18.0 / 11.0;
            double exactY = 38.0 / 11.0;

            double jacobiX = (10.0 - 0.0) / 4.0;
            double jacobiY = (12.0 - 0.0) / 3.0;

            Console.WriteLine($"\nИстинное решение: x = {exactX:F3}; y = {exactY:F3}");
            Console.WriteLine($"Отклонение Зейделя (3-й шаг): dx = {Math.Abs(exactX - x):F3}, dy = {Math.Abs(exactY - y):F3}");
            Console.WriteLine($"1-я итерация Якоби: x = {jacobiX:F3}; y = {jacobiY:F3}");
            Console.WriteLine($"Отклонение Якоби (1-й шаг): dx = {Math.Abs(exactX - jacobiX):F3}, dy = {Math.Abs(exactY - jacobiY):F3}");
        }
    }
}