using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesApp2
{
    class Program
    {
        static void Main()
        {
            // Enable Unicode
            Console.OutputEncoding = Encoding.Unicode;

            // Створюємо рандомізатор для створення випадкової команди працюючих
            Random rnd = new Random();

            // припустимо що ми маємо таку кількість робітників
            int empCount = rnd.Next(10, 28);

            // Створення команди робітників певної компанії
            Post[] team = new Post[empCount];

            // вивід матриці посад
            var posArray = Enum.GetValues(typeof(Post.Employee))
                .Cast<Post.Employee>().ToArray();

            var posArray2 = Enum.GetValues(typeof(Accountant.Salary));

            // Заповнення робітниками команди
            for (int i = 0; i < team.Length; i++)
            {
                // вибір позиції для робітника
                var posTemp = posArray[rnd.Next(0, posArray.Length)];
                // заповнення даними про працівника
                team[i] = new Post()
                {
                    FullName = $"Employee {i + 1}",
                    Position = posTemp,
                    HoursPerMonth = (int)posTemp
                };
            }

            // згідно рандому, визначивши допустимі межі робочого часу
            // задаємо те скільки пропрацював робітник
            // і виводимо інформацію про те чи отримав премію і яку ЗП отримав
            for (int i = 0; i < team.Length; i++)
            {
                // розрахунок min/max допустимого діапазону робочого часу
                // робітника в залежності від посади
                int minHours = (int)(Math.Round(0.75 * team[i].HoursPerMonth, MidpointRounding.AwayFromZero)),
                    maxHours = (int)(Math.Round(1.5 * team[i].HoursPerMonth, MidpointRounding.AwayFromZero));
                // створення випадкового відпрацювання годин
                int hours = rnd.Next(minHours, maxHours + 1);
                // вивід інформації в консоль
                Console.WriteLine(ShowInfo(team[i], hours));
            }

            // повторення
            DoExitOrRepeat();
        }

        /// <summary>
        /// Вивід інформації
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="hour"></param>
        private static string ShowInfo(Post worker, int hour)
        {
            // Наймаємо на роботу бухгалтера/менеджера який рахуватиме ЗП робітників
            Accountant manager = new Accountant();

            string s = new StringBuilder()
                .Append($"\n\tРобітник: {worker.FullName},")
                .Append($"\n\t\tякий обіймає посаду: {worker.Position.ToString()},")
                .Append($"\n\t\tвідпрацював за місяць: {hour:N0} годин,")
                .Append($"\n\t\tвін премію: {((manager.AskForBonus(worker, hour)) ? "" : "не")}отримав,")
                .Append($"\n\t\tоскільки: {manager.WorkerResult},")
                .Append($"\n\t\tза цей місяць отримує ЗП в розмірі: {manager.FinalySalary:N0} $,")
                .Append($"\n\t\tвідмінність від базової ЗП становить: {manager.DifSalary:N0} $.")
                .ToString();

            return s;
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
