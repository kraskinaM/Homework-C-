using System;
using System.Linq;

namespace BlackBoxTestingPR28
{
    class Program
    {
        static int totalTests = 0;
        static int passedTests = 0;
        static int failedTests = 0;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("0.Проверка");
            RunWhiteBoxBaseTests();
            int baseCount = totalTests;
            Console.WriteLine($"Итого проверок было белый ящик: {baseCount}");
            Console.WriteLine("1.Первое: классы эквивалентности");
            RunEquivalenceClassesTests();
            Console.WriteLine($"Счёт после 1 проверки: было {baseCount} Стало {totalTests} (добавлено {totalTests - baseCount})");

            Console.WriteLine("2.Второе: границы и негатив");
            RunBoundaryAndNegativeTests();
            Console.WriteLine($"Счёт после 2 проверки: всего проверок = {totalTests} Успешно = {passedTests} Расхождений = {failedTests}");

            Console.WriteLine("3.Третье: Тестирование EstimateDays");
            RunEstimateDaysTests();
            Console.WriteLine($"Итог: всего = {totalTests} Зелёных = {passedTests} Красных = {failedTests}");
        }

        static void CheckInt(string testName, string inputDesc, int expected, int actual)
        {
            totalTests++;
            if (expected == actual)
            {
                passedTests++;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[ок] {testName} Вход: {inputDesc} = {actual}");
            }
            else
            {
                failedTests++;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[Расхождение] {testName} Вход: {inputDesc} Ожидалось: {expected} Пришло: {actual}");
            }
            Console.ResetColor();
        }

