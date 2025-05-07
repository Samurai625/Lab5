using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

public class Block2
{
    public struct Student
    {
        public string LastName;
        public string FirstName;
        public string MiddleName;
        public string Gender;
        public DateTime BirthDate;
        public int? MathMark;
        public int? PhysMark;
        public int? InfoMark;
        public int Scholarship;
    }

    public static void run()
    {
        Console.WriteLine("\n=== БЛОК 2: Обробка студентів ===");

        string fileName = "input.txt";
        if (!File.Exists(fileName))
        {
            Console.WriteLine($"Файл {fileName} не знайдено.");
            return;
        }

        var students = new List<Student>();
        int lineNumber = 0;

        foreach (var line in File.ReadLines(fileName))
        {
            lineNumber++;
            if (string.IsNullOrWhiteSpace(line)) continue;

            var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 9)
            {
                Console.WriteLine($"[Рядок {lineNumber}] Невірна кількість полів: {parts.Length} (очікується 9)");
                continue;
            }

            try
            {
                var student = new Student
                {
                    LastName = parts[0],
                    FirstName = parts[1],
                    MiddleName = parts[2],
                    Gender = ParseGender(parts[3]),
                    BirthDate = ParseDate(parts[4]),
                    MathMark = ParseMark(parts[5]),
                    PhysMark = ParseMark(parts[6]),
                    InfoMark = ParseMark(parts[7]),
                    Scholarship = int.Parse(parts[8])
                };

                students.Add(student);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Рядок {lineNumber}] Помилка парсингу: {ex.Message}");
            }
        }

        Console.WriteLine($"\nЗагалом зчитано студентів: {students.Count}");

        Console.WriteLine("\nСтуденти, які мають 5 з фізики:");
         for (int i = 0; i < students.Count; i++)
        {
            Student s = students[i];
            if (s.PhysMark.HasValue && s.PhysMark.Value == 5)
            {
                double math = s.MathMark.HasValue ? s.MathMark.Value : 2;
                double phys = s.PhysMark.HasValue ? s.PhysMark.Value : 2;
                double info = s.InfoMark.HasValue ? s.InfoMark.Value : 2;
                double average = (math + phys + info) / 3;

                Console.WriteLine($"{s.LastName} {s.FirstName} {s.MiddleName} | Середній бал: {average:F2} | Стипендія: {s.Scholarship}");
            }
        }
    }

    static string ParseGender(string input)
    {
        input = input.ToUpper();
        if ("MМЧ".Contains(input)) return "Ч";
        if ("FЖ".Contains(input)) return "Ж";
        throw new Exception("Невірне значення статі.");
    }

    static DateTime ParseDate(string input)
    {
        if (!DateTime.TryParseExact(input, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            throw new Exception("Невірний формат дати (очікується ДД.ММ.РРРР)");
        return date;
    }

    static int? ParseMark(string input)
    {
        if (input == "-") return null;
        if (int.TryParse(input, out int mark) && mark >= 2 && mark <= 5)
            return mark;
        throw new Exception("Невірна оцінка (очікується 2–5 або '-')");
    }
}
