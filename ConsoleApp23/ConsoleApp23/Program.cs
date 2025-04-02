using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp23
{
    #region M
    /*class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Введите значение A (целое число):");
                int A = int.Parse(Console.ReadLine());

                Console.WriteLine("Введите значение B (целое число):");
                int B = int.Parse(Console.ReadLine());

                
                if (A == 0 && B == 0)
                {
                    Console.WriteLine("Уравнение имеет бесконечно много решений, так как 0x = 0.");
                }
                else if (A == 0)
                {
                    Console.WriteLine("Уравнение не имеет решений, так как A = 0 и B ≠ 0.");
                }
                else if (B % A == 0)
                {
                    int x = B / A; 
                    Console.WriteLine($"Решение уравнения {A}x = {B}: x = {x}");
                }
                else
                {
                    Console.WriteLine("Уравнение не имеет целочисленного решения, так как B не делится на A без остатка.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Ошибка: Введите корректные целые числа.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Ошибка: Введенное число слишком велико или слишком мало.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }
    }*/
    #endregion
    #region G
    /*class Program
     {
         static void CheckNumber(int number)
         {
             if (number % 2 == 0) 
             {
                 throw new ArithmeticException("Четное число.");
             }
             else 
             {
                 throw new OverflowException("Нечетное число.");
             }
         }

         static void Main(string[] args)
         {
             while (true)
             {
                 Console.WriteLine("Введите целое число (или любой другой символ для выхода):");

                 string input = Console.ReadLine();


                 if (!int.TryParse(input, out int number))
                 {
                     Console.WriteLine("Выход из программы.");
                     break; 
                 }

                 try
                 {
                     CheckNumber(number); 
                 }
                 catch (OverflowException ex)
                 {
                     Console.WriteLine($"Ошибка: {ex.Message}"); 
                 }
                 catch (ArithmeticException ex)
                 {
                     Console.WriteLine($"Ошибка: {ex.Message}"); 
                 }
             }
         }
     }*/
    #endregion
    #region L
    public class CustomException : Exception
    {
        
        public char[] CharArray { get; }

        
        public CustomException(int size) : base("Произошла ошибка пользовательского типа.")
        {
            CharArray = new char[size];
            FillCharArray(size);
        }

        
        private void FillCharArray(int size)
        {
            for (int i = 0; i < size; i++)
            {
                CharArray[i] = (char)('A' + i); 
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                
                CustomException customException = new CustomException(26);

                
                throw customException;
            }
            catch (CustomException ex)
            {
                
                Console.WriteLine("Содержимое символьного массива:");
                Console.WriteLine(string.Join(", ", ex.CharArray));
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }
    }
    #endregion
}