using System;

namespace PatternMatchingExamples
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public record Point(int X, int Y);

    public class Shape { }
    public class Circle : Shape { public double Radius { get; set; } }
    public class Rectangle : Shape { public double Width { get; set; } public double Height { get; set; } }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1. Простой паттерн по типу:");
            Console.WriteLine(GetObjectTypeInfo("строка"));
            Console.WriteLine(GetObjectTypeInfo(42));
            Console.WriteLine(GetObjectTypeInfo(true));
            Console.WriteLine(GetObjectTypeInfo(3.14));

            Console.WriteLine("\n2. Проверка на null:");
            Console.WriteLine(CheckNull(null));
            Console.WriteLine(CheckNull("не null"));

            Console.WriteLine("\n3. Паттерн свойств:");
            Console.WriteLine(GetPersonCategory(new Person { Age = 10 }));
            Console.WriteLine(GetPersonCategory(new Person { Age = 30 }));
            Console.WriteLine(GetPersonCategory(new Person { Age = 65 }));
            Console.WriteLine(GetPersonCategory(null));

            Console.WriteLine("\n4. Паттерн кортежей:");
            Console.WriteLine(GetQuadrantInfo((3, 4)));
            Console.WriteLine(GetQuadrantInfo((-2, -5)));
            Console.WriteLine(GetQuadrantInfo((-1, 2)));

            Console.WriteLine("\n5. Позиционный паттерн:");
            Console.WriteLine(GetPointPosition(new Point(0, 0)));
            Console.WriteLine(GetPointPosition(new Point(5, 0)));
            Console.WriteLine(GetPointPosition(new Point(0, 3)));
            Console.WriteLine(GetPointPosition(new Point(2, 2)));
            Console.WriteLine(GetPointPosition(new Point(-3, 4)));
            Console.WriteLine(GetPointPosition(new Point(-1, -1)));
            Console.WriteLine(GetPointPosition(new Point(3, -2)));

            Console.WriteLine("\n6. Паттерн с when:");
            Console.WriteLine(CheckNumber(4));
            Console.WriteLine(CheckNumber(7));
            Console.WriteLine(CheckNumber("текст"));

            Console.WriteLine("\n7. Паттерн с switch expression:");
            Console.WriteLine(GetTrafficLightAction("Red"));
            Console.WriteLine(GetTrafficLightAction("Yellow"));
            Console.WriteLine(GetTrafficLightAction("Green"));
            Console.WriteLine(GetTrafficLightAction("Blue"));

            Console.WriteLine("\n8. Паттерн с логическими операторами:");
            Console.WriteLine(GetNumberRange(5));
            Console.WriteLine(GetNumberRange(15));
            Console.WriteLine(GetNumberRange(0));
            Console.WriteLine(GetNumberRange(-3));
            Console.WriteLine(GetNumberRange(25));

            Console.WriteLine("\n9. Паттерн с рекурсивными шаблонами:");
            Console.WriteLine(CalculateArea(new Circle { Radius = 3 }));
            Console.WriteLine(CalculateArea(new Rectangle { Width = 4, Height = 5 }));

            Console.WriteLine("\n10. Паттерн в цикле и коллекциях:");
            object[] items = { 1, "two", 3.0, null, true, new Person() { Name = "Иван", Age = 30 } };
            foreach (var item in items)
            {
                string info = item switch
                {
                    string s => $"Это строка: {s}",
                    int i => $"Это число: {i}",
                    bool b => $"Это булево значение: {b}",
                    double d => $"Это дробное число: {d}",
                    Person p => $"Это человек: {p.Name} ({p.Age} лет)",
                    null => "Объект null",
                    _ => $"Неизвестный тип: {item?.GetType().Name}"
                };
                Console.WriteLine(info);
            }
        }

        static string GetObjectTypeInfo(object obj)
        {
            return obj switch
            {
                string s => $"Это строка: {s}",
                int i => $"Это число: {i}",
                bool b => $"Это булево значение: {b}",
                _ => "Неизвестный тип"
            };
        }

        static string CheckNull(object? obj)
        {
            return obj switch
            {
                null => "Объект null",
                _ => $"Не null: {obj.GetType().Name}"
            };
        }

        static string GetPersonCategory(Person? person)
        {
            return person switch
            {
                null => "Неизвестно",
                { Age: < 18 } => "Ребенок",
                { Age: >= 18 and < 60 } => "Взрослый",
                { Age: >= 60 } => "Пенсионер"
            };
        }

        static string GetQuadrantInfo((int x, int y) coords)
        {
            return coords switch
            {
                ( > 0, > 0) => "Обе координаты положительные",
                ( < 0, < 0) => "Обе координаты отрицательные",
                _ => "Координаты в разных квадрантах"
            };
        }

        static string GetPointPosition(Point point)
        {
            return point switch
            {
                (0, 0) => "Начало координат",
                (_, 0) => "На оси X",
                (0, _) => "На оси Y",
                ( > 0, > 0) => "В квадранте I",
                ( < 0, > 0) => "В квадранте II",
                ( < 0, < 0) => "В квадранте III",
                ( > 0, < 0) => "В квадранте IV"
            };
        }

        static string CheckNumber(object obj)
        {
            return obj switch
            {
                int i when i % 2 == 0 => $"Четное число: {i}",
                int i => $"Нечетное число: {i}",
                _ => "Не число"
            };
        }

        static string GetTrafficLightAction(string color) => color switch
        {
            "Red" => "Stop",
            "Yellow" => "Wait",
            "Green" => "Go",
            _ => "Unknown"
        };

        static string GetNumberRange(int number) => number switch
        {
            >= 1 and <= 10 => "От 1 до 10",
            >= 11 and <= 20 => "От 11 до 20",
            <= 0 => "0 или отрицательное",
            _ => "Больше 20"
        };

        static double CalculateArea(Shape shape)
        {
            return shape switch
            {
                Circle { Radius: var r } => Math.PI * r * r,
                Rectangle { Width: var w, Height: var h } => w * h,
                _ => throw new ArgumentException("Unknown shape type")
            };
        }
    }
}