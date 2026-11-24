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
                        ShowAbout();
                        Pause();
                        break;
                    case "2":
                        ShowRentTabulation();
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

        static void ShowAbout()
        {
            Console.WriteLine("Тема проєкту: Оренда житла (Варіант №27)");
            Console.WriteLine("Сутності: Користувач, Оголошення (головна), Район (довідник).");
            Console.WriteLine("\nПоля головної сутності «Оголошення» (9 полів):");
            Console.WriteLine("  1. int код");
            Console.WriteLine("  2. string адреса");
            Console.WriteLine("  3. int кількість кімнат");
            Console.WriteLine("  4. double площа");
            Console.WriteLine("  5. int поверх");
            Console.WriteLine("  6. double ціна за місяць");
            Console.WriteLine("  7. DateTime дата публікації");
            Console.WriteLine("  8. bool чи здано");
            Console.WriteLine("  9. int код району (посилання на довідник)");
            Console.WriteLine("\nПідсумкова величина: середня ціна за квадратний метр по районах.");
        }

        static void ShowRentTabulation()
        {
            Console.WriteLine("Розрахунок орієнтовної вартості оренди залежно від площі:");
            Console.WriteLine("Формула: Базова ставка (180 грн/м²) + коефіцієнт комфорту\n");

            double fromArea = 25.0;
            double toArea = 80.0;
            double step = 5.5;
            double baseRate = 180.0;

            Console.WriteLine($"{"Площа (м²)",12} | {"Ціна/міс (грн)",16}");
            Console.WriteLine(new string('-', 31));

            for (double area = fromArea; area <= toArea + 1e-9; area += step)
            {
                double monthlyPrice = area * baseRate + Math.Sqrt(area) * 250.0;
                Console.WriteLine($"{area,12:F2} | {monthlyPrice,16:F2}");
            }
        }
    }
}