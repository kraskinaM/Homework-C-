using System;
using System.Diagnostics;
using System.Text;

class Program
{
    static void Main()
    {
        int lines = 10000; 

        Console.WriteLine($"Запуск замера для {lines} строк...");
        
        var sw1 = Stopwatch.StartNew();
        string report = "";
        for (int i = 0; i < lines; i++)
        {
            report = report + "строка " + i + ": заказ обработан" + Environment.NewLine;
        }
        sw1.Stop();
        Console.WriteLine("1. Склейка строкой (+): " + sw1.ElapsedMilliseconds + " мс");
        
        var sw2 = Stopwatch.StartNew();
        var sb = new StringBuilder();
        for (int i = 0; i < lines; i++)
        {
            sb.AppendLine("строка " + i + ": заказ обработан");
        }
        string report2 = sb.ToString();
        sw2.Stop();
        Console.WriteLine("2. StringBuilder:" + sw2.ElapsedMilliseconds + " мс");
        
    }
}