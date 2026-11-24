using System;
using System.Text;

namespace HousingRental
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            bool running = true;

            while (running)
            {
                Console.WriteLine("=== Оренда житла ===");
                Console.WriteLine("1 - Довідка про систему та сутність");
                Console.WriteLine("2 - Таблиця оренди за площею (табулювання)");
                Console.WriteLine("3 - Зведена таблиця вартості (райони / кімнати)");
                Console.WriteLine("4 - Показати всі оголошення (поки не зроблено)");
                Console.WriteLine("5 - Додати оголошення (поки не зроблено)");
                Console.WriteLine("0 - Вихід");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Поки не зроблено.");
                        Pause();
                        break;
                    case "2":
                        Console.WriteLine("Поки не зроблено.");
                        Pause();
                        break;
                    case "3":
                        Console.WriteLine("Поки не зроблено.");
                        Pause();
                        break;
                    case "4":
                    case "5":
                        Console.WriteLine("Цей функціонал з'явиться у наступних лабораторних роботах.");
                        Pause();
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("Роботу завершено. До побачення!");
                        break;
                    default:
                        Console.WriteLine("Помилка: такого пункту меню не існує. Спробуйте ще раз.");
                        Pause();
                        break;
                }
            }
        }

        static void Pause()
        {
            Console.WriteLine("\nНатисніть будь-яку клавішу для повернення в меню...");
            Console.ReadKey(true);
            Console.Clear();
        }
    }
}