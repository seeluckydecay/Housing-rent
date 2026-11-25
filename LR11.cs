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
                    case "1":
                        PrintAllListings(
                            ids, addresses, roomsCounts, areas, floors,
                            monthlyPrices, publicationDates, isRentedFlags, districtIds,
                            count);
                        Pause();
                        break;
                    case "2":
                        AddListing(
                            ids, addresses, roomsCounts, areas, floors,
                            monthlyPrices, publicationDates, isRentedFlags, districtIds,
                            ref count, ref nextId, Capacity);
                        Pause();
                        break;
                    case "3":
                        HandleSearchMenu(
                            ids, addresses, roomsCounts, areas, floors,
                            monthlyPrices, publicationDates, isRentedFlags, districtIds,
                            count);
                        Pause();
                        break;
                    case "4":
                        EditListing(
                            ids, addresses, roomsCounts, areas, floors,
                            monthlyPrices, publicationDates, isRentedFlags, districtIds,
                            count);
                        Pause();
                        break;
                    case "5":
                        DeleteListing(
                            ids, addresses, roomsCounts, areas, floors,
                            monthlyPrices, publicationDates, isRentedFlags, districtIds,
                            ref count);
                        Pause();
                        break;
                    case "6": HandleDistrictRoomsMenu(); Pause(); break;
                    case "7": ShowNotImplemented(); Pause(); break;
                    case "8": ShowNotImplemented(); Pause(); break;
                    case "9": ShowNotImplemented(); Pause(); break;
                    case "10":
                        SeedDemoListings(
                            ids, addresses, roomsCounts, areas, floors,
                            monthlyPrices, publicationDates, isRentedFlags, districtIds,
                            ref count, ref nextId, Capacity);
                        Pause();
                        break;
                    case "11": ShowAbout(); Pause(); break;
                    case "12":
                        ShowSummary(ids, monthlyPrices, areas, districtIds, count);
                        Pause();
                        break;
                    case "0": running = false; ShowGoodbye(); break;
                    default: ShowInvalidChoice(); Pause(); break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("=== Оренда житла ===");
            Console.WriteLine("1 - Показати всі оголошення");
            Console.WriteLine("2 - Додати оголошення");
            Console.WriteLine("3 - Знайти за умовою (адреса / діапазон ціни)");
            Console.WriteLine("4 - Змінити оголошення");
            Console.WriteLine("5 - Видалити оголошення");
            Console.WriteLine("6 - Зведена таблиця вартості (райони / кімнати)");
            Console.WriteLine("7 - Зберегти у файл (поки не зроблено)");
            Console.WriteLine("8 - Завантажити з файлу (поки не зроблено)");
            Console.WriteLine("9 - Експорт у CSV (поки не зроблено)");
            Console.WriteLine("10 - Заповнити тестовими оголошеннями");
            Console.WriteLine("11 - Довідка про систему та сутність");
            Console.WriteLine("12 - Підсумки по оголошеннях");
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

        static string ReadText(string prompt, int minLength)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (input != null && input.Trim().Length >= minLength)
                {
                    return input.Trim();
                }

                Console.WriteLine($"Помилка: рядок не може бути порожнім (мінімум {minLength} символи).");
            }
        }

        static DateTime ReadDate(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (DateTime.TryParseExact(input, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                {
                    if (date.Year >= 1900 && date <= DateTime.Today)
                    {
                        return date;
                    }
                    Console.WriteLine("Помилка: дата має бути від 01.01.1900 і не в майбутньому.");
                    continue;
                }

                Console.WriteLine("Помилка: введіть коректну дату у форматі дд.мм.рррр.");
            }
        }

        static bool ReadBool(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (input != null)
                {
                    string normalized = input.Trim().ToLower();
                    if (normalized == "так")
                    {
                        return true;
                    }
                    if (normalized == "ні")
                    {
                        return false;
                    }
                }

                Console.WriteLine("Помилка: введіть «так» або «ні».");
            }
        }

        static void CalculateSummarySinglePass(
            int[] ids,
            double[] monthlyPrices,
            double[] areas,
            int[] districtIds,
            int count,
            out double avgPricePerSqm,
            out double maxPrice,
            out int maxPriceId,
            out double minPrice,
            out int minPriceId,
            out double avgMonthlyPrice)
        {
            if (count <= 0)
            {
                avgPricePerSqm = 0;
                maxPrice = 0;
                maxPriceId = 0;
                minPrice = 0;
                minPriceId = 0;
                avgMonthlyPrice = 0;
                return;
            }

            double sumPrices = monthlyPrices[0];
            double sumPricePerSqm = areas[0] > 0 ? monthlyPrices[0] / areas[0] : 0;
            int validAreaCount = areas[0] > 0 ? 1 : 0;

            maxPrice = monthlyPrices[0];
            maxPriceId = ids[0];
            minPrice = monthlyPrices[0];
            minPriceId = ids[0];

            for (int i = 1; i < count; i++)
            {
                double currentPrice = monthlyPrices[i];
                sumPrices += currentPrice;

                if (areas[i] > 0)
                {
                    sumPricePerSqm += currentPrice / areas[i];
                    validAreaCount++;
                }

                if (currentPrice > maxPrice)
                {
                    maxPrice = currentPrice;
                    maxPriceId = ids[i];
                }

                if (currentPrice < minPrice)
                {
                    minPrice = currentPrice;
                    minPriceId = ids[i];
                }
            }

            avgMonthlyPrice = sumPrices / count;
            avgPricePerSqm = validAreaCount > 0 ? sumPricePerSqm / validAreaCount : 0;
        }

        static void ShowSummary(
            int[] ids,
            double[] monthlyPrices,
            double[] areas,
            int[] districtIds,
            int count)
        {
            if (count == 0)
            {
                Console.WriteLine("Сховище порожнє. Немає даних для підрахунку підсумків.");
                return;
            }

            CalculateSummarySinglePass(
                ids, monthlyPrices, areas, districtIds, count,
                out double avgPricePerSqm,
                out double maxPrice,
                out int maxPriceId,
                out double minPrice,
                out int minPriceId,
                out double avgMonthlyPrice);

            Console.WriteLine("=== Підсумки по оголошеннях ===");
            Console.WriteLine($"  Кількість записів: {count}");
            Console.WriteLine($"  Середня ціна за 1 м² (підсумкова величина теми): {avgPricePerSqm:F2} грн/м²");
            Console.WriteLine($"  Найбільша місячна ціна: {maxPrice:F2} грн (оголошення №{maxPriceId})");
            Console.WriteLine($"  Найменша місячна ціна: {minPrice:F2} грн (оголошення №{minPriceId})");
            Console.WriteLine($"  Середня місячна ціна оренди: {avgMonthlyPrice:F2} грн");
        }

        static int FindIndexById(int[] ids, int count, int id)
        {
            for (int i = 0; i < count; i++)
            {
                if (ids[i] == id)
                {
                    return i;
                }
            }
            return -1;
        }

        static void DeleteListing(
            int[] ids, string[] addresses, int[] roomsCounts,
            double[] areas, int[] floors, double[] monthlyPrices,
            DateTime[] publicationDates, bool[] isRentedFlags, int[] districtIds,
            ref int count)
        {
            if (count == 0)
            {
                Console.WriteLine("Сховище порожнє. Видаляти нічого.");
                return;
            }

            int id = ReadInt("Введіть номер оголошення для видалення: ", 1, int.MaxValue);
            int index = FindIndexById(ids, count, id);

            if (index == -1)
            {
                Console.WriteLine($"Оголошення №{id} немає.");
                return;
            }

            Console.Write($"Справді видалити оголошення №{id} ({addresses[index]})? (так/ні): ");
            string confirm = Console.ReadLine();
            if (confirm == null || confirm.Trim().ToLower() != "так")
            {
                Console.WriteLine("Видалення скасовано.");
                return;
            }

            for (int i = index; i < count - 1; i++)
            {
                ids[i] = ids[i + 1];
                addresses[i] = addresses[i + 1];
                roomsCounts[i] = roomsCounts[i + 1];
                areas[i] = areas[i + 1];
                floors[i] = floors[i + 1];
                monthlyPrices[i] = monthlyPrices[i + 1];
                publicationDates[i] = publicationDates[i + 1];
                isRentedFlags[i] = isRentedFlags[i + 1];
                districtIds[i] = districtIds[i + 1];
            }

            count--;
            Console.WriteLine($"Оголошення №{id} видалено. Лишилося записів: {count}.");
        }

        static void EditListing(
            int[] ids, string[] addresses, int[] roomsCounts,
            double[] areas, int[] floors, double[] monthlyPrices,
            DateTime[] publicationDates, bool[] isRentedFlags, int[] districtIds,
            int count)
        {
            if (count == 0)
            {
                Console.WriteLine("Сховище порожнє. Редагувати нічого.");
                return;
            }

            int id = ReadInt("Введіть номер оголошення для редагування: ", 1, int.MaxValue);
            int index = FindIndexById(ids, count, id);

            if (index == -1)
            {
                Console.WriteLine($"Оголошення №{id} не знайдено.");
                return;
            }

            Console.WriteLine($"\nРедагування оголошення №{ids[index]} (натисніть Enter, щоб залишити поле без змін):");

            Console.Write($"Адреса [{addresses[index]}]: ");
            string addressInput = Console.ReadLine();
            if (addressInput != null && addressInput.Trim().Length > 0)
            {
                if (addressInput.Trim().Length >= 2)
                {
                    addresses[index] = addressInput.Trim();
                }
                else
                {
                    Console.WriteLine("Попередження: закоротка адреса, залишено попереднє значення.");
                }
            }

            while (true)
            {
                Console.Write($"Кількість кімнат [{roomsCounts[index]}]: ");
                string roomsInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(roomsInput))
                {
                    break;
                }
                if (int.TryParse(roomsInput, out int newRooms) && newRooms >= 1 && newRooms <= 20)
                {
                    roomsCounts[index] = newRooms;
                    break;
                }
                Console.WriteLine("Помилка: потрібне ціле число від 1 до 20 або порожній Enter.");
            }

            while (true)
            {
                Console.Write($"Площа, м² [{areas[index]:F1}]: ");
                string areaInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(areaInput))
                {
                    break;
                }
                if (double.TryParse(areaInput, out double newArea) && newArea >= 10 && newArea <= 1000)
                {
                    areas[index] = newArea;
                    break;
                }
                Console.WriteLine("Помилка: потрібне число від 10 до 1000 або порожній Enter.");
            }

            while (true)
            {
                Console.Write($"Поверх [{floors[index]}]: ");
                string floorInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(floorInput))
                {
                    break;
                }
                if (int.TryParse(floorInput, out int newFloor) && newFloor >= 1 && newFloor <= 100)
                {
                    floors[index] = newFloor;
                    break;
                }
                Console.WriteLine("Помилка: потрібне ціле число від 1 до 100 або порожній Enter.");
            }

            while (true)
            {
                Console.Write($"Ціна за місяць, грн [{monthlyPrices[index]:F0}]: ");
                string priceInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(priceInput))
                {
                    break;
                }
                if (double.TryParse(priceInput, out double newPrice) && newPrice >= 500 && newPrice <= 500000)
                {
                    monthlyPrices[index] = newPrice;
                    break;
                }
                Console.WriteLine("Помилка: потрібне число від 500 до 500000 або порожній Enter.");
            }

            while (true)
            {
                Console.Write($"Дата публікації [{publicationDates[index]:dd.MM.yyyy}]: ");
                string dateInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(dateInput))
                {
                    break;
                }
                if (DateTime.TryParseExact(dateInput.Trim(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime newDate)
                    && newDate.Year >= 1900 && newDate <= DateTime.Today)
                {
                    publicationDates[index] = newDate;
                    break;
                }
                Console.WriteLine("Помилка: введіть дату у форматі дд.мм.рррр або порожній Enter.");
            }

            while (true)
            {
                string currentRented = isRentedFlags[index] ? "так" : "ні";
                Console.Write($"Чи здано зараз [{currentRented}]: ");
                string rentedInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(rentedInput))
                {
                    break;
                }
                string norm = rentedInput.Trim().ToLower();
                if (norm == "так")
                {
                    isRentedFlags[index] = true;
                    break;
                }
                if (norm == "ні")
                {
                    isRentedFlags[index] = false;
                    break;
                }
                Console.WriteLine("Помилка: введіть «так», «ні» або порожній Enter.");
            }

            while (true)
            {
                Console.Write($"Код району [{districtIds[index]}]: ");
                string districtInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(districtInput))
                {
                    break;
                }
                if (int.TryParse(districtInput, out int newDistrict) && newDistrict >= 1 && newDistrict <= 10)
                {
                    districtIds[index] = newDistrict;
                    break;
                }
                Console.WriteLine("Помилка: потрібне ціле число від 1 до 10 або порожній Enter.");
            }

            Console.WriteLine($"\nОголошення №{ids[index]} успішно оновлено.");
        }

        static void PrintTableHeader()
        {
            Console.WriteLine($"{"№",4} {"Адреса",-28} {"Кімн.",6} {"Площа",8} {"Пов.",5} {"Ціна/міс",11} {"Дата публ.",12} {"Здано",7} {"Район",6}");
            Console.WriteLine(new string('-', 95));
        }

        static void PrintListingRow(
            int id, string address, int rooms, double area,
            int floor, double price, DateTime pubDate, bool isRented, int districtId)
        {
            string rentedMark = isRented ? "так" : "ні";
            Console.WriteLine(
                $"{id,4} {address,-28} {rooms,6} {area,8:F1} {floor,5} {price,11:F0} {pubDate,12:dd.MM.yyyy} {rentedMark,7} {districtId,6}");
        }

        static void HandleSearchMenu(
            int[] ids, string[] addresses, int[] roomsCounts,
            double[] areas, int[] floors, double[] monthlyPrices,
            DateTime[] publicationDates, bool[] isRentedFlags, int[] districtIds,
            int count)
        {
            if (count == 0)
            {
                Console.WriteLine("Сховище порожнє. Пошук неможливий.");
                return;
            }

            Console.WriteLine("Оберіть режим пошуку:");
            Console.WriteLine("  1 - За частиною адреси");
            Console.WriteLine("  2 - За діапазоном місячної ціни");
            int mode = ReadInt("Ваш вибір (1 або 2): ", 1, 2);
            Console.WriteLine();

            if (mode == 1)
            {
                string query = ReadText("Введіть частину адреси для пошуку: ", 1);
                Console.WriteLine();
                SearchByAddress(
                    ids, addresses, roomsCounts, areas, floors,
                    monthlyPrices, publicationDates, isRentedFlags, districtIds,
                    count, query);
            }
            else
            {
                double minPrice = ReadDouble("Мінімальна ціна, грн (0 - 500000): ", 0, 500000);
                double maxPrice = ReadDouble("Максимальна ціна, грн (0 - 500000): ", 0, 500000);
                Console.WriteLine();
                SearchByPriceRange(
                    ids, addresses, roomsCounts, areas, floors,
                    monthlyPrices, publicationDates, isRentedFlags, districtIds,
                    count, minPrice, maxPrice);
            }
        }

        static void SearchByAddress(
            int[] ids, string[] addresses, int[] roomsCounts,
            double[] areas, int[] floors, double[] monthlyPrices,
            DateTime[] publicationDates, bool[] isRentedFlags, int[] districtIds,
            int count, string query)
        {
            int foundCount = 0;

            for (int i = 0; i < count; i++)
            {
                if (addresses[i].Contains(query))
                {
                    if (foundCount == 0)
                    {
                        PrintTableHeader();
                    }

                    PrintListingRow(
                        ids[i], addresses[i], roomsCounts[i], areas[i],
                        floors[i], monthlyPrices[i], publicationDates[i], isRentedFlags[i], districtIds[i]);
                    foundCount++;
                }
            }

            if (foundCount == 0)
            {
                Console.WriteLine($"За запитом «{query}» оголошень не знайдено.");
            }
            else
            {
                Console.WriteLine(new string('-', 95));
                Console.WriteLine($"Знайдено оголошень: {foundCount}.");
            }
        }

        static void SearchByPriceRange(
            int[] ids, string[] addresses, int[] roomsCounts,
            double[] areas, int[] floors, double[] monthlyPrices,
            DateTime[] publicationDates, bool[] isRentedFlags, int[] districtIds,
            int count, double fromPrice, double toPrice)
        {
            if (fromPrice > toPrice)
            {
                Console.WriteLine("Помилка: нижня межа ціни не може бути більшою за верхню.");
                return;
            }

            int foundCount = 0;

            for (int i = 0; i < count; i++)
            {
                if (monthlyPrices[i] >= fromPrice && monthlyPrices[i] <= toPrice)
                {
                    if (foundCount == 0)
                    {
                        PrintTableHeader();
                    }

                    PrintListingRow(
                        ids[i], addresses[i], roomsCounts[i], areas[i],
                        floors[i], monthlyPrices[i], publicationDates[i], isRentedFlags[i], districtIds[i]);
                    foundCount++;
                }
            }

            if (foundCount == 0)
            {
                Console.WriteLine($"Оголошень із ціною від {fromPrice:F0} до {toPrice:F0} грн не знайдено.");
            }
            else
            {
                Console.WriteLine(new string('-', 95));
                Console.WriteLine($"Знайдено оголошень у діапазоні: {foundCount}.");
            }
        }

        static void AddListing(
            int[] ids, string[] addresses, int[] roomsCounts,
            double[] areas, int[] floors, double[] monthlyPrices,
            DateTime[] publicationDates, bool[] isRentedFlags, int[] districtIds,
            ref int count, ref int nextId, int capacity)
        {
            if (count == capacity)
            {
                Console.WriteLine("Сховище заповнене, видаліть зайві записи.");
                return;
            }

            string address = ReadText("Адреса (вулиця та номер будинку): ", 2);
            int rooms = ReadInt("Кількість кімнат (1 - 20): ", 1, 20);
            double area = ReadDouble("Площа, м² (10 - 1000): ", 10, 1000);
            int floor = ReadInt("Поверх (1 - 100): ", 1, 100);
            double price = ReadDouble("Ціна за місяць, грн (500 - 500000): ", 500, 500000);
            DateTime pubDate = ReadDate("Дата публікації (дд.мм.рррр): ");
            bool isRented = ReadBool("Чи здано зараз (так/ні): ");
            int districtId = ReadInt("Код району з довідника (1 - 10): ", 1, 10);

            ids[count] = nextId;
            addresses[count] = address;
            roomsCounts[count] = rooms;
            areas[count] = area;
            floors[count] = floor;
            monthlyPrices[count] = price;
            publicationDates[count] = pubDate;
            isRentedFlags[count] = isRented;
            districtIds[count] = districtId;

            count++;
            nextId++;

            Console.WriteLine($"\nДодано оголошення №{ids[count - 1]}. Усього записів: {count}.");
        }

        static void SeedDemoListings(
            int[] ids, string[] addresses, int[] roomsCounts,
            double[] areas, int[] floors, double[] monthlyPrices,
            DateTime[] publicationDates, bool[] isRentedFlags, int[] districtIds,
            ref int count, ref int nextId, int capacity)
        {
            string[] demoAddresses =
            {
                "вул. Соборна, 14, кв. 5",
                "просп. Миру, 82, кв. 19",
                "вул. Шевченка, 7, кв. 3",
                "вул. Київська, 105, кв. 44",
                "бульв. Незалежності, 21, кв. 12",
                "вул. Лесі Українки, 30, кв. 8"
            };
            int[] demoRooms = { 1, 2, 3, 2, 1, 4 };
            double[] demoAreas = { 36.5, 54.0, 78.2, 49.0, 32.0, 96.5 };
            int[] demoFloors = { 3, 7, 2, 9, 5, 4 };
            double[] demoPrices = { 9500, 14000, 21500, 12500, 8800, 26000 };
            DateTime[] demoDates =
            {
                new DateTime(2026, 10, 12),
                new DateTime(2026, 10, 18),
                new DateTime(2026, 11, 2),
                new DateTime(2026, 11, 10),
                new DateTime(2026, 11, 19),
                new DateTime(2026, 11, 22)
            };
            bool[] demoRented = { false, true, false, false, true, false };
            int[] demoDistricts = { 1, 2, 1, 3, 2, 4 };

            int added = 0;
            for (int i = 0; i < demoAddresses.Length; i++)
            {
                if (count == capacity)
                {
                    break;
                }

                ids[count] = nextId;
                addresses[count] = demoAddresses[i];
                roomsCounts[count] = demoRooms[i];
                areas[count] = demoAreas[i];
                floors[count] = demoFloors[i];
                monthlyPrices[count] = demoPrices[i];
                publicationDates[count] = demoDates[i];
                isRentedFlags[count] = demoRented[i];
                districtIds[count] = demoDistricts[i];

                count++;
                nextId++;
                added++;
            }

            Console.WriteLine($"Додано тестових оголошень: {added}. Усього записів у сховищі: {count}.");
        }

        static double CalculateAveragePricePerSqm(double[] monthlyPrices, double[] areas, int count)
        {
            if (count <= 0)
            {
                return 0;
            }

            double sumPricePerSqm = 0;
            int validCount = 0;

            for (int i = 0; i < count; i++)
            {
                if (areas[i] > 0)
                {
                    sumPricePerSqm += monthlyPrices[i] / areas[i];
                    validCount++;
                }
            }

            return validCount > 0 ? sumPricePerSqm / validCount : 0;
        }

        static void PrintAllListings(
            int[] ids, string[] addresses, int[] roomsCounts,
            double[] areas, int[] floors, double[] monthlyPrices,
            DateTime[] publicationDates, bool[] isRentedFlags, int[] districtIds,
            int count)
        {
            if (count == 0)
            {
                Console.WriteLine("Записів поки немає.");
                return;
            }

            PrintTableHeader();

            for (int i = 0; i < count; i++)
            {
                PrintListingRow(
                    ids[i], addresses[i], roomsCounts[i], areas[i],
                    floors[i], monthlyPrices[i], publicationDates[i], isRentedFlags[i], districtIds[i]);
            }

            Console.WriteLine(new string('-', 95));

            double overallAvgSqm = CalculateAveragePricePerSqm(monthlyPrices, areas, count);
            Console.WriteLine($"Записів: {count}. Загальна середня ціна за 1 м²: {overallAvgSqm:F2} грн/м².");
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