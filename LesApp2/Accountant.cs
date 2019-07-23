using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Примітка. Дану технологію виплати ніде не використовують або не знаю чи десь
// використовують, вона була придумана для розширення функціоналу і 
// додатковго внесення математики, а то її якось маловато в ДЗ)))

namespace LesApp2
{
    /// <summary>
    /// Бухгалтер
    /// </summary>
    class Accountant
    {
        /// <summary>
        /// Заробітня платня по медіані в залежності від посади
        /// </summary>
        // https://jobs.dou.ua/salaries/#period=jun2019&city=Kyiv&title=Junior%20Software%20Engineer&language=C%23%2F.NET&spec=&exp1=0&exp2=0
        // JuniorSE = 500$ в місяць - досвід <1 року
        // MiddleSE = 1600$ в місяць - досвід 2 роки
        // SeniorSE = 3500$ в місяць - досвід 4 роки
        // TechnicalLead = 3500$ в місяць - досвід 8 років
        // SystemArchitect = 5300$ в місяць - досвід 10 років
        public enum Salary
        {
            JuniorSE = 500,
            MiddleSE = 1600,
            SeniorSE = 3500,
            TechnicalLead = 3800,
            SystemArchitect = 5300,
        }

        /// <summary>
        /// Результуюча ЗП
        /// </summary>
        public int FinalySalary { get; private set; }
        /// <summary>
        /// Різниця між отриманою і базовою ЗП, тобто недоотримані і преміальні гроші відносно базової
        /// </summary>
        public int DifSalary { get; private set; }
        /// <summary>
        /// Інформація, про як працював робітник
        /// </summary>
        public string WorkerResult { get; private set; }

        /// <summary>
        /// Запит на премію
        /// </summary>
        /// <returns></returns>
        public bool AskForBonus(Post worker, int hours)
        {
            #region Explain
            // припустимо, що якщо робітник відпрацвав необхідну кількість часу
            // з допуском +-5% то він отримує 100% ЗП, якщо перепрацював більше +5%
            // то ЗП оплачується x2 за перевищені базові години
            // а якщо неоопрацював до норми то кількість недопрацьованих годин
            // множиться на х0,5 і віжнімається від напрацьованого
            // припустимо, що ці +-5% - це запізення через пробки і т.д., або затримка
            // на робочому місці через необхідність завершення завдання, або довге
            // збирання додому з робочого місця
            // незай -25% це максимальний недоробіток в годинах,
            // а +50% максимальний переробіток (який реалізуватиметься рандомно для кожного користувача) 
            #endregion

            // результуюча ЗП
            bool bonus = default;

            // зчитуємо дані із робітника, тобто необхідну норму виконання годин
            int normHours = (int)worker.Position;
            // ЗП в місяць по посаді
            int normSalary = ConvertToSalary(worker.Position);

            // перевірка чи робітниик увійшов в базовий діапазон з допуском
            // +-5% і вираховуємо ЗП згідно базової і залежно від часу

            if ((0.95 * normHours <= hours) && (hours <= 1.05 * normHours)) // план виконано
            {
                bonus = false;
                // обраховується коефіцієнт - зп/год і оплачується
                // по кількості відпрацьованих годин
                FinalySalary = (int)Math.Round(
                    (decimal)hours * normSalary / normHours, 
                    MidpointRounding.AwayFromZero);
                WorkerResult = "працівник просто виконав робочий план";
            }
            else if (hours < 0.95 * normHours) // коли план не виконано
            {
                bonus = false;
                #region help
                // вираховується базова зп/год і оплачується по кількості
                // відпрацьованих годин, а також мінус штраф за невиконання плану
                // який обраховується як, недопрацьовані години * х0,5 від зп/год
                // (int)((decimal)hours * normSalary / normHours -
                //    (decimal)0.5 * Math.Abs(normHours - hours) * normSalary / normHours)
                // але формулу можна спростити 
                #endregion
                FinalySalary = (int)Math.Round(
                    (decimal)(hours - 0.5 * Math.Abs(normHours - hours)) 
                    * normSalary / normHours,
                    MidpointRounding.AwayFromZero);
                WorkerResult = "працівник невиконав робочий план";
            }
            else // план перевиконано
            {
                // в даному випадку оплачуємо норму по нормальній ціні зп/год
                // а все, що перевиконано вище норми, окремо оплачуємо по тарифу x2
                bonus = true;
                FinalySalary = (int)Math.Round(
                    (decimal)(normHours + 2 * Math.Abs(hours - normHours))
                    * normSalary / normHours,
                    MidpointRounding.AwayFromZero);
                WorkerResult = "працівник перевиконав робочий план";
            }

            // розрахунок різниці в ЗП відносно базової по медіані
            DifSalary = FinalySalary - normSalary;

            return bonus;
        }

        /// <summary>
        /// Видання ЗП в місяць в залежності від посади
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private int ConvertToSalary(Post.Employee position)
        {
            // створюємо масив ЗП
            var salary = Enum.GetValues(typeof(Salary))
                .Cast<Salary>().ToArray();

            for (int i = 0; i < salary.Length; i++)
            {
                // якщо імена співпадають видаємо ЗП
                if (salary[i].ToString() == position.ToString())
                {
                    return (int)salary[i];
                }
            }

            // заглушка
            return default;
        }

    }
}
