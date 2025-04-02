using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp21
{
        internal class Program
        {
            public class Rectangle
            {
                public double X;
                public double Y;
                public double Z;
                public double W;


                public Rectangle()
                {
                    double X = 0;
                    double Y = 0;
                    double Z = 0;
                    double W = 0;
                }


                public Rectangle(double x, double y, double z, double w)
                {
                    this.X = x;
                    this.Y = y;
                    this.Z = z;
                    this.W = w;
                }

                public double Perimetr()
                {
                    double width = Math.Abs(Z - X);
                    double height = Math.Abs(W - Y);
                    return 2 * (width + height);
                }
                public double Area()
                {
                    double width = Z - X;
                    double height = W - Y;
                    return width * height;
                }
            }
            public class Programm
            {
                public static void Main()
                {

                    Console.WriteLine("Введтие координату x1");
                    double x = Convert.ToDouble(Console.ReadLine());



                    Console.WriteLine("Введтие координату x2");
                    double y = Convert.ToDouble(Console.ReadLine());

                    Console.WriteLine("Введтие координату y1");
                    double z = Convert.ToDouble(Console.ReadLine());

                    Console.WriteLine("Введтие координату y2");
                    double w = Convert.ToDouble(Console.ReadLine());

                    Rectangle user = new Rectangle(x, y, z, w);
                    Rectangle rectangle = new Rectangle(5, 5, 1, 1);


                    double perimetr = user.Perimetr();
                    double area = user.Area();

                    double nperimetr = rectangle.Perimetr();
                    double narea = rectangle.Area();

                    Console.Write("\nПрямоугольник с координатами введёнными пользователем\n\n");

                    Console.WriteLine($"Периметр = {perimetr}\n");

                    Console.WriteLine($"Площадь = {area}\n\n");

                    Console.WriteLine("Прямоугольник с координатами введёнными программой\n");

                    Console.WriteLine($"Пертмеир = {nperimetr}\n");

                    Console.WriteLine($"Площадь = {narea}");

                    Console.ReadLine();


                }
            }
        }
    }
