﻿using System;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        
        string[] dNames = { "вес 3, обычная", "вес 5, обычная", "вес 6, обычная", "вес 20, обычная", "вес 21, обычная", "вес 7, срочная", "вес 25, срочная", "вес 0, обычная", "вес -5, обычная" };
        int[] dWeights = { 3, 5, 6, 20, 21, 7, 25, 0, -5 };
        bool[] dExpress = { false, false, false, false, false, true, true, false, false };
        int[] dExpect = { 200, 200, 300, 300, 550, 600, 1100, -1, -1 };

        Console.WriteLine("Расчет доставки");
        for (int i = 0; i < dNames.Length; i++)
        {
            try { Check(dNames[i], CalcDelivery(dWeights[i], dExpress[i]), dExpect[i]); }
            catch (Exception e) { Console.WriteLine(dNames[i] + ": ИСКЛЮЧЕНИЕ, " + e.Message); }
        }
        
        string[] pNames = { "Abc12345", "Abc1234", "Abcd1234", "Abcdefgh", "abcdefg1", "abcdEfg1", "пустая строка" };
        string[] pInputs = { "Abc12345", "Abc1234", "Abcd1234", "Abcdefgh", "abcdefg1", "abcdEfg1", "" };
        string[] pExpect = { "OK", "Короткий пароль", "OK", "Нет цифры", "Нет заглавной буквы", "OK", "Короткий пароль" };

        Console.WriteLine("Проверить пароль");
        for (int i = 0; i < pNames.Length; i++)
        {
            try { CheckStr(pNames[i], CheckPassword(pInputs[i]), pExpect[i]); }
            catch (Exception e) { Console.WriteLine(pNames[i] + ": ИСКЛЮЧЕНИЕ, " + e.Message); }
        }
        
        string[] gNames = { "100", "85", "84", "70", "69", "50", "49", "0", "-1", "101" };
        int[]    gScore = { 100, 85, 84, 70, 69, 50, 49, 0, -1, 101 };
        string[] gExpect = { "5", "5", "4", "4", "3", "3", "2", "2", "Ошибка", "Ошибка" };

        Console.WriteLine("Оценка");
        for (int i = 0; i < gNames.Length; i++)
            CheckStr("оценка" + gNames[i], Grade(gScore[i]), gExpect[i]);
    }

    static void Check(string name, int got, int expected)
    {
        if (got == expected) Console.WriteLine(name + ": OK");
        else Console.WriteLine(name + ": РАСХОЖДЕНИЕ, ожидалось " + expected + ", получено " + got);
    }

    static void CheckStr(string name, string got, string expected)
    {
        if (got == expected) Console.WriteLine(name + ": OK");
        else Console.WriteLine(name + ": РАСХОЖДЕНИЕ, ожидалось «" + expected + "», получено «" + got + "»");
    }
    
    static int CalcDelivery(int weightKg, bool express)
    {
        int price = 200;
        if (weightKg > 5) price = price + 100;
        if (weightKg > 20) price = price + 250;
        if (express) price = price * 2;
        return price;
    }

    static string CheckPassword(string password)
    {
        if (password.Length < 8) return "Короткий пароль";
        bool hasDigit = false;
        for (int i = 0; i < password.Length; i++)
            if (char.IsDigit(password[i])) hasDigit = true;
        if (!hasDigit) return "Нет цифры";
        if (char.IsUpper(password[0])) return "OK";
        return "Нет заглавной буквы";
    }

    static string Grade(int score)
    {
        if (score < 0 || score > 100) return "Ошибка";
        if (score > 85) return "5";
        if (score >= 70) return "4";
        if (score >= 50) return "3";
        return "2";
    }
}