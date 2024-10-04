using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace ExceptionLesson
{
    internal class ComplexMenu
    {
        Dictionary<string, string> Values = new Dictionary<string, string>();
        private static void SetDown()
        {
            if (selectedValue < 3)
            {
                selectedValue++;
            }
            else
            {
                selectedValue = 1;
            }
        }

        private static void SetUp()
        {
            if (selectedValue > 1)
            {
                selectedValue--;
            }
            else
            {
                selectedValue = 3;
            }
        }

        private void PrintMenu()
        {
            Console.WriteLine("Для выхода нажмие Escape");
            var a = Input("Введите значение a: ");
            var b = Input("Введите значение b: ");
            var c = Input("Введите значение c: ");

            Values.Add("a", a);
            Values.Add("b", b);
            Values.Add("c", c);
        }

        private static int selectedValue = 1;

        private static void WriteCursor(int pos)
        {
            Console.SetCursorPosition(1, pos);
            Console.Write(">");
            Console.SetCursorPosition(1, pos);
        }

        private static void ClearCursor(int pos)
        {
            Console.SetCursorPosition(1, pos);
            Console.Write(" ");
            Console.SetCursorPosition(1, pos);
        }

        public void Start()
        {
            ConsoleKeyInfo ki;

            selectedValue = 1;

            PrintMenu();

            WriteCursor(selectedValue);
            do
            {

                ki = Console.ReadKey();
                ClearCursor(selectedValue);
                switch (ki.Key)
                {
                    case ConsoleKey.UpArrow:
                        SetUp();
                        break;
                    case ConsoleKey.DownArrow:
                        SetDown();
                        break;
                    case ConsoleKey.D1:
                    case ConsoleKey.D2:
                    case ConsoleKey.D3:
                    case ConsoleKey.D4:
                        selectedValue = int.Parse(ki.KeyChar.ToString());
                        break;
                }
                WriteCursor(selectedValue);
            } while (ki.Key != ConsoleKey.Escape);

        }

        private static string Input(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

    }
}
