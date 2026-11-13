using System;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // 1. Формулы для уравнения
        double F(double x) => x * x * x + x - 1.0;
        double dF(double x) => 3.0 * x * x + 1.0;
        double eps = 1e-6;

        //Прогон хорды и касательных
        Console.WriteLine("Прогон");
        var c = Chord(F, 0.5, 1.0, eps);
        var newt = Newton(F, dF, 1.0, eps);
        Console.WriteLine($"Хорды: x = {c.x:F6}, шагов = {c.n}");
        Console.WriteLine($"Касательные: x = {newt.x:F6}, шагов = {newt.n}");

        //Комбинированный метод
        Console.WriteLine("Комбинирование");
        var comb = Combined(F, dF, 0.5, 1.0, eps);
        double root = (comb.a + comb.b) / 2.0;
        Console.WriteLine($"Итоговый корень: {root:F6}, всего шагов: {comb.n}");

        //Сводная таблица
        Console.WriteLine("Таблица");
        double Phi(double x) => Math.Cbrt(1.0 - x);
        var bis = Bisection(F, 0.5, 1.0, eps);
        var iter = SimpleIteration(Phi, 0.75, eps);

        Console.WriteLine($"{"Метод",-20} {"Корень",-12} {"Шагов",-6}");
        Console.WriteLine($"{"Бисекция",-20} {bis.x,-12:F6} {bis.n,-6}");
        Console.WriteLine($"{"Простые итерации",-20} {iter.x,-12:F6} {iter.n,-6}");
        Console.WriteLine($"{"Хорды",-20} {c.x,-12:F6} {c.n,-6}");
        Console.WriteLine($"{"Касательные",-20} {newt.x,-12:F6} {newt.n,-6}");
        Console.WriteLine($"{"Комбинированный",-20} {root,-12:F6} {comb.n,-6}");
 
        //Цикл Ньютона
        Console.WriteLine("Цикл");
        double F2(double x) => x * x * x - 2.0 * x + 2.0;
        double dF2(double x) => 3.0 * x * x - 2.0;
        NewtonBroken(F2, dF2, 0.0, 10);
    }

    //Вычеслительные методы
    static (double x, int n) Chord(Func<double, double> f, double a, double b, double eps)
    {
        double x = a;
        int n = 0;
        while (true)
        {
            double next = x - f(x) * (b - x) / (f(b) - f(x));
            n++;
            if (Math.Abs(next - x) < eps) return (next, n);
            x = next;
        }
    }

    static (double x, int n) Newton(Func<double, double> f, Func<double, double> df, double x0, double eps)
    {
        double x = x0;
        int n = 0;
        while (true)
        {
            double next = x - f(x) / df(x);
            n++;
            if (Math.Abs(next - x) < eps) return (next, n);
            x = next;
        }
    }

    static (double a, double b, int n) Combined(Func<double, double> f, Func<double, double> df, double a, double b, double eps)
    {
        int n = 0;
        while ((b - a) / 2 > eps)
        {
            a = a - f(a) * (b - a) / (f(b) - f(a));
            b = b - f(b) / df(b);
            n++;

            double mid = (a + b) / 2.0;
            Console.WriteLine($"Шаг {n}: a = {a:F6}, b = {b:F6}, середина = {mid:F6}");
        }
        return (a, b, n);
    }

    static (double x, int n) Bisection(Func<double, double> f, double a, double b, double eps)
    {
        int n = 0;
        while ((b - a) / 2 > eps)
        {
            double c = (a + b) / 2;
            if (f(a) * f(c) <= 0) b = c;
            else a = c;
            n++;
        }
        return ((a + b) / 2, n);
    }

    static (double x, int n) SimpleIteration(Func<double, double> phi, double x0, double eps)
    {
        double x = x0;
        int n = 0;
        while (true)
        {
            double next = phi(x);
            n++;
            if (Math.Abs(next - x) < eps) return (next, n);
            x = next;
        }
    }

    static void NewtonBroken(Func<double, double> f, Func<double, double> df, double x0, int limit)
    {
        double x = x0;
        Console.WriteLine($"Старт: x0 = {x:F6}");
        for (int n = 1; n <= limit; n++)
        {
            double next = x - f(x) / df(x);
            Console.WriteLine($"Шаг {n,2}: x = {next:F6}");
            x = next;
        }
    }
}