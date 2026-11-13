using System;

class Program
{
    //Тест-драйв функции CalcDelivery

    static int CalcDelivery(int weight, bool urgent)
    {
        if (weight <= 0) return -1;

        int price;
        if (weight <= 5) price = 250;
        else if (weight <= 20) price = 350;
        else price = 570;

        if (urgent) price = price * 2;
        return price;
    }

    static string CheckPassword(string password)
    {
        if (password.Length < 8) return "Короткий пароль";

        bool hasDigit = false;
        for (int i = 0; i < password.Length; i++)
            if (char.IsDigit(password[i])) { hasDigit = true; break; }
        if (!hasDigit) return "Нет цифры";

        bool hasUpper = false;
        for (int i = 0; i < password.Length; i++)
            if (char.IsUpper(password[i])) { hasUpper = true; break; }
        if (!hasUpper) return "Нет заглавной буквы";

        return "Ок";
    }

    static string Grade(int score)
    {
        if (score < 0 || score > 100) return "Ошибка";
        if (score < 50) return "2";
        if (score < 70) return "3";
        if (score < 85) return "4";
        return "5";
    }

    //Тест раннер

    static int passed = 0;
    static int total = 0;

    static void TestInt(string name, int got, int expected)
    {
        total++;
        if (got == expected)
        {
            passed++;
            Console.WriteLine("Oк" + name);
        }
        else
            Console.WriteLine("Провал" + name + ":Ожидалось" + expected + ",Получено" + got);
    }

    static void TestStr(string name, string got, string expected)
    {
        total++;
        if (got == expected)
        {
            passed++;
            Console.WriteLine("Oк" + name);
        }
        else
            Console.WriteLine("Провал" + name + ":Ожидалось" + expected + "получено" + got);
    }

    //Пакет тестов

    static void RunAllTests()
    {
        //CalcDelivery 7 кейсов
        TestInt("CalcDelivery(3, false)", CalcDelivery(3, false), 250);
        TestInt("CalcDelivery(3, true)", CalcDelivery(3, true), 500);
        TestInt("CalcDelivery(7, false)", CalcDelivery(7, false), 350);
        TestInt("CalcDelivery(7, true)", CalcDelivery(7, true), 700);
        TestInt("CalcDelivery(25, false)", CalcDelivery(25, false), 570);
        TestInt("CalcDelivery(25, true)", CalcDelivery(25, true), 1140);
        TestInt("CalcDelivery(-3, false)", CalcDelivery(-3, false), -1);

        //CheckPassword 4 кейса
        TestStr("CheckPassword(\"a1b2c\")", CheckPassword("a1b2c"), "Короткий пароль");
        TestStr("CheckPassword(\"abcdefgh\")", CheckPassword("abcdefgh"), "Нет цифры");
        TestStr("CheckPassword(\"a1b2c3d4\")", CheckPassword("a1b2c3d4"), "Нет заглавной буквы");
        TestStr("CheckPassword(\"a1b2cD3f\")", CheckPassword("a1b2cD3f"), "Oк");

        //Grade 6 кейсов
        TestStr("Grade(-5)", Grade(-5), "Ошибка");
        TestStr("Grade(30)", Grade(30), "2");
        TestStr("Grade(60)", Grade(60), "3");
        TestStr("Grade(75)", Grade(75), "4");
        TestStr("Grade(90)", Grade(90), "5");
        TestStr("Grade(85)", Grade(85), "5");

        //Граничные тесты
        TestInt("CalcDelivery(0, false)", CalcDelivery(0, false), -1);
        TestInt("CalcDelivery(5, false)", CalcDelivery(5, false), 250);
        TestInt("CalcDelivery(6, false)", CalcDelivery(6, false), 350);
        TestInt("CalcDelivery(20, false)", CalcDelivery(20, false), 350);
        TestInt("CalcDelivery(21, false)", CalcDelivery(21, false), 570);

        TestStr("Grade(49)", Grade(49), "2");
        TestStr("Grade(50)", Grade(50), "3");
        TestStr("Grade(69)", Grade(69), "3");
        TestStr("Grade(70)", Grade(70), "4");
        TestStr("Grade(84)", Grade(84), "4");
        TestStr("Grade(85)", Grade(85), "5");
        TestStr("Grade(100)", Grade(100), "5");
        TestStr("Grade(101)", Grade(101), "Ошибка");

        TestStr("CheckPassword(\"a1b2c3d\")", CheckPassword("a1b2c3d"), "Короткий пароль");
        TestStr("CheckPassword(\"a1b2c3d4\")", CheckPassword("a1b2c3d4"), "Нет заглавной буквы");
    }

    static void Main()
    {
        RunAllTests();
        Console.WriteLine("Итог:" + passed + " из " + total + " тестов пройдено");
    }
}