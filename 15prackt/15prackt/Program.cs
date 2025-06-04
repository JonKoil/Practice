using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15prackt
{
        class Program
        {
            #region 1. Вычисление гипотенузы
            public static double CalculateHypotenuse(double a, double b)
            {
                return Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
            }
            #endregion

            #region 2. Округление чисел
            public static void RoundNumber(double number, out double floor, out double ceiling)
            {
                floor = Math.Floor(number);
                ceiling = Math.Ceiling(number);
            }
            #endregion

            #region 3. Тригонометрические функции
            public static void CalculateTrigonometricFunctions(double angleDegrees, out double sin, out double cos, out double tan)
            {
                double angleRadians = angleDegrees * Math.PI / 180;
                sin = Math.Sin(angleRadians);
                cos = Math.Cos(angleRadians);
                tan = Math.Tan(angleRadians);
            }
            #endregion

            #region 4. Генератор случайных чисел
            public static int[] GenerateRandomNumbers(int count, int min, int max)
            {
                Random random = new Random();
                int[] numbers = new int[count];
                for (int i = 0; i < count; i++)
                {
                    numbers[i] = random.Next(min, max + 1);
                }
                return numbers;
            }
            #endregion

            #region 5. Минимум и максимум
            public static void FindMinMax(double[] numbers, out double min, out double max)
            {
                if (numbers == null || numbers.Length == 0)
                {
                    throw new ArgumentException("Массив не может быть пустым");
                }

                min = numbers[0];
                max = numbers[0];

                foreach (double num in numbers)
                {
                    min = Math.Min(min, num);
                    max = Math.Max(max, num);
                }
            }
            #endregion

            #region 6. Конвертация строки в число
            public static void ConvertStringToInt()
            {
                Console.Write("Введите число: ");
                string input = Console.ReadLine();

                try
                {
                    int number = Convert.ToInt32(input);
                    Console.WriteLine($"Успешно преобразовано в int: {number}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка: введенная строка не является числом.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Ошибка: число слишком большое или слишком маленькое для int.");
                }
            }
            #endregion

            #region 7. Преобразование числа в двоичную строку
            public static string ConvertToBinary(int number)
            {
                return Convert.ToString(number, 2);
            }
            #endregion

            #region 8. Конвертация даты
            public static DateTime ConvertStringToDateTime(string dateString)
            {
                try
                {
                    return Convert.ToDateTime(dateString);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка: строка не является допустимой датой.");
                    return DateTime.MinValue;
                }
            }
            #endregion

            #region 9. Преобразование object в double
            public static double ConvertObjectToDouble(object obj)
            {
                try
                {
                    return Convert.ToDouble(obj);
                }
                catch (InvalidCastException)
                {
                    Console.WriteLine("Ошибка: объект не может быть преобразован в double.");
                    return 0.0;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка: формат объекта не подходит для преобразования в double.");
                    return 0.0;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Ошибка: значение слишком большое или слишком маленькое для double.");
                    return 0.0;
                }
            }
            #endregion

            #region 10. Преобразование строк в bool
            public static bool ConvertStringToBool(string value)
            {
                try
                {
                    return Convert.ToBoolean(value);
                }
                catch (FormatException)
                {
                    if (value == "1") return true;
                    if (value == "0") return false;

                    Console.WriteLine("Ошибка: строка должна быть 'True', 'False', '1' или '0'.");
                    return false;
                }
            }
            #endregion

            static void Main(string[] args)
            {
                Console.WriteLine("1. Вычисление гипотенузы треугольника с катетами 3 и 4: " + CalculateHypotenuse(3, 4));

                RoundNumber(3.7, out double floor, out double ceiling);
                Console.WriteLine("\n2. Округление числа 3.7: вниз = {floor}, вверх = {ceiling}");

                CalculateTrigonometricFunctions(45, out double sin, out double cos, out double tan);
                Console.WriteLine("\n3. Для угла 45°: sin = {sin:F4}, cos = {cos:F4}, tan = {tan:F4}");

                Console.WriteLine("\n4. Случайные числа от 1 до 100:");
                foreach (int num in GenerateRandomNumbers(10, 1, 100))
                {
                    Console.Write(num + " ");
                }

                double[] numbers = { 2.5, 1.7, 3.9, 4.1, 0.5 };
                FindMinMax(numbers, out double min, out double max);
                Console.WriteLine("\n\n5. Минимальное значение: {min}, Максимальное значение: {max}");

                Console.WriteLine("\n6. Конвертация строки в число:");
                ConvertStringToInt();

                Console.WriteLine("\n7. Преобразование числа 10 в двоичное: " + ConvertToBinary(10));
                Console.WriteLine("Преобразование числа 255 в двоичное: " + ConvertToBinary(255));

                Console.WriteLine("\n8. Конвертация строки '2025-05-26' в DateTime: " + ConvertStringToDateTime("2025-05-26"));

                Console.WriteLine("\n9. Преобразование object в double:");
                Console.WriteLine("Строка '3.14': " + ConvertObjectToDouble("3.14"));
                Console.WriteLine("Число 42: " + ConvertObjectToDouble(42));

                Console.WriteLine("\n10. Преобразование строк в bool:");
                Console.WriteLine("'True': " + ConvertStringToBool("True"));
                Console.WriteLine("'False': " + ConvertStringToBool("False"));
                Console.WriteLine("'1': " + ConvertStringToBool("1"));
                Console.WriteLine("'0': " + ConvertStringToBool("0"));

                Console.WriteLine("\nНажмите любую клавишу для выхода...");
                Console.ReadKey();
            }
        }
    }