using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp22
{
    #region J
    /*public class GenericClass<J>
    {
       
        private J _value;

        
        public J Value
        {
            get { return _value; }
            set { _value = value; }
        }

        
        public GenericClass(J initialValue)
        {
            _value = initialValue;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            
            GenericClass<int> intInstance = new GenericClass<int>(4);
            Console.WriteLine("Начальное значение: " + intInstance.Value);
            intInstance.Value = 44;
            Console.WriteLine("Новое значение: " + intInstance.Value);

           
            GenericClass<string> stringInstance = new GenericClass<string>("Kon'nichiwa");
            Console.WriteLine("Начальное значение: " + stringInstance.Value);
            stringInstance.Value = "Sekai";
            Console.WriteLine("Новое значение: " + stringInstance.Value);
        }
    }*/
    #endregion
    #region A
    /* public class Program
     {

         public static A FindMax<A>(I[] array) where A : IComparable<A>
         {
             if (array == null || array.Length == 0)
             {
                 throw new ArgumentException("Array cannot be null or empty.");
             }

             A max = array[0]; 


             for (int i = 1; i < array.Length; i++)
             {
                 if (array[i].CompareTo(max) > 0) 
                 {
                     max = array[i]; 
                 }
             }

             return max; 
         }

         static void Main(string[] args)
         {

             int[] intArray = { 3, 5, 7, 2, 8 };
             int maxInt = FindMax(intArray);
             Console.WriteLine("Maximum integer: " + maxInt);


             string[] stringArray = { "Toyota", "Mazda", "Nissan", "Subaru" };
             string maxString = FindMax(stringArray);
             Console.WriteLine("Maximum string: " + maxString);
         }
     }*/
    #endregion
    #region P
    /* public class GenericArray<T> where T : struct
     {

         private T[] _array;


         public GenericArray(T[] array)
         {
             _array = array;
         }


         public static GenericArray<T> operator +(GenericArray<T> a, GenericArray<T> b)
         {

             T[] combinedArray = new T[a._array.Length + b._array.Length];
             Array.Copy(a._array, combinedArray, a._array.Length);
             Array.Copy(b._array, 0, combinedArray, a._array.Length, b._array.Length);

             return new GenericArray<T>(combinedArray);
         }


         public void Print()
         {
             Console.WriteLine("Элемент массива: " + string.Join(", ", _array));
         }
     }

     class Program
     {
         static void Main(string[] args)
         {

             GenericArray<int> array1 = new GenericArray<int>(new int[] { 1, 2, 3 });
             GenericArray<int> array2 = new GenericArray<int>(new int[] { 4, 5, 6 });


             GenericArray<int> combinedArray = array1 + array2;


             combinedArray.Print();


             GenericArray<double> array3 = new GenericArray<double>(new double[] { 1.1, 2.2 });
             GenericArray<double> array4 = new GenericArray<double>(new double[] { 3.3, 4.4 });


             GenericArray<double> combinedArrayDouble = array3 + array4;


             combinedArrayDouble.Print();
         }
     }*/
    #endregion
    #region AN
   


public class GenericArray<AN>
    {
        
        private AN[] _array;

        
        public GenericArray()
        {
            _array = new AN[0]; 
        }
        public void Add(AN item)
        {
           
            AN[] newArray = new AN[_array.Length + 1];
            Array.Copy(_array, newArray, _array.Length);
            newArray[_array.Length] = item; 
            _array = newArray; 
        }
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _array.Length)
            {
                throw new ArgumentOutOfRangeException("Индекс вне диапазона.");
            }
            AN[] newArray = new AN[_array.Length - 1];
            for (int i = 0, j = 0; i < _array.Length; i++)
            {
                if (i != index) 
                {
                    newArray[j++] = _array[i];
                }
            }
            _array = newArray; 
        }

        
        public AN Get(int index)
        {
            if (index < 0 || index >= _array.Length)
            {
                throw new ArgumentOutOfRangeException("Индекс вне диапазона.");
            }
            return _array[index];
        }
        public int Length
        {
            get { return _array.Length; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            GenericArray<int> intArray = new GenericArray<int>();
            intArray.Add(1);
            intArray.Add(2);
            intArray.Add(3);
            Console.WriteLine("Длина массива: " + intArray.Length); 

            Console.WriteLine("Элемент по индексу 1: " + intArray.Get(1)); 

            intArray.RemoveAt(1); 
            Console.WriteLine("Длина массива после удаления: " + intArray.Length); 

            GenericArray<string> stringArray = new GenericArray<string>();
            stringArray.Add("Kon'nichiwa");
            stringArray.Add("Sekai");
            Console.WriteLine("Длина массива строк: " + stringArray.Length); 
            Console.WriteLine("Элемент по индексу 0: " + stringArray.Get(0)); 
        }
    }
    #endregion
}