        static void CheckString(string testName, string inputDesc, string expected, Func<string> funcCall)
        {
            totalTests++;
            try
            {
                string actual = funcCall();
                if (expected == actual)
                {
                    passedTests++;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[ок] {testName} Вход: {inputDesc} = \"{actual}\"");
                }
                else
                {
                    failedTests++;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[Расхождение] {testName} Вход: {inputDesc} Ожидалось: \"{expected}\" Пришло: \"{actual}\"");
                }
            }
            catch (Exception ex)
            {
                failedTests++;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[Расхождение и падение] {testName} Вход: {inputDesc} Ожидалось: \"{expected}\" Пришло: Исключение {ex.GetType().Name}");
            }
            Console.ResetColor();
        }

        static void RunWhiteBoxBaseTests()
        {
            CheckInt("WB CalcDelivery базовая ветка", "(2, false)", 250, CalcDelivery(2, false));
            CheckInt("WB CalcDelivery ветка > 5", "(10, false)", 350, CalcDelivery(10, false));
            CheckInt("WB CalcDelivery ветка > 20 + срочн", "(30, true)", 1140, CalcDelivery(30, true));

            CheckString("WB Grade ветка 5", "90", "5", () => Grade(90));
            CheckString("WB Grade ветка 4", "75", "4", () => Grade(75));
            CheckString("WB Grade ветка 3", "55", "3", () => Grade(55));
            CheckString("WB Grade ветка 2", "30", "2", () => Grade(30));

            CheckString("WB CheckPassword валидный", "\"Pass1234\"", "Надёжный", () => CheckPassword("Pass1234"));
            CheckString("WB CheckPassword короткий", "\"Pa1\"", "Ненадёжный", () => CheckPassword("Pa1"));
        }

        static void RunEquivalenceClassesTests()
        {
            CheckInt("КЛ-01 CalcDelivery (вес <= 0)", "(-3, false)", -1, CalcDelivery(-3, false));
            CheckInt("КЛ-02 CalcDelivery (1-5 кг, обычн)", "(3, false)", 250, CalcDelivery(3, false));
            CheckInt("КЛ-03 CalcDelivery (1-5 кг, срочн)", "(3, true)", 500, CalcDelivery(3, true));
            CheckInt("КЛ-04 CalcDelivery (6-20 кг, обычн)", "(7, false)", 350, CalcDelivery(7, false));
            CheckInt("КЛ-05 CalcDelivery (6-20 кг, срочн)", "(7, true)", 700, CalcDelivery(7, true));
            CheckInt("КЛ-06 CalcDelivery (> 20 кг, обычн)", "(25, false)", 570, CalcDelivery(25, false));
            CheckInt("КЛ-07 CalcDelivery (> 20 кг, срочн)", "(25, true)", 1140, CalcDelivery(25, true));

            CheckString("КЛ-08 Grade (< 0)", "-10", "Ошибка", () => Grade(-10));
            CheckString("КЛ-09 Grade (0-49)", "25", "2", () => Grade(25));
            CheckString("КЛ-10 Grade (50-69)", "60", "3", () => Grade(60));
            CheckString("КЛ-11 Grade (70-84)", "75", "4", () => Grade(75));
            CheckString("КЛ-12 Grade (85-100)", "92", "5", () => Grade(92));
            CheckString("КЛ-13 Grade (> 100)", "120", "Ошибка", () => Grade(120));

            CheckString("КЛ-14 CheckPassword (< 8 симв)", "\"Ab1\"", "Ненадёжный", () => CheckPassword("Ab1"));
            CheckString("КЛ-15 CheckPassword (без цифры)", "\"Abcdefgh\"", "Ненадёжный", () => CheckPassword("Abcdefgh"));
            CheckString("КЛ-16 CheckPassword (без заглавной)", "\"abcdefg1\"", "Ненадёжный", () => CheckPassword("abcdefg1"));
            CheckString("КЛ-17 CheckPassword (валидный)", "\"Abcdefg1\"", "Надёжный", () => CheckPassword("Abcdefg1"));
        }

        static void RunBoundaryAndNegativeTests()
        {
            CheckInt("ГР-01 CalcDelivery граница 0 (-1)", "(-1, false)", -1, CalcDelivery(-1, false));
            CheckInt("ГР-02 CalcDelivery граница 0 (0)", "(0, false)", -1, CalcDelivery(0, false));
            CheckInt("ГР-03 CalcDelivery граница 0 (+1)", "(1, false)", 250, CalcDelivery(1, false));

            CheckInt("ГР-04 CalcDelivery граница 5 (-1)", "(4, false)", 250, CalcDelivery(4, false));
            CheckInt("ГР-05 CalcDelivery граница 5 (0)", "(5, false)", 250, CalcDelivery(5, false));
            CheckInt("ГР-06 CalcDelivery граница 5 (+1)", "(6, false)", 350, CalcDelivery(6, false));

            CheckInt("ГР-07 CalcDelivery граница 20 (-1)", "(19, false)", 350, CalcDelivery(19, false));
            CheckInt("ГР-08 CalcDelivery граница 20 (0)", "(20, false)", 350, CalcDelivery(20, false));
            CheckInt("ГР-09 CalcDelivery граница 20 (+1)", "(21, false)", 570, CalcDelivery(21, false));

            CheckString("ГР-10 Grade граница 0 (-1)", "-1", "Ошибка", () => Grade(-1));
            CheckString("ГР-11 Grade граница 0 (0)", "0", "2", () => Grade(0));
            CheckString("ГР-12 Grade граница 0 (+1)", "1", "2", () => Grade(1));

            CheckString("ГР-13 Grade граница 50 (-1)", "49", "2", () => Grade(49));
            CheckString("ГР-14 Grade граница 50 (0)", "50", "3", () => Grade(50));
            CheckString("ГР-15 Grade граница 50 (+1)", "51", "3", () => Grade(51));

            CheckString("ГР-16 Grade граница 70 (-1)", "69", "3", () => Grade(69));
            CheckString("ГР-17 Grade граница 70 (0)", "70", "4", () => Grade(70));
            CheckString("ГР-18 Grade граница 70 (+1)", "71", "4", () => Grade(71));

            CheckString("ГР-19 Grade граница 85 (-1)", "84", "4", () => Grade(84));
            CheckString("ГР-20 Grade граница 85 (0)", "85", "5", () => Grade(85));
            CheckString("ГР-21 Grade граница 85 (+1)", "86", "5", () => Grade(86));

            CheckString("ГР-22 Grade граница 100 (-1)", "99", "5", () => Grade(99));
            CheckString("ГР-23 Grade граница 100 (0)", "100", "5", () => Grade(100));
            CheckString("ГР-24 Grade граница 100 (+1)", "101", "Ошибка", () => Grade(101));

            CheckString("ГР-25 CheckPassword длина 7", "\"Abcdef1\"", "Ненадёжный", () => CheckPassword("Abcdef1"));
            CheckString("ГР-26 CheckPassword длина 8", "\"Abcdef12\"", "Надёжный", () => CheckPassword("Abcdef12"));
            CheckString("ГР-27 CheckPassword длина 9", "\"Abcdef123\"", "Надёжный", () => CheckPassword("Abcdef123"));

            CheckString("ГР-28 CheckPassword заглавная 1-я", "\"Abcdef12\"", "Надёжный", () => CheckPassword("Abcdef12"));
            CheckString("ГР-29 CheckPassword заглавная в центре", "\"abcdEf12\"", "Надёжный", () => CheckPassword("abcdEf12"));
            CheckString("ГР-30 CheckPassword заглавная последняя", "abcdef1A", "Надёжный", () => CheckPassword("abcdef1A"));
            CheckString("НЕГ-01 CheckPassword пустая строка", "", "Ненадёжный", () => CheckPassword(""));
            CheckString("НЕГ-02 CheckPassword null", "null", "Ненадёжный", () => CheckPassword(null));
        }
        static void RunEstimateDaysTests()
        {
            CheckInt("ED-01 негатив < 0", "(-5, false)", -1, EstimateDays(-5, false));
            CheckInt("ED-02 граница -1 срочн", "(-1, true)", -1, EstimateDays(-1, true));
            CheckInt("ED-03 граница 0 обычн", "(0, false)", 1, EstimateDays(0, false));
            CheckInt("ED-04 середина 0-100 срочн", "(50, true)", 1, EstimateDays(50, true));
            CheckInt("ED-05 граница 100 обычн", "(100, false)", 1, EstimateDays(100, false));
            CheckInt("ED-06 граница 100 срочн", "(100, true)", 1, EstimateDays(100, true));
            CheckInt("ED-07 граница 101 обычн", "(101, false)", 2, EstimateDays(101, false));
            CheckInt("ED-08 граница 101 срочн", "(101, true)", 1, EstimateDays(101, true));
            CheckInt("ED-09 граница 500 обычн", "(500, false)", 2, EstimateDays(500, false));
            CheckInt("ED-10 граница 501 обычн", "(501, false)", 4, EstimateDays(501, false));
            CheckInt("ED-11 середина 501-2000 срочн", "(1000, true)", 2, EstimateDays(1000, true));
            CheckInt("ED-12 граница 2000 обычн", "(2000, false)", 4, EstimateDays(2000, false));
            CheckInt("ED-13 граница 2001 обычн", "(2001, false)", 7, EstimateDays(2001, false));
            CheckInt("ED-14 граница 2001 срочн", "(2001, true)", 4, EstimateDays(2001, true));
        }
        static int CalcDelivery(int weightKg, bool express)
        {
            if (weightKg <= 0) return -1;
            int price = 250;
            if (weightKg > 5) price += 100;
            if (weightKg > 20)
            {
                price += 250;
                price = (int)(price * 0.95);
            }
            if (express) price *= 2;
            return price;
        }
        static string Grade(int score)
        {
            if (score < 0 || score > 100) return "Ошибка";
            if (score >= 85) return "5";
            if (score >= 70) return "4";
            if (score >= 50) return "3";
            return "2";
        }
        static string CheckPassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8)
                return "Ненадёжный";
            bool hasDigit = password.Any(char.IsDigit);
            bool hasUpper = password.Any(char.IsUpper);
            return (hasDigit && hasUpper) ? "Надёжный" : "Ненадёжный";
        }
        static int EstimateDays(int distanceKm, bool express)
        {
            int days;
            if (distanceKm < 100) days = 1;
            else if (distanceKm <= 500) days = 2;
            else if (distanceKm < 2000) days = 4;
            else days = 7;
            if (express) days = days / 2;
            return days;
        }
    }
}