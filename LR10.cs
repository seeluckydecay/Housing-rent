using System;
using System.Globalization;
using System.Text;

namespace HousingRental
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            const int Capacity = 100;

            int[] ids = new int[Capacity];
            string[] addresses = new string[Capacity];
            int[] roomsCounts = new int[Capacity];
            double[] areas = new double[Capacity];
            int[] floors = new int[Capacity];
            double[] monthlyPrices = new double[Capacity];
            DateTime[] publicationDates = new DateTime[Capacity];
            bool[] isRentedFlags = new bool[Capacity];
            int[] districtIds = new int[Capacity];

            int count = 0;
            int nextId = 1;

            bool running = true;

            while (running)
            {
                ShowMenu();
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": ShowNotImplemented(); Pause(); break;
                    case "2": ShowNotImplemented(); Pause(); break;
                    case "3": ShowNotImplemented(); Pause(); break;
                    case "4": ShowNotImplemented(); Pause(); break;
                    case "5": ShowNotImplemented(); Pause(); break;
                    case "6": HandleDistrictRoomsMenu(); Pause(); break;
                    case "7": ShowNotImplemented(); Pause(); break;
                    case "8": ShowNotImplemented(); Pause(); break;
                    case "9": ShowNotImplemented(); Pause(); break;
                    case "10": ShowNotImplemented(); Pause(); break;
                    case "11": ShowAbout(); Pause(); break;
                    case "0": running = false; ShowGoodbye(); break;
                    default: ShowInvalidChoice(); Pause(); break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("=== Оренда житла ===");
            Console.WriteLine("1 - Показати всі оголошення (поки не зроблено)");
            Console.WriteLine("2 - Додати оголошення (поки не зроблено)");
            Console.WriteLine("3 - Знайти за умовою (поки не зроблено)");
            Console.WriteLine("4 - Змінити оголошення (поки не зроблено)");
            Console.WriteLine("5 - Видалити оголошення (поки не зроблено)");
            Console.WriteLine("6 - Зведена таблиця вартості (райони / кімнати)");
            Console.WriteLine("7 - Зберегти у файл (поки не зроблено)");
            Console.WriteLine("8 - Завантажити з файлу (поки не зроблено)");
            Console.WriteLine("9 - Експорт у CSV (поки не зроблено)");
            Console.WriteLine("10 - Заповнити тестовими оголошеннями (поки не зроблено)");
            Console.WriteLine("11 - Довідка про систему та сутність");
            Console.WriteLine("0 - Вихід");
            Console.Write("Ваш вибір: ");
        }

        static void ShowNotImplemented()
        {
            Console.WriteLine("Поки не зроблено.");
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

        static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (int.TryParse(input, out int value) && value >= min && value <= max)
                {
                    return value;
                }

                Console.WriteLine($"Помилка: потрібне ціле число від {min} до {max}.");
            }
        }

        static double ReadDouble(string prompt, double min, double max)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (double.TryParse(input, out double value) && value >= min && value <= max)
                {
                    return value;
                }

                Console.WriteLine($"Помилка: потрібне число від {min} до {max}.");
            }
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

        static double CalculateCellPrice(int districtNumber, int roomsCount)
        {
            return 6000 + districtNumber * 1200 + roomsCount * 2500;
        }

        static void HandleDistrictRoomsMenu()
        {
            int rows = ReadInt("Введіть кількість районів (від 1 до 20): ", 1, 20);
            int cols = ReadInt("Введіть максимальну кількість кімнат (від 1 до 10): ", 1, 10);
            Console.WriteLine();

            PrintDistrictRoomsTable(rows, cols);
        }

        static void PrintDistrictRoomsTable(int rows, int cols)
        {
            if (rows <= 0 || cols <= 0)
            {
                Console.WriteLine("Помилка: розміри таблиці мають бути більшими за нуль.");
                return;
            }

            Console.WriteLine("Зведена таблиця вартості оренди за районами та кімнатами (грн/міс):\n");

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