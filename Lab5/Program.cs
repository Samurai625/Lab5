using System;
using static System.Math;

using MyFrac = (long nom, long denom);

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Введіть перший дріб (чисельник і знаменник):");
        var f1 = ReadFraction();

        Console.WriteLine("Введіть другий дріб (чисельник і знаменник):");
        var f2 = ReadFraction();

        Console.WriteLine("\nРезультати:");
        Console.WriteLine("f1 = " + MyFracToString(f1));
        Console.WriteLine("f2 = " + MyFracToString(f2));
        Console.WriteLine("Нормалізований f1 = " + MyFracToString(Normalize(f1)));
        Console.WriteLine("Нормалізований f2 = " + MyFracToString(Normalize(f2)));
        Console.WriteLine("ToStringWithIntPart(f1) = " + ToStringWithIntPart(f1));
        Console.WriteLine("ToStringWithIntPart(f2) = " + ToStringWithIntPart(f2));
        Console.WriteLine("DoubleValue(f1) = " + DoubleValue(f1));
        Console.WriteLine("DoubleValue(f2) = " + DoubleValue(f2));
        Console.WriteLine("Sum:(f1 + f2) = " + MyFracToString(Plus(f1, f2)));
        Console.WriteLine("Difference:(f1 - f2) = " + MyFracToString(Minus(f1, f2)));
        Console.WriteLine("Product:(f1 * f2) = " + MyFracToString(Multiply(f1, f2)));
        Console.WriteLine("Quotient:(f1 / f2) = " + MyFracToString(Divide(f1, f2)));

        Console.Write("\nВведіть n для обчислення виразів CalcExpr1 і CalcExpr2: ");
        int n = int.Parse(Console.ReadLine()!);

        Console.WriteLine("CalcExpr1(n) = " + MyFracToString(CalcExpr1(n)));
        Console.WriteLine("CalcExpr2(n) = " + MyFracToString(CalcExpr2(n)));
    }

    static MyFrac ReadFraction()
    {
        Console.Write("Чисельник: ");
        long nom = long.Parse(Console.ReadLine()!);
        Console.Write("Знаменник: ");
        long denom = long.Parse(Console.ReadLine()!);
        while (denom == 0)
        {
            Console.Write("Знаменник не може бути нулем. Введіть ще раз: ");
            denom = long.Parse(Console.ReadLine()!);
        }
        return (nom, denom);
    }

    static string MyFracToString(MyFrac f)
    {
        return $"{f.nom} / {f.denom}";
    }

    static MyFrac Normalize(MyFrac f)
    {
        long gcd = GCD(f.nom, f.denom);
        long nom = f.nom / gcd;
        long denom = f.denom / gcd;

        if (denom < 0)
        {
            nom = -nom;
            denom = -denom;
        }

        return (nom, denom);
    }

    static string ToStringWithIntPart(MyFrac f)
    {
        f = Normalize(f);
        long intPart = f.nom / f.denom;
        long fracNom = Abs(f.nom % f.denom);
        long denom = f.denom;

        if (f.nom < 0 && intPart == 0)
            return $"-({intPart}+{fracNom}/{denom})";
        else if (f.nom < 0 && intPart != 0)
            return $"-({Abs(intPart)}+{fracNom}/{denom})";
        else
            return $"({intPart}+{fracNom}/{denom})";
    }

    static double DoubleValue(MyFrac f)
    {
        return (double)f.nom / f.denom;
    }

    static MyFrac Plus(MyFrac f1, MyFrac f2)
    {
        return Normalize((
            f1.nom * f2.denom + f2.nom * f1.denom,
            f1.denom * f2.denom
        ));
    }

    static MyFrac Minus(MyFrac f1, MyFrac f2)
    {
        return Normalize((
            f1.nom * f2.denom - f2.nom * f1.denom,
            f1.denom * f2.denom
        ));
    }

    static MyFrac Multiply(MyFrac f1, MyFrac f2)
    {
        return Normalize((
            f1.nom * f2.nom,
            f1.denom * f2.denom
        ));
    }

    static MyFrac Divide(MyFrac f1, MyFrac f2)
    {
        return Normalize((
            f1.nom * f2.denom,
            f1.denom * f2.nom
        ));
    }

    static MyFrac CalcExpr1(int n)
    {
        MyFrac sum = (0, 1);
        for (int i = 1; i <= n; i++)
        {
            sum = Plus(sum, (1, i * (i + 1)));
        }
        return Normalize(sum);
    }

    static MyFrac CalcExpr2(int n)
    {
        MyFrac result = (1, 1);
        for (int i = 2; i <= n; i++)
        {
            result = Multiply(result, Minus((1, 1), (1, i * i)));
        }
        return Normalize(result);
    }

    static long GCD(long a, long b) //Greatest Common Divisor
    {
        while (b != 0)
            (a, b) = (b, a % b);
        return Abs(a);
    }
}
