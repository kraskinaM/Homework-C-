using System;
using System.Diagnostics;
using System.Collections.Generic;

class Program
{
    struct TestCase
    {
        public string Name;
        public double Weight;
        public bool IsUrgent;
        public double Expected;
    }

    static void Main()
    {
        List<TestCase> tests = new List<TestCase>
        {
            new TestCase {
                Name = "Обычная доставка, стандартный вес",
                Weight = 7, IsUrgent = false,
                Expected = 350 
            },
            new TestCase {
                Name = "Срочная доставка, минимальный вес",
                Weight = 2, IsUrgent = true,
                Expected = 450 
            },
            new TestCase {
                Name = "Доставка на границе тяжелого веса",
                Weight = 20, IsUrgent = false,
                Expected = 450 
            },

            new TestCase {
                Name = "Вес 25 без срочности, скидка применена",
                Weight = 25, IsUrgent = false,
                Expected = 475 
            },
            new TestCase {
                Name = "Вес 3, скидки нет",
                Weight = 3, IsUrgent = false,
                Expected = 300 
            },
            new TestCase {
                Name = "Вес 20 ровно, скидки нет",
                Weight = 20, IsUrgent = false,
                Expected = 450 
            },
            new TestCase {
                Name = "Вес 25 со срочностью, скидка считается от суммы со срочностью",
                Weight = 25, IsUrgent = true,
                Expected = 617.5 
            }
        };

        Stopwatch sw = Stopwatch.StartNew();
        int failed = 0;

        foreach (var test in tests)
        {
            double actual = CalcDelivery(test.Weight, test.IsUrgent);

            if (Math.Abs(actual - test.Expected) < 0.001)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[PASS] {test.Name}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[FAIL] {test.Name}: ожидалось {test.Expected}, пришло {actual}");
                failed++;
            }
        }

        sw.Stop();
        Console.ResetColor();
        Console.WriteLine($"Всего проверок: {tests.Count}. Ошибок: {failed}.");
        Console.WriteLine($"Время прогона: {sw.Elapsed.TotalSeconds:F4} сек.");
    }

    public static double CalcDelivery(double weight, bool isUrgent)
    {
        double cost = 250;
        cost += Math.Ceiling(weight / 5) * 50;

        if (isUrgent)
        {
            cost += 150;
        }

        if (weight > 20)
        {
            cost = cost * 0.95;
        }

        return cost;
    }
}