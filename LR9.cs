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

            const int Capacity = 100;
            double[] rentPrices = new double[Capacity];
            int count = 0;

            bool running = true;

            while (running)
            {
                ShowMenu();
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": ShowAbout(); Pause(); break;
                    case "2": HandleTabulationMenu(); Pause(); break;
                    case "3": HandleDistrictRoomsMenu(); Pause(); break;
                    case "4": ShowPricePerSqmDemo(); Pause(); break;
                    case "5":
                        double newPrice = ReadDouble("Введіть місячну ціну оренди (1000 - 200000 грн): ", 1000, 200000);
                        AddRentPrice(rentPrices, ref count, newPrice, Capacity);
                        Pause();
                        break;
                    case "6": ShowNotImplemented(); Pause(); break;
                    case "7": ShowNotImplemented(); Pause(); break;
                    case "8": ShowNotImplemented(); Pause(); break;
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
            Console.WriteLine("5 - Додати значення ціни оренди у масив");
            Console.WriteLine("6 - Показати всі ціни з масиву (поки не зроблено)");
            Console.WriteLine("7 - Видалити ціну за номером (поки не зроблено)");
            Console.WriteLine("8 - Показати статистику та дослід ref/out (поки не зроблено)");
            Console.WriteLine("0 - Вихід");
            Console.Write("Ваш вибір: ");
        }

        static void ShowNotImplemented()
        {
            Console.WriteLine("Цей функціонал буде додано у наступних кроках.");
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

                if (!int.TryParse(input, out int value))
                {
                    Console.WriteLine("Помилка: потрібно ввести ціле число.");
                    continue;
                }

                if (value < min || value > max)
                {
                    Console.WriteLine($"Помилка: число має бути в межах від {min} до {max}.");
                    continue;
                }

                return value;
            }
        }

        static double ReadDouble(string prompt, double min, double max)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!double.TryParse(input, out double value))
                {
                    Console.WriteLine("Помилка: потрібно ввести число.");
                    continue;
                }

                if (value < min || value > max)
                {
                    Console.WriteLine($"Помилка: значення має бути в межах від {min} до {max}.");
                    continue;
                }

                return value;
            }
        }

        static void AddRentPrice(double[] values, ref int count, double newValue, int capacity)
        {
            if (count >= capacity)
            {
                Console.WriteLine("Помилка: сховище заповнене (досягнуто ліміт 100 записів).");
                return;
            }

            values[count] = newValue;
            count++;
            Console.WriteLine($"Значення {newValue:F2} грн додано. Поточна кількість записів у Main: {count}.");
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
            double samplePrice = ReadDouble("Введіть місячну ціну оренди (1000 - 200000 грн): ", 1000, 200000);
            double sampleArea = ReadDouble("Введіть площу квартири (10 - 500 м²): ", 10, 500);
            double pricePerSqm = CalculatePricePerSquareMeter(samplePrice, sampleArea);

            Console.WriteLine($"\nЦіна за квадратний метр: {pricePerSqm:F2} грн/м²");
        }

        static void HandleTabulationMenu()
        {
            double fromArea = ReadDouble("Введіть початкову площу (10 - 200 м²): ", 10, 200);
            double toArea = ReadDouble($"Введіть кінцеву площу ({fromArea} - 300 м²): ", fromArea, 300);
            double step = ReadDouble("Введіть крок зміни площі (0.5 - 50 м²): ", 0.5, 50);
            Console.WriteLine();

            PrintRentTabulation(fromArea, toArea, step);
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