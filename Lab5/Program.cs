using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n=== ГОЛОВНЕ МЕНЮ ===");
            Console.WriteLine("1. Блок 1: Обробка дробів");
            Console.WriteLine("2. Блок 2: Обробка студентів");
            Console.WriteLine("0. Вихід");
            Console.Write("Виберіть блок (0-2): ");

            string choice = Console.ReadLine()?.Trim()!;

            switch (choice)
            {
                case "1":
                    Block1.run();
                    break;
                case "2":
                    Block2.run();
                    break;
                case "0":
                    Console.WriteLine("Завершення програми...");
                    return;
                default:
                    Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                    break;
            }
        }
    }
}
