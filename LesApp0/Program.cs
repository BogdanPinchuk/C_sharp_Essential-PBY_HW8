using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesApp0
{
    class Program
    {
        static void Main()
        {
            // Enable Unicode
            Console.OutputEncoding = Encoding.Unicode;

            // введення дати
            Console.Write("\n\tВведіть дату свого дня народження: ");
            string s = Console.ReadLine();

            // конвертуємо рядок в дату
            bool error = DateTime.TryParse(s, out DateTime dateB);
            // аналіз чи можна далі продовжувати 
            AnaliseOfInputNumber(error);

            // дана в наступному році
            DateTime dateNB =
                new DateTime(DateTime.Now.Year + 1, dateB.Month, dateB.Day);

            int days = dateNB.Subtract(DateTime.Now).Days;

            Console.Write($"\n\tВам залишилось {days} днів до свого наступного дня народження.");

            // повторення
            DoExitOrRepeat();
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
    }
}
