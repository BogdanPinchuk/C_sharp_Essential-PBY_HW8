using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesApp2
{
    /// <summary>
    /// Дані парцівника
    /// </summary>
    // Чому post - записано в умові як робітник, хоча перекладається як повідомлення - невідомо
    class Post
    {
        /// <summary>
        /// Працівник: посада = необхідність відпрацьованих годин на місяць
        /// </summary>
        // В інтернеті не найшов якоїсь бази даних, то роблю дані на угад
        // посади взято для C# .Net Developer 
        // https://jobs.dou.ua/salaries/#period=jun2019&city=Kyiv&title=System%20Architect&language=C%23%2F.NET&spec=&exp1=0&exp2=10
        // також написано, що праця буває до 60 годин/тиждень
        // тому припустив, що це може бути максимумом якоъсь кривоъ розподілу співробітників
        // щоб числа були більш менш випадкові і реальні одночасно
        // дані взяті із тематики "Досвід роботи в ІТ"
        // https://dou.ua/lenta/articles/portrait-2019/
        // пронормувавши дані і прийнявши, що максимум буде 60 годин/тиждень
        // + округливши числа, записав умовну кількість годин як мають парцювати
        // а так як в місяці приблизно 4 тижні то перемножити це на 4
        public enum Employee
        {
            JuniorSE = 108,
            MiddleSE = 207,
            SeniorSE = 240,
            TechnicalLead = 141,
            SystemArchitect = 99,
        }

        /// <summary>
        /// ПІБ - працівника
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Необхідно відпрацювати годин на місяць
        /// </summary>
        public int HoursPerMonth { get; set; }
        /// <summary>
        /// Посада, яку обіймає працівник
        /// </summary>
        public Employee Position { get; set; }
    }
}
