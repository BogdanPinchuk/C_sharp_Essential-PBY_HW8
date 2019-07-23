using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesApp1
{
    class Program
    {
        static void Main()
        {
            // Enable Unicode
            Console.OutputEncoding = Encoding.Unicode;

            // вивід довідкової інформації
            Console.WriteLine("\n\tНаявні кольори та їх номера:\n");
            foreach (var color in PTC.arrayColor)
            {
                PTC.Print("\t" + color.ToString() + " - " + (int)color, color);
            }

            // введення дати
            Console.Write("\n\tВведіть номер кольору для Вашого тексту: ");

            // конвертуємо рядок в номер кольору
            bool error = int.TryParse(Console.ReadLine(), out int numColor);
            // аналіз чи можна далі продовжувати 
            AnaliseOfInputNumber(error);

            // введення рядка
            Console.Write("\n\tВведіть Ваш рядок: ");

            // збереження позиції курсора
            Cursor.Save();
            string s = Console.ReadLine();

            // відновлення координат з перезаписом в кольорі
            Cursor.Back();
            PTC.Print(s, numColor);
            
            // повторення
            DoExitOrRepeat();
        }

        /// <summary>
        /// Метод виходу або повторення методу Main()
        /// </summary>
        static void DoExitOrRepeat()
        {
            Console.WriteLine("\n\nСпробувати ще раз: [т, н]");
            Console.Write("\t");
            var button = Console.ReadKey(true);

            if ((button.KeyChar.ToString().ToLower() == "т") ||
                (button.KeyChar.ToString().ToLower() == "n")) // можливо забули переключити розкладку клавіатури
            {
                Console.Clear();
                Main();
                // без використання рекурсії
                //Process.Start(Assembly.GetExecutingAssembly().Location);
                //Environment.Exit(0);
            }
            else
            {
                // закриває консоль
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Умова коли невірно введені дані
        /// </summary>
        /// <param name="res"></param>
        static void AnaliseOfInputNumber(bool res)
        {
            if (!res)
            {
                Console.WriteLine("\nНевірно введені дані!");
                DoExitOrRepeat();
            }
        }

        /// <summary>
        /// Позиція курсора
        /// </summary>
        static class Cursor
        {
            private static int X { get; set; }
            private static int Y { get; set; }

            /// <summary>
            /// Зебереження координат
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>

            public static void Save()
            {
                X = Console.CursorLeft;
                Y = Console.CursorTop;
            }

            /// <summary>
            /// Відновлення координат
            /// </summary>
            public static void Back()
            {
                Console.SetCursorPosition(X, Y);
            }
        }
    }
}
