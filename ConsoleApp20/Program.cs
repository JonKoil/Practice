using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp20
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            //1
            
            Generic<int>
                intInstance = new
                Generic<int>(10);
            Console.WriteLine($"начальное значение: {intInstance.Value}");
            intInstance.Value = 20;
            Console.WriteLine($"новое значение: {intInstance.Value}");
            Generic<string>
                stringInstance = new
                Generic<string>("Дай мид гнида");
            Console.WriteLine($"начальное значение: {stringInstance.Value}");
            stringInstance.Value = "Спасибо за мид";
            Console.WriteLine($"новое значение: {stringInstance.Value}");
            Console.ReadKey();
            */

            /*
            //2
            
            int[] intArray = { 1, 2, 3, 4, 5 };
            int maxInt = FindMax(intArray);
            int minInt = FindMin(intArray);
            Console.WriteLine($"максимальное число: {maxInt}");
            Console.WriteLine($"минимальное число: {minInt}");
            string[] stringArray = { "iphone", "vivo", "asus", "samsung" };
            string maxString = FindMax(stringArray);
            Console.WriteLine($"Максимальная строка: {maxString}");
            string minString = FindMin(stringArray);
            Console.WriteLine($"минимальная строка: {minString}");
            Console.ReadKey();
            */
            /*
             //3
            var intArray1 = new GenericArray<int>(new int[] { 1, 2, 3 });
            var intArray2 = new GenericArray<int>(new int[] { 4, 5, 6 });
            var intResult = intArray1 + intArray2;
            Console.WriteLine(intResult);

            var strArray1 = new GenericArray<string>(new string[] { "a", "b", "c" });
            var strArray2 = new GenericArray<string>(new string[] { "d", "e" });
            var strResult = strArray1 + strArray2;
            Console.WriteLine(strResult);
            Console.ReadKey();
            */
            //4
            var intArray = new GenericArray<int>();
            intArray.Add(1);
            intArray.Add(2);
            intArray.Add(3);
            intArray.PrintAll();

            Console.WriteLine("\nЭлемент с индексом 1:" + intArray.Get(1));
            intArray.Clear(1);
            Console.WriteLine("\nПосле удаления с индексом 1");
                intArray.PrintAll();

            var stringArray = new GenericArray<string>();
            stringArray.Add("данил слил всё в казик(");
            stringArray.Add("данил поднял, но поять всё слил((");
            Console.WriteLine("строковый массив");
            stringArray.PrintAll();
            Console.ReadKey();
        }
        /*
         //1
        public class Generic<G>
        {
            private G _value;

            public G Value
            {
                get { return _value; }
                set { _value = value; }
            }
            public Generic(G initialValue)
            {
                _value = initialValue;
            }

        }
        */
        /*
        //2
        public static T FindMax<T>(T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length == 0)
                throw new ArgumentException("Массив не может быть null");

            T max = array[0];

            for (int i = 1; i < array.Length; i++)
            {
                if (array[i].CompareTo(max) > 0)
                {
                    max = array[i];
                }
            }

            return max;
        }
        public static T FindMin<T>(T[] array) where T : IComparable<T>
        {
            if (array == null || array.Length == 0)
                throw new ArgumentException("Массив не может быть null");

            T min = array[0];

            for (int i = 1; i > array.Length; i--)
            {
                if (array[i].CompareTo(min) > 0)
                {
                    min = array[i];
                }
            }

            return min;
        }
        */
        /*
         public class GenericArray<G>
         {
             private G[] array;

             public GenericArray(G[] array)
             {
                 this.array = array ?? throw new ArgumentNullException(nameof(array));
             }
             public G[] Array => array;

             public static GenericArray<G> operator +(GenericArray<G> left, GenericArray<G> right)
             {
                 if (left == null || right == null)
                 {
                     throw new ArgumentNullException("не могут быть 0");
                 }

                 int newLength = left.array.Length + right.array.Length;
                 G[] newArray = new G[newLength];
                 left.array.CopyTo(newArray, 0);
                 right.array.CopyTo(newArray, left.array.Length);

                 return new GenericArray<G>(newArray);
             }
             public override string ToString()
             {
                 return $"массив: <{typeof(G).Name}>: [{string.Join(",", array)}]";
             }
         }
         */

        public class GenericArray<T>
        {
            private T[] array;


            public GenericArray()
            {
                array = Array.Empty<T>();
            }
            public void Add(T item)
            {
                T[] newArray = new T[array.Length + 1];
                Array.Copy(array, newArray, array.Length);
                newArray[array.Length] = item;
                array = newArray;
            }
            public void Clear(int index)
            {
                if (index < 0 || index >= array.Length)
                {
                    throw new IndexOutOfRangeException("Индекс выходит за границы массива");
                }
                T[] newArray = new T[array.Length - 1];
                Array.Copy(array, 0, newArray, 0, index);
                Array.Copy(array, index + 1, newArray, index, array.Length - index - 1);
                array = newArray;
            }
            public T Get(int index)
            {
                if (index < 0 || index >= array.Length)
                {
                    throw new IndexOutOfRangeException("Индекс выходит за границы массива");
                }
                return array[index];
            }
            public int Length()
            {
                return array.Length;
            }

            public void PrintAll()
            {
                Console.WriteLine($"Массив ({typeof(T)}), длина: {Length()}");
                for (int i = 0; i < array.Length; i++)
                {
                    Console.WriteLine($"[{i}] = {array[i]}");
                }
            }
        }
    }
}

