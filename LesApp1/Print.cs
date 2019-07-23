using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace LesApp1
{
    /// <summary>
    /// Друк
    /// </summary>
    static class PTC //PrintToConsole
    {
        /// <summary>
        /// Масив кольорів консолі
        /// </summary>
        public readonly static List<ConsoleColor> arrayColor = Enum
            .GetValues(typeof(ConsoleColor))
            .Cast<ConsoleColor>()
            .ToList();

        /// <summary>
        /// Вивід рядка кольором заданим по номеру
        /// </summary>
        /// <param name="stroka"></param>
        /// <param name="color"></param>
        public static void Print(string stroka, int color)
        {
            int minColor = arrayColor.Cast<int>().Min(),
                maxColor = arrayColor.Cast<int>().Max();

            if (minColor <= color && color <= maxColor)
            {
                Print(stroka, arrayColor[color]);
#if false
                // конвертування номеру кольору в тип
                Console.ForegroundColor = arrayColor[color];
                // перевірка чи колір тексту не чорний
                if (arrayColor[color] == ConsoleColor.Black)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                }
                Console.WriteLine(stroka);
                Console.ResetColor(); 
#endif
            }
            else
            {
                Console.WriteLine("\n\tВихід за межі діапазону кольорів.");
            }
        }

        /// <summary>
        /// Вивід рядка заданим кольором
        /// </summary>
        /// <param name="stroka"></param>
        /// <param name="color"></param>
        public static void Print(string stroka, ConsoleColor color)
        {
            // конвертування номеру кольору в тип
            Console.ForegroundColor = color;
            // перевірка чи колір тексту не чорний
            if (color == ConsoleColor.Black)
            {
                Console.BackgroundColor = ConsoleColor.White;
            }
            Console.WriteLine(stroka);
            Console.ResetColor();
        }


    }
}
