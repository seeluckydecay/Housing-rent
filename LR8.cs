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
                ShowMenu();
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": ShowAbout(); Pause(); break;
                    case "2": PrintRentTabulation(25.0, 80.0, 5.5); Pause(); break;
                    case "3": ShowDistrictRoomsTable(); Pause(); break;
                    case "4": ShowPricePerSqmDemo(); Pause(); break;
                    case "5": ShowNotImplemented(); Pause(); break;
                    case "0": running = false; ShowGoodbye(); break;
                    default: ShowInvalidChoice(); Pause(); break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("=== Оренда житла ===");
            Console.WriteLine("1 - Довідка про систему та сутність");
            Console.WriteLine("2 - Таблиця оренди за площею (табулювання)");
            Console.WriteLine("3 - Зведена таблиця вартості (райони / кімнати)");
            Console.WriteLine("4 - Розрахунок ціни за квадратний метр (приклад)");
            Console.WriteLine("5 - Додати оголошення (поки не зроблено)");
            Console.WriteLine("0 - Вихід");
            Console.Write("Ваш вибір: ");
        }

        static void ShowNotImplemented()
        {
            Console.WriteLine("Цей функціонал з'явиться у наступних лабораторних роботах.");
        }

        static void ShowGoodbye()
        {
            Console.WriteLine("Роботу завершено. До побачення!");
        }

        static void ShowInvalidChoice()
        {
            Console.WriteLine("Помилка: такого пункту меню не існує. Спробуйте ще раз.");
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

        static double CalculateMonthlyRent(double area, double baseRate)
        {
            return area * baseRate + Math.Sqrt(area) * 250.0;
        }

        static double CalculatePricePerSquareMeter(double monthlyPrice, double area)
        {
            if (area <= 0)
            {
                return 0;
            }
            return monthlyPrice / area;
        }

        static double CalculateCellPrice(int districtNumber, int roomsCount)
        {
            return 6000 + districtNumber * 1200 + roomsCount * 2500;
        }

        static void ShowPricePerSqmDemo()
        {
            double samplePrice = 14500.0;
            double sampleArea = 52.5;
            double pricePerSqm = CalculatePricePerSquareMeter(samplePrice, sampleArea);

            Console.WriteLine("Контрольний розрахунок ціни за 1 м²:");
            Console.WriteLine($"Місячна оренда: {samplePrice:F2} грн, площа: {sampleArea:F2} м²");
            Console.WriteLine($"Ціна за квадратний метр: {pricePerSqm:F2} грн/м²");
        }

        static void PrintRentTabulation(double fromArea, double toArea, double step)
        {
            if (step <= 0 || fromArea > toArea)
            {
                Console.WriteLine("Помилка: некоректні межі або крок табулювання.");
                return;
            }

            Console.WriteLine("Розрахунок орієнтовної вартості оренди залежно від площі:");
            Console.WriteLine("Формула: Базова ставка (180 грн/м²) + коефіцієнт комфорту\n");

            double baseRate = 180.0;

            Console.WriteLine($"{"Площа (м²)",12} | {"Ціна/міс (грн)",16}");
            Console.WriteLine(new string('-', 31));

            for (double area = fromArea; area <= toArea + 1e-9; area += step)
            {
                double monthlyPrice = CalculateMonthlyRent(area, baseRate);
                Console.WriteLine($"{area,12:F2} | {monthlyPrice,16:F2}");
            }
        }

        static void ShowDistrictRoomsTable()
        {
            Console.Write("Введіть кількість районів (рядків таблиці): ");
            if (!int.TryParse(Console.ReadLine(), out int rows) || rows <= 0)
            {
                Console.WriteLine("Помилка: кількість рядків має бути цілим числом більшим за нуль.");
                return;
            }

            Console.Write("Введіть максимальну кількість кімнат (стовпців таблиці): ");
            if (!int.TryParse(Console.ReadLine(), out int cols) || cols <= 0)
            {
                Console.WriteLine("Помилка: кількість стовпців має бути цілим числом більшим за нуль.");
                return;
            }

            Console.WriteLine("\nЗведена таблиця вартості оренди за районами та кімнатами (грн/міс):\n");

            Console.Write($"{"Район \\ Кімнати",-16}");
            for (int col = 1; col <= cols; col++)
            {
                Console.Write($"{col + " кімн.",12}");
            }
            Console.WriteLine($"{"Підсумок",14}");

            int lineWidth = 16 + cols * 12 + 14;
            Console.WriteLine(new string('-', lineWidth));

            for (int row = 1; row <= rows; row++)
            {
                Console.Write($"{"Район №" + row,-16}");
                double rowSum = 0;

                for (int col = 1; col <= cols; col++)
                {
                    double price = CalculateCellPrice(row, col);
                    rowSum += price;
                    Console.Write($"{price,12:F0}");
                }

                Console.WriteLine($"{rowSum,14:F0}");
            }

            Console.WriteLine(new string('-', lineWidth));

            Console.Write($"{"Разом по стовп.",-16}");
            double grandTotal = 0;

            for (int col = 1; col <= cols; col++)
            {
                double colSum = 0;
                for (int row = 1; row <= rows; row++)
                {
                    colSum += CalculateCellPrice(row, col);
                }
                grandTotal += colSum;
                Console.Write($"{colSum,12:F0}");
            }

            Console.WriteLine($"{grandTotal,14:F0}");
        }
    }
}