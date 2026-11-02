using System;

namespace ErrorExperiment
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            Console.WriteLine("Расчет погрешностей для a и b");
            
            double a = 12.6, deltaA = 0.05;
            double b = 3.4, deltaB = 0.1;
            
            double sumVal = a + b;
            double sumDelta = deltaA + deltaB;
            double sumRel = (sumDelta / sumVal) * 100;
            Console.WriteLine($"Сумма: {sumVal:F4} ± {sumDelta:F4} (отн: {sumRel:F2}%)");
            
            double diffVal = a - b;
            double diffDelta = deltaA + deltaB;
            double diffRel = (diffDelta / diffVal) * 100;
            Console.WriteLine($"Разность: {diffVal:F4} ± {diffDelta:F4} (отн: {diffRel:F2}%)");
            
            double prodVal = a * b;
            double prodDelta = a * deltaB + b * deltaA;
            double prodRel = (prodDelta / prodVal) * 100;
            Console.WriteLine($"Произведение:{prodVal:F4} ± {prodDelta:F4} (отн: {prodRel:F2}%)");
            
            double divVal = a / b;
            double divRel = ((deltaA / a) + (deltaB / b)) * 100;
            double divDelta = divVal * (divRel / 100);
            Console.WriteLine($"Частное: {divVal:F4} ± {divDelta:F4} (отн: {divRel:F2}%)");
            
            Console.WriteLine("Вычитание близких чисел");
            
            double x = 4.321;
            double y = 4.319;
            double d = x - y;
            double exactD = 0.002;

            Console.WriteLine($"Результат double d = x - y: {d:G17}");
            Console.WriteLine($"Точный ручной ответ: {exactD}");
            Console.WriteLine($"Разница (мусор в конце): {d - exactD:G17}");
            
            Console.WriteLine("Циклическое суммирование 0.1");
            
            SumLoops(10, 1.0);
            SumLoops(100, 10.0);
            SumLoops(1_000_000, 100_000.0);

            Console.WriteLine("Сравнение 0.1 + 0.2 и 0.3");
            
            double d1 = 0.1, d2 = 0.2, d3 = 0.3;
            Console.WriteLine($"double через '==': {((d1 + d2) == d3)}");
            Console.WriteLine($"double и эпсилон: {(Math.Abs((d1 + d2) - d3) < 1e-9)}");

            decimal m1 = 0.1m, m2 = 0.2m, m3 = 0.3m;
            Console.WriteLine($"decimal через '==': {(m1 + m2 == m3)}");
            
            Console.WriteLine("Корни уравнения x2 - 50000x + 1 = 0");
            
            double A = 1;
            double B = -50000;
            double C = 1;

            double D = B * B - 4 * A * C;
            
            double x1 = (-B + Math.Sqrt(D)) / (2 * A);
            double x2_direct = (-B - Math.Sqrt(D)) / (2 * A);

            double x2_vieta = C / x1;

            Console.WriteLine($"Больший корень x1: {x1:G17}");
            Console.WriteLine($"Меньший корень x2 (дискриминант): {x2_direct:G17}");
            Console.WriteLine($"Меньший корень x2 (Виет): {x2_vieta:G17}");
        }

        static void SumLoops(int iterations, double expected)
        {
            double sum = 0;
            for (int i = 0; i < iterations; i++)
            {
                sum += 0.1;
            }
            double error = sum - expected;
            Console.WriteLine($"Итераций: {iterations,-7} Получено: {sum,-12:G12} Ошибка: {error:G17}");
        }
    }
}