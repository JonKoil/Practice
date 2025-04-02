using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Patient[] patients = new Patient[]
        {
            new Patient("Зайцев", "Захар", "Васильевич", "ул. Сталина 15", 4365, "Апендицит"),
            new Patient("Урохара", "Киске", "Нарутович", "ул. Окинава 10", 2346, "Грипп"),
            new Patient("семенюк", "Ларик", "Дмитриевич", "ул.Хирасимова 1", 3486, "Грипп"),
            new Patient("Куросаки", "Ичиго", "Бличевич", "ул.Душ 666 ", 6666, "Корона"),
            new Patient("Якубович", "Леонид", "Аркадьевич", "ул. Поле чудес", 7777, "Грипп")
        };

            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine("Пациенты с диагнозом 'Грипп':");
            foreach (var patient in patients)
            {
                if (patient.Diagnosis == "Грипп")
                {
                    patient.Show();
                }
            }
            Console.WriteLine("Пациенты с номером медицинской карты в интервале [1000, 5000]:");
            foreach (var patient in patients)
            {
                if (patient.MedicalCardNumber >= 1000 && patient.MedicalCardNumber <= 5000)
                {
                    patient.Show();
                }
            }
        }
        public class Patient
        {
           
            public string LastName { get; private set; } 
            public string FirstName { get; private set; } 
            public string MiddleName { get; private set; } 
            public string Address { get; private set; } 
            public int MedicalCardNumber { get; private set; } 
            public string Diagnosis { get; private set; }

            
            public Patient(string lastName, string firstName, string middleName, string address, int medicalCardNumber, string diagnosis)
            {
                LastName = lastName;
                FirstName = firstName;
                MiddleName = middleName;
                Address = address;
                MedicalCardNumber = medicalCardNumber;
                Diagnosis = diagnosis;
            }

            
            public void Set(string lastName, string firstName, string middleName, string address, int medicalCardNumber, string diagnosis)
            {
                LastName = lastName;
                FirstName = firstName;
                MiddleName = middleName;
                Address = address;
                MedicalCardNumber = medicalCardNumber;
                Diagnosis = diagnosis;
            }

            
            public (string, string, string, string, int, string) Get()
            {
                return (LastName, FirstName, MiddleName, Address, MedicalCardNumber, Diagnosis);
            }

            
            public void Show()
            {
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine($"Пациент: {LastName} {FirstName} {MiddleName}");
                Console.WriteLine($"Адрес: {Address}");
                Console.WriteLine($"Номер медицинской карты: {MedicalCardNumber}");
                Console.WriteLine($"Диагноз: {Diagnosis}");
            }

        }
    }
}

   

