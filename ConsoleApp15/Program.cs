


// Ignore Spelling: Fullstupor

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student student = new Student("Неконенко", "Макар", "Юрьевич", "12.03.2006", "Пролитарская 107", "9183774168", "animeshakek23@gmail.ru", "1", "ИП11(СО)", "843297382");
            student.DisplayInfo();
            Student student1 = new Student();
            student1.DisplayInfo();

            Console.WriteLine("Нажмите любую кнопку чтобы изменить данные");
            Console.ReadLine();

            student.GetFullPrint1();

            Console.Write($"Введите адрес:");
            student.Address = Console.ReadLine();

            Console.Write("Введите номер телефона:");
            student.Telephone = Console.ReadLine();

            Console.Write("Введите электронную почту:");
            student.Email = Console.ReadLine();

            Console.Write("Введите курс:");
            student.Course = Console.ReadLine();

            Console.Write("Введите группу:");
            student.Group = Console.ReadLine();

            student.GetFullPrint2();
            Console.ReadKey();

        }

        class Student
        {
            public string Surname { get; set; }
            public string Name { get; set; }
            public string MiddleName { get; set; }
            public string MiddleAfterbirth { get; set; }
            public string Address { get; set; }
            public string Telephone { get; set; }
            public string Email { get; set; }
            public string Course { get; set; }
            public string Group { get; set; }
            public string MiddleCreditableness { get; set; }
            public Student(string surname, string name, string middleName, string middleAfterbirth, string address, string telephone, string email, string course, string group, string middleCreditableness)
            {
                Surname = surname;
                Name = name;
                MiddleName = middleName;
                MiddleAfterbirth = middleAfterbirth;
                Address = address;
                Telephone = telephone;
                Email = email;
                Course = course;
                Group = group;
                MiddleCreditableness = middleCreditableness;
            }
            public Student()
            {
                Surname = "Берзегов";
                Name = "Тимур";
                MiddleName = "Сергеевич";
                MiddleAfterbirth = "04.08.2006";
                Address = "ул. Пушкина ";
                Telephone = "984572849";
                Email = "hrhhrhe@Gmail.ru";
                Course = "2";
                Group = "ИП11(СО)";
                MiddleCreditableness = "473287492";
            }

            public void DisplayInfo()
            {
                Console.WriteLine("-----------------------------------------------------------");
                Console.WriteLine($"Фамилия: {Surname}");
                Console.WriteLine($"Имя: {Name}");
                Console.WriteLine($"Отчество: {MiddleName}");
                Console.WriteLine($"Дата рождения: {MiddleAfterbirth}");
                Console.WriteLine($"Адрес: {Address}");
                Console.WriteLine($"Телефон: {Telephone}");
                Console.WriteLine($"Электронный адрес: {Email}");
                Console.WriteLine($"Курс: {Course}");
                Console.WriteLine($"Группа: {Group}");
                Console.WriteLine($"Номер зачетной книжки: {MiddleCreditableness}");
            }
            public string GetFullName()
            {
                return $"{Surname} {Name} {MiddleName} {MiddleAfterbirth} {Address} {Telephone} {Email} {Course} {Group} {MiddleCreditableness}";
            }
            public string Print()
            {
                return $"{Surname} {Name} {MiddleName} {MiddleAfterbirth} {Address} {Telephone} {Email} {Course} {Group} {MiddleCreditableness}";
            }
            public void GetFullPrint()
            {
                Console.WriteLine($"Фамилия: {Surname}");
                Console.WriteLine($"Имя: {Name}");
                Console.WriteLine($"Отчество: {MiddleName}");
                Console.WriteLine($"Дата рождения: {MiddleAfterbirth}");
                Console.WriteLine($"Адрес: {Address}");
                Console.WriteLine($"Телефон: {Telephone}");
                Console.WriteLine($"Электронный адрес: {Email}");
                Console.WriteLine($"Курс: {Course}");
                Console.WriteLine($"Группа: {Group}");
                Console.WriteLine($"Номер зачетной книжки: {MiddleCreditableness}");
            }
            public Student(string surname, string name, string middleName)
            {
                Surname = surname;
                Name = name;
                MiddleName = middleName;
            }
            public string Print1()
            {
                return $"{Surname} {Name} {MiddleName}";
            }
            public void GetFullPrint1()
            {
                Console.WriteLine("-----------------------------------------------------------");
                Console.WriteLine($"Фамилия: {Surname}");
                Console.WriteLine($"Имя: {Name}");
                Console.WriteLine($"Отчество: {MiddleName}");
            }
            public Student(string middleCreditableness)
            {
                MiddleCreditableness = middleCreditableness;
            }
            public string Print2()
            {
                return $"{MiddleCreditableness}";
            }
            public void GetFullPrint2()
            {
                Console.WriteLine($"Номер зачетной книжки: {MiddleCreditableness}");
                Console.WriteLine("-----------------------------------------------------------");
            }
        }
    }
}

