using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExceptionLesson
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string a = "";
            string b = "";
            string c = "";
            string error = "";
            string errorOutOfBorder = "";
            var wrongValues = new Dictionary<string, string>();

            Console.WriteLine("a * x^2 + b * x + c = 0");

            while (true)
            {
                //ComplexMenu.Start();
                
                a = Input("Введите значение a: ");
                b = Input("Введите значение b: ");
                c = Input("Введите значение c: ");

                try
                {
                    Console.WriteLine(CalculateQuadraticEquation(error, errorOutOfBorder, a, b, c, wrongValues));
                }
                catch (FormatException ex)
                {
                    FormatData(ex.Message, Severity.Error, wrongValues);
                    wrongValues.Clear();
                }
                catch (SolutionNotFoundException ex)
                {
                    FormatData(ex.Message, Severity.Warning, wrongValues);
                    wrongValues.Clear();
                }
                catch (SolutionOutOfBordersException ex)
                {
                    FormatData(ex.Message, Severity.OutOfBorders, wrongValues);
                    wrongValues.Clear();
                }
            }
        }
        private static string Input(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        private static void FormatData(string message, Severity severity, IDictionary data)
        {
            if (severity == Severity.Error)
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }

            if (severity == Severity.Warning) 
            { 
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
            }

            if (severity == Severity.OutOfBorders)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
            }

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine(message);
            Console.WriteLine("--------------------------------------------------");

            if (data.Contains("wrongA") || data.Contains("wrongB") || data.Contains("wrongC"))
            {
                Console.WriteLine($"a = {data["wrongA"]}");
                Console.WriteLine($"b = {data["wrongB"]}");
                Console.WriteLine($"c = {data["wrongC"]}");
            }

            if (data.Contains("wrongLA") || data.Contains("wrongLB") || data.Contains("wrongLC"))
            {
                Console.WriteLine($"a = {data["wrongLA"]}");
                Console.WriteLine($"b = {data["wrongLB"]}");
                Console.WriteLine($"c = {data["wrongLC"]}");
            }
            
            Console.ResetColor();
        }

        private static string CalculateQuadraticEquation(string error, string errorOutOfBorder, string a, string b, string c, Dictionary<string, string> wrongValues)
        {
            long.TryParse(a, out var la);
            long.TryParse(b, out var lb);
            long.TryParse(c, out var lc);
            if (la > 2147483647 || la < -2147483648)
            {
                errorOutOfBorder += "a не должно выходить за рамки диапазона [-2147483648, 2147483647].\n";
                wrongValues.Add("wrongLA", a);
            }
            if (lb > 2147483647 || lb < -2147483648)
            {
                errorOutOfBorder += "b не должно выходить за рамки диапазона [-2147483648, 2147483647].\n";
                wrongValues.Add("wrongLB", b);
            }
            if (lc > 2147483647 || lc < -2147483648)
            {
                errorOutOfBorder += "c не должно выходить за рамки диапазона [-2147483648, 2147483647].";
                wrongValues.Add("wrongLC", c);
            }

            if (!wrongValues.ContainsKey("wrongLA"))
            {
                wrongValues.Add("wrongLA", la.ToString());
            }
            if (!wrongValues.ContainsKey("wrongLB"))
            {
                wrongValues.Add("wrongLB", lb.ToString());
            }
            if (!wrongValues.ContainsKey("wrongLC"))
            {
                wrongValues.Add("wrongLC", lc.ToString());
            }

            if (!string.IsNullOrEmpty(errorOutOfBorder))
            {
                throw new SolutionOutOfBordersException("Введено число за пределами допустимого диапазона!");
            }

            wrongValues.Clear();

            if (!int.TryParse(a, out var ia))
            {
                error += "Неверный формат параметра a.\n";
                wrongValues.Add("wrongA", a);
            }
            if (!int.TryParse(b, out var ib))
            {
                error += "Неверный формат параметра b.\n";
                wrongValues.Add("wrongB", b);
            }
            if (!int.TryParse(c, out var ic))
            {
                error += "Неверный формат параметра c.";
                wrongValues.Add("wrongC", c);
            }

            if (!wrongValues.ContainsKey("wrongA"))
            {
                wrongValues.Add("wrongA", ia.ToString());
            }
            if (!wrongValues.ContainsKey("wrongB"))
            {
                wrongValues.Add("wrongB", ib.ToString());
            }
            if (!wrongValues.ContainsKey("wrongC"))
            {
                wrongValues.Add("wrongC", ic.ToString());
            }

            if (!string.IsNullOrEmpty(error))
            {
                var ex = new FormatException(error);
                throw ex;
            }

            var d = Math.Pow(ib, 2) - (4 * ia * ic);
            var x1 = (-ib - Math.Sqrt(d)) / (2 * ia);
            var x2 = (-ib + Math.Sqrt(d)) / (2 * ia);

            string result = "";

            if (d < 0)
            {
                throw new SolutionNotFoundException("Вещественных значений не найдено!");
            }

            if (x1 != x2)
            {
                result += $"x1 = {x1}, x2 = {x2}";
            }
            
            if (x1 == x2)
            {
                result += $"x = {x1}";
            }

            return result;
        }
    }
}